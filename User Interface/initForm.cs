using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test_project
{
    public partial class initForm : Form
    {
        public event EventHandler BeginBackgroundCapture;
        public event EventHandler DisplayData;

        private Action result;
        public Action getAction()
        {
            return result;
        }


        public initForm()
        {
            InitializeComponent();
        }

        private void initForm_Load(object sender, EventArgs e)
        {

        }

        // the only thing that should happen when either button is clicked is
        // an event should be invoked to be handled by the program manager
        private void displayEnvDataButton_Click(object sender, EventArgs e)
        {
            DisplayData?.Invoke(this, EventArgs.Empty);
        }

        private void beginDataCaptureButton_Click(object sender, EventArgs e)
        {

            BeginBackgroundCapture?.Invoke(this, EventArgs.Empty);
        }
    }
}
