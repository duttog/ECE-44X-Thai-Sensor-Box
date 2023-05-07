using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Globalization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;
using System.IO.Ports;
using System.Reflection;
using System.DirectoryServices;

namespace test_project
{
    public enum Action
    {
        BeginBackgroundProcess,
        BeginFileExplorer,
        BeginDataCharts,
    }

    class ProgramManager : ApplicationContext
    {
        private string dataFile = "";
        private initForm init;
        private Form1 fileExplorer;
        private dataForm graphs;
        private backgroundDataInit backgroundData;
        private string? comPort;
        SerialPort? hostNode;

        // this is basically a list of timestamps with data
        // making the data and getter static allows for one copy
        // (the current copy) to be accessible from anywhere in the program
        // but innaccessable to change elsewhere
        private static EnvironmentalDataFile? edf = null;
        public static EnvironmentalDataFile? getEDF()
        {
            return edf;
        }



        private void onFormClosed(object? sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        public T CreateForm<T>() where T : Form, new()
        {
            var ret = new T();
            ret.FormClosed += onFormClosed;
            return ret;
        }

        private static Lazy<ProgramManager> _current = new Lazy<ProgramManager>();
        public static ProgramManager Current => _current.Value;

        public ProgramManager()
        {
            /*
             * the flow of the forms should work like this:
             * boot with the first form of either starting the background manager or 
             * selecting a file. return a dialog result to determine and query the form
             * to determine which subsequent form to open.
             * 
             * continue process for both program flow cases.
             */

            // create all forms for the application
            init = CreateForm<initForm>();
            fileExplorer = CreateForm<Form1>();
            graphs = CreateForm<dataForm>();
            backgroundData = CreateForm<backgroundDataInit>(); 


            // attach the necessary events for each form
            // init form needs one event for either button
            init.BeginBackgroundCapture += BackgroundDataReading;
            init.DisplayData += FileExplorerTransition;

            // fileExplorer forms needs one for both the select file and 
            // cancel buttons
            fileExplorer.FileLoad += LoadFile;
            fileExplorer.CancelDisplay += ReturnToHomeScreen;

            // add any events to the background data screen
            backgroundData.beginDataCollection += DataCollection;
            
            // the only form showing at the beginning should be the initForm
            init.Show();
            fileExplorer.Hide();
            graphs.Hide();

            


            // the rest of the program control should be handled through user events...
            // this class should basically be handling events from the pages themselves as they throw them
        }


        /// <summary>
        /// Event Handlers for program management
        /// </summary>

      
        /// <summary>
        /// Invoker: initForm
        /// If the user chooses to display data, the file explorer should be shown
        /// </summary>
        private void FileExplorerTransition(object? sender, EventArgs e)
        {
            init.Hide();
            fileExplorer.Show();
        }

        /// <summary>
        /// Invoker: initForm
        /// Begins the background data reading process.Should end up at the graph
        /// form (empty graphs if necessary).
        /// </summary>
        private void BackgroundDataReading(object? sender, EventArgs e)
        {
            // after finishing this form, we should be able to open the data file
            init.Hide();

            // show the background data window
            backgroundData.Show();
        }

        /// <summary>
        /// Invoker: Form1 (fileExplorer)
        /// Exchanges the file explorer page for the data display page. Parses the data
        /// from the file selected.
        /// </summary>
        private void LoadFile(object? sender, string filename)
        {
            fileExplorer.Hide();
            // show an empty data form while the program parses

            // I need to pass this data file into the graphing form and then update the drawings once
            // they are written
            graphs.Refresh();
            graphs.Show();

            edf = new EnvironmentalDataFile(filename);

            
        }

        /// <summary>
        /// Invoker: Form1
        /// If the user chooses to cancel the file selection, returns them to the homepage 
        /// of the screen.
        /// </summary>
        private void ReturnToHomeScreen(object? sender, EventArgs e)
        {
            fileExplorer.Hide();
            init.Refresh();
            init.Show();
        }


        private void DataCollection(object? sender, string[] output_info)
        {
            backgroundData.Hide();

            // the file name and com port have been determined
            comPort = output_info[0];
            dataFile= output_info[1];

            // open the com port to timeout every 10 seconds
            hostNode = new SerialPort(comPort);
            hostNode.ReadTimeout = 10000;
            hostNode.DataReceived += DataRead;
            hostNode.Open();


            graphs.Refresh();
            graphs.Show();

            edf = new EnvironmentalDataFile(dataFile);
        }

        private void DataRead(object? sender, SerialDataReceivedEventArgs e)
        {

            List<string> input_data = new List<string>(); 
            List<string> curated_data = new List<string>();
            

            // this reads until either an empty buffer or a timeout exception
            try
            {
                while (true)
                {
                    input_data.Add(hostNode.ReadLine());
                }
            }

            catch
            {
                string[] data = input_data.ToArray();
                foreach (string rawData in data)
                {
                    // disregard test readings
                    if (rawData.Contains("TEST:"))
                        continue;

                    else
                    {
                        StringBuilder sb = new StringBuilder();
                        for (int i = (rawData.IndexOf(' ') + 1); i < rawData.Length - 1; i++)
                        {
                            sb.Append(rawData[i]);
                        }
                        

                        string sensorData = sb.ToString();
                        curated_data.Add(sensorData);
                    }
                }


                StreamWriter writeFile = new StreamWriter(new FileStream(dataFile, FileMode.Append | FileMode.Create));

                writeFile.Write(CurateEDFData(curated_data));

                writeFile.Close();
            }
            
        }

        private string CurateEDFData(List<string> curated_data)
        {
            StringBuilder edfData = new StringBuilder();
            string date_template = DateTime.Now.ToString(CultureInfo.GetCultureInfo("de-DE"));
            date_template = date_template.Replace('.', '-');
            date_template = date_template.Replace(' ', ',');

            // we also need to trim the seconds from the reading... find the last occurence of the
            // ':' character and trim form that index
            int colon = date_template.LastIndexOf(':');

            // might have to remove a newline charcter
            date_template = date_template.Remove(colon);
            edfData.Append(date_template + ";\n");

            foreach (string rawData in curated_data)
            {
                
                edfData.Append(ParseRawData(rawData));
            }

            edfData.Append('\n');
            return edfData.ToString();
        }

        private string ParseRawData(string data)
        {
            StringBuilder sb = new StringBuilder();
            string[] tokens = data.Split(',');

            // token list:
            // 0: node number
            // 1: temp
            // 2: humiditiy
            // 3: water level
            // 4: wind speed

            // scope issue :(
            int index = 0;

            for (int i = 0; i < 3; i++)
            {
                index = tokens[i].IndexOf(':');
                sb.Append(tokens[i].Substring(index + 1) + ',');
            }

            // water level and wind speed print out-of-order
            index = tokens[4].IndexOf(':');
            sb.Append(tokens[4].Substring(index + 1) + ',');

            index = tokens[3].IndexOf(':');
            sb.Append(tokens[3].Substring(index + 1));
            sb.Append(";\n");

            return sb.ToString();
        }



    }
}
