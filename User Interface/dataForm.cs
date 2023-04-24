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
        private dataGraph? sortedData;

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

        private void dataForm_Load(object sender, EventArgs e)
        {

        }

        private void dataForm_Shown(object sender, EventArgs e)
        {
            // get the data from the program manager
            current_data = ProgramManager.getEDF();

            if (current_data != null)
            {
                // sort the data
                sortedData = new dataGraph(current_data.getData());
            }


        }
    }
}
