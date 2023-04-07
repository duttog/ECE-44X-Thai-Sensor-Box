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

        private void displayEnvDataButton_Click(object sender, EventArgs e)
        {
            result = Action.BeginFileExplorer;
            this.DialogResult = DialogResult.OK;
        }

        private void beginDataCaptureButton_Click(object sender, EventArgs e)
        {
            result = Action.BeginBackgroundProcess;
            this.DialogResult = DialogResult.OK;
        }
    }
}
