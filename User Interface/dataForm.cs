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
using ScottPlot;

namespace test_project
{
    public partial class dataForm : Form
    {
        private EnvironmentalDataFile? current_data = null;
        private List<int> sensorId = new List<int>();
        private dataGraph? sortedData;

        /// <summary>
        /// This is the constructor for the form. This happens during the program initialization stage,
        /// so the data itself is not available. When the form itself is shown the data will be plotted,
        /// but during the constructor only the axis and labels will be constructed.
        /// </summary>
        public dataForm()
        {
            InitializeComponent();

        }

        /// <summary>
        /// Adds the labels to each graph including the axis titles and 
        /// graph name.
        /// </summary>
        private void InitGraphs()
        {
            tempGraph.Plot.XAxis.Label("Received Time");
            tempGraph.Plot.YAxis.Label("Temperature [Celsius]");
            tempGraph.Plot.Title("Temperature Graph");
            tempGraph.Plot.XAxis.DateTimeFormat(true);


            tempGraph.Refresh();

            humGraph.Plot.XAxis.Label("Received Time");
            humGraph.Plot.YAxis.Label("Relative Humidity [%]");
            humGraph.Plot.Title("Humidity Graph");
            humGraph.Plot.XAxis.DateTimeFormat(true);

            humGraph.Refresh();

            wndspdGraph.Plot.XAxis.Label("Received Time");
            wndspdGraph.Plot.YAxis.Label("Wind Speed [mph]");
            wndspdGraph.Plot.Title("Wind Speed Graph");
            wndspdGraph.Plot.XAxis.DateTimeFormat(true);

            wndspdGraph.Refresh();

            wtrlvlGraph.Plot.XAxis.Label("Received Time");
            wtrlvlGraph.Plot.YAxis.Label("Water Level [Inches]");
            wtrlvlGraph.Plot.Title("Water Level Graph");
            wtrlvlGraph.Plot.XAxis.DateTimeFormat(true);

            wtrlvlGraph.Refresh();
        }

        private void AddData()
        {
            var humLegend = humGraph.Plot.Legend(true);
            var tempLegend = tempGraph.Plot.Legend(true);
            var speedLegend = wndspdGraph.Plot.Legend(true);
            var levelLegend = wtrlvlGraph.Plot.Legend(true);

            // sorted data is an object of type "dataGraph"
            double[] time_values = sortedData.getTime();

            int num_sens = sortedData.getSensIds().Count();
            // loop through each sensor and get the four data values
            for (int i = 0; i < num_sens; i++)
            {
                // get the specific sensor id being graphed
                int sensId = sortedData.getSensIds()[i];
                string label = "Sensor " + sensId.ToString();

                double[] hum = sortedData.getHum(sensId);
                double[] speed = sortedData.getSpeed(sensId);
                double[] level = sortedData.getLevel(sensId);
                double[] temp = sortedData.getTemp(sensId);

                var temp_plot = tempGraph.Plot.AddScatter(time_values, temp, label: label);
                var speed_plot = wndspdGraph.Plot.AddScatter(time_values, speed, label: label);
                var level_plot = wtrlvlGraph.Plot.AddScatter(time_values, level, label: label);
                var hum_plot = humGraph.Plot.AddScatter(time_values, hum, label: label);

            }

            tempGraph.Plot.AxisAutoX();
            wndspdGraph.Plot.AxisAutoX();
            wtrlvlGraph.Plot.AxisAutoX();
            humGraph.Plot.AxisAutoX();



            wndspdGraph.Refresh();
            wtrlvlGraph.Refresh();
            humGraph.Refresh();
            tempGraph.Refresh();
        }


        /// <summary>
        /// If a valid file is chosen, the program manager will invoke this event when
        /// switching which form is shown.
        /// </summary>
        private void dataForm_Shown(object sender, EventArgs e)
        {
            // get the data from the program manager
            current_data = ProgramManager.getEDF();

            if (current_data != null)
            {
                // sort the data
                sortedData = new dataGraph(current_data.getData());

                if (sortedData == null)
                {
                    throw new Exception();
                }
            }

            // add the titles and labels to the graphs
            this.InitGraphs();

            // add the signals to the graph
            this.AddData();


        }
    }
}
