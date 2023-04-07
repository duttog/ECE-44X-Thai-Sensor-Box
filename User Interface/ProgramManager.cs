using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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



        private void onFormClosed(object? sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms.Count == 0)
            {
                Application.Exit();
            }
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

            // create the first form
            var initForm = CreateForm<initForm>();
            var fileExplorer = CreateForm<Form1>();
            fileExplorer.Hide();
            var result = initForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                Action nextForm = initForm.getAction();
                

                if (nextForm == Action.BeginFileExplorer)
                {
                    //fileExplorer.Show();
                    
                    result = fileExplorer.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        dataFile = fileExplorer.getSelectedFile();
                    }

                    else if (result == DialogResult.Cancel)
                    {
                        return;
                    }

                    ExitThread();
                }


                else if (nextForm == Action.BeginBackgroundProcess)
                {
                    BackgroundDataReading();
                }
            }

        }

        /*
         * The user chose to display data. Begin by opening the file explorer and then
         * if a valid file is chosen, open up the graphing form to display the data in 
         * the file chosen
         */
        private void DataIllustrationProcess()
        {
            var fileExplorer = CreateForm<Form1>();
            

            // var dataDisplay = CreateForm<displayData>(dataFile);
            // dataDisplay.Show();
            fileExplorer.Close();
            return;
        }

        /*
         * Yet to implement 
         */
        private void BackgroundDataReading()
        {

        }




    }
}
