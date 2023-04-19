using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Forms;

namespace test_project
{
    public partial class dataForm : Form
    {
        private EnvironmentalDataFile? current_data = null;

        public dataForm()
        {
            InitializeComponent();

            // get the data from the program manager
            current_data = ProgramManager.getEDF();

            // load the data

        }

        private void GraphData()
        {
            double x_val = 0;
            List<SensorReadings>? sensor_data = null;


            if (current_data != null)
            {

                foreach (TimeStamp data_point in current_data.getData())
                {
                    x_val = data_point.getDate().ToOADate();
                    sensor_data = data_point.getSensorData();
                    


                }
            }

            else
            {
                // shouldn't happen
            }
        }




        private void tempGraph_Load(object sender, EventArgs e)
        {

        }

        private void humGraph_Load(object sender, EventArgs e)
        {

        }

        private void wndspdGraph_Load(object sender, EventArgs e)
        {

        }

        private void wtrlvlGraph_Load(object sender, EventArgs e)
        {

        }
    }
}
