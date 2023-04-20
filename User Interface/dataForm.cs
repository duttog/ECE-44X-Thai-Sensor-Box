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
        private List<int> sensorId = new List<int>();
        private ScottPlot.Plot? humidity;
        private ScottPlot.Plot? temperature;
        private ScottPlot.Plot? windSpeed;
        private ScottPlot.Plot? waterLevel;

        /// <summary>
        /// This is the constructor for the form. This happens during the program initialization stage,
        /// so the data itself is not available. When the form itself is shown the data will be plotted,
        /// but during the constructor only the axis and labels will be constructed.
        /// </summary>
        public dataForm()
        {
            InitializeComponent();

         
            temperature = new ScottPlot.Plot(900, 900);
            windSpeed = new ScottPlot.Plot(900, 900);
            waterLevel = new ScottPlot.Plot(900, 900);

            // label the x-axis as time

            temperature.XLabel("Received Time");
            windSpeed.XLabel("Received Time");
            waterLevel.XLabel("Received Time");

            // label the respective y-axis

            temperature.YLabel("Temperature [Celsius]");
            windSpeed.YLabel("Wind Speed []");
            waterLevel.YLabel("Water Level [Inches]");

            // add titles to each of the graphs

            temperature.Title("Temperature Graph");
            windSpeed.Title("Wind Speed Graph");
            waterLevel.Title("Water Level Graph");

            this.Refresh();
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

                    foreach (SensorReadings sensor in sensor_data)
                    {
                        int sensID = sensor.getID();

                        // this is a new sensor that is giving a reading
                        if (!sensorId.Contains(sensID))
                        {
                            sensorId.Add(sensID);
                        }

                        // the sensor already exists
                        else
                        {
                            // 
                        }
                    }


                }
            }

            else
            {
                // shouldn't happen
            }
        }


        private void GraphSensor()
        {

        }



        private void tempGraph_Load(object sender, EventArgs e)
        {

        }

        private void humGraph_Load(object sender, EventArgs e)
        {
            // create the plot objects
            humidity = new ScottPlot.Plot(900, 900);
            humidity.XAxis.Label("Received Time");
            humidity.YAxis.Label("Relative Humidity [%]");
            humidity.Title("Relative Humidity Graph");


        }

        private void wndspdGraph_Load(object sender, EventArgs e)
        {

        }

        private void wtrlvlGraph_Load(object sender, EventArgs e)
        {

        }

        private void InitGraphs(object sender, EventArgs e)
        {
            // get the data from the program manager
            current_data = ProgramManager.getEDF();

            // 
        }

        private void dataForm_Load(object sender, EventArgs e)
        {

        }
    }
}
