using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace test_project
{
    public partial class backgroundDataInit : Form
    {
        public event EventHandler<string[]> beginDataCollection;

        private bool fileSelected = false;
        private bool comSelected = false;

        private string? currentCOM = null;
        private string? fileName = null;

        private readonly char[] illegal_chars = { '<', '>', ':', '\"', '\\', '/', '|', '?', '*', '.' };
        private readonly string[] illegal_names = { "CON", "PRN", "AUX", "NUL", "COM1",
                                                    "COM2", "COM3", "COM4", "COM5", "COM6",
                                                    "COM7", "COM8", "COM9", "LPT1", "LPT2",
                                                    "LPT3", "LPT4", "LPT5", "LPT6", "LPT7", "LPT8", "LPT9" };

        public backgroundDataInit()
        {
            InitializeComponent();
        }

        private void comPortList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
        }

        private void fileNameButton_MouseClick(object sender, MouseEventArgs e)
        {
            // grab the file name
            var tempFile = fileTitleBox.Text;

            // no text was entered into the textbox
            if (tempFile == null)
            {
                fileName = null;
                return;
            }

            // an illegal character was entered
            foreach (char bad_char in illegal_chars)
            {
                if (tempFile.Contains(bad_char))
                {
                    // invalid file name, print error message and exit
                    fileErrorLabel.Text = "File Title cannot contain the character \'" + bad_char + "\'!";
                    return;
                }
            }

            // the file 
            foreach (string bad_name in illegal_names)
            {
                if (tempFile.Equals(bad_name))
                {
                    // invalid file name, print error message and exit
                    fileErrorLabel.Text = "File Title  " + bad_name + " is reserved by the operating system!";
                    return;
                }
            }

            // technically the period check is irrelevant because I already check but never hurts to be careful
            if (tempFile.EndsWith('.') || tempFile.EndsWith(' '))
            {
                // invalid file name, print error message and exit
                fileErrorLabel.Text = "File Title cannot end with either a space or period!";
                return;
            }

            // the file title is valid, save it for use!
            fileName = tempFile + ".edf";
            fileSelected = true;

            // if both com and file title have been selected, then invoke the correct event
            if (comSelected && fileSelected)
            {
                CreateReturnArgs();
            }
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            currentCOM = e.Item.Text;

            // if they double click on something other than a list item
            if (currentCOM == null)
            {
                return;
            }

            comSelected = true;

            // if both com and file title have been selected, then invoke the correct event
            if (comSelected && fileSelected)
            {
                CreateReturnArgs();
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CreateReturnArgs()
        {
            // double check to make sure everything is ready
            if (currentCOM == null || fileName == null)
            {
                return;
            }

            string[] return_args = { currentCOM, fileName };

            // send the arguments back to the ProgramManager
            beginDataCollection?.Invoke(this, return_args);
        }

        private void backgroundDataInit_Shown(object sender, EventArgs e)
        {
            // get the available serial ports
            string[] serialPorts = SerialPort.GetPortNames();

            foreach (string serialPort in serialPorts)
            {
                comPortList.Items.Add(serialPort);
            }
        }
    }
}
