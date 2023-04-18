using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

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

        // this is basically a list of timestamps with data
        private EnvironmentalDataFile? edf = null;

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


            // attach the necessary events for each form
            // init form needs one event for either button
            init.BeginBackgroundCapture += BackgroundDataReading;
            init.DisplayData += FileExplorerTransition;

            // fileExplorer forms needs one for both the select file and 
            // cancel buttons
            fileExplorer.FileLoad += LoadFile;
            fileExplorer.CancelDisplay += ReturnToHomeScreen;


            // the only form showing at the beginning should be the initForm
            init.Show();
            fileExplorer.Hide();



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

            edf = new EnvironmentalDataFile(filename);

            // I need to pass this data file into the graphing form and then update the drawings once
            // they are written
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


    }
}
