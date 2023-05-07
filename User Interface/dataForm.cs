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
        private List<warningInformation> warningList = new List<warningInformation>();

        public event EventHandler RefreshDisplay;
        public event EventHandler NewFile;
        public event EventHandler BackgroundBegin;

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

            // generate the warnings from the graph
            this.generateWarnings();

            // place the warnings on the warning list
            this.placeWarnings();
        }

        private void generateWarnings()
        {
            // 
            var timeList = current_data.getData();

            TimeStamp point1, point2;

            for (int i = 1; i < timeList.Count; i++)
            {
                // get both points
                point1 = timeList[i - 1];
                point2 = timeList[i];

                // check for any differences
                var warnings = point2.checkDifferences(point1);
                if (warnings != null)
                {
                    foreach (int[] warningPoint in warnings)
                    {
                        warningList.Add(new warningInformation(point1, point2, warningPoint[1], warningPoint[0]));
                    }
                }
            }
        }

        private void placeWarnings()
        {
            // clear any current warnings
            warningListIcon.Items.Clear();

            // add the most recent discovered timestamps
            foreach (var warning in warningList)
            {
                warningListIcon.Items.Add(warning.ConstructWarningIcon(), 2);
            }
        }

        private void warningListIcon_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            warningInformation? actualWarning = null;

            // to use this information, i need to actually find the same warningInformation struct
            foreach (var warning in warningList)
            {
                if (warning.getWarningMessage() != null)
                {
                    if (warning.getWarningMessage().Equals(e.Item.Text))
                    {
                        actualWarning = warning;
                        break;
                    }
                }
            }

            if (actualWarning != null)
            {
                warningInformation.Text = WriteToWarningBox(actualWarning);
            }

            else
            {
                warningInformation.Text = "ERROR";
            }

        }

        private string WriteToWarningBox(warningInformation warning)
        {
            var textbox = new StringBuilder();

            /*
             * Part 1: (line not included)
             * DateTime String
             * Sensor ID: {}
             * Warning Type: {}
             * 
             * Part 2: (line not included)
             * Previous Sensor Readings:
             * Temperature = {}
             * Humidity = {}
             * Wind Speed = {}
             * Water Level = {}
             * 
             * Part 3: (line not included)
             * Current Sensor Readings:
             * Temperature = {}
             * Humidity = {}
             * Wind Speed = {}
             * Water Level = {}
             * 
             * Part 4: (line not included)
             * Change in Readings:
             * Temperature = {}
             * Humidity = {}
             * Wind Speed = {}
             * Water Level = {}
             */



            // Beginning of Part 1
            textbox.Append(warning.getTime() + "\n");
            textbox.Append("Sensor ID: " + warning.getID().ToString() + "\n");

            switch (warning.getWarningType())
            {
                case 1:
                    textbox.Append("Warning Type: Maximum Temperature Exceeded!\n");
                    break;

                case 2:
                    textbox.Append("Warning Type: Change in Temperature Exceeded!\n");
                    break;

                case 3:
                    textbox.Append("Warning Type: Change in Humidity Exceeded!\n");
                    break;

                default:
                    textbox.Append("Warning Type: Unregistered Error Type!\n");
                    break;
            }
            // End of Part 1



            // Beginning of Part 2
            textbox.Append("\nPrevious Sensor Readings: \n");
            textbox.Append("Temperature: " + warning.getReadings()[0].findSensorData(warning.getID()).getTemp().ToString() + "\n");
            textbox.Append("Humidity: " + warning.getReadings()[0].findSensorData(warning.getID()).getHum().ToString() + "\n");
            textbox.Append("Wind Speed: " + warning.getReadings()[0].findSensorData(warning.getID()).getSpeed().ToString() + "\n");
            textbox.Append("Water Level: " + warning.getReadings()[0].findSensorData(warning.getID()).getLevel().ToString() + "\n");
            // End of Part 2



            // Beginning of Part 3
            textbox.Append("\nCurrent Sensor Readings: \n");
            textbox.Append("Temperature: " + warning.getReadings()[1].findSensorData(warning.getID()).getTemp().ToString() + "\n");
            textbox.Append("Humidity: " + warning.getReadings()[1].findSensorData(warning.getID()).getHum().ToString() + "\n");
            textbox.Append("Wind Speed: " + warning.getReadings()[1].findSensorData(warning.getID()).getSpeed().ToString() + "\n");
            textbox.Append("Water Level: " + warning.getReadings()[1].findSensorData(warning.getID()).getLevel().ToString() + "\n");
            // End of Part 3



            // Beginning of Part 4
            textbox.Append("\nChange in Readings: \n");
            textbox.Append("Temperature: " + (warning.getReadings()[1].findSensorData(warning.getID()).getTemp() - warning.getReadings()[0].findSensorData(warning.getID()).getTemp()).ToString("F2") + "\n");
            textbox.Append("Humidity: " + (warning.getReadings()[1].findSensorData(warning.getID()).getHum() - warning.getReadings()[0].findSensorData(warning.getID()).getHum()).ToString("F2") + "\n");
            textbox.Append("Wind Speed: " + (warning.getReadings()[1].findSensorData(warning.getID()).getSpeed() - warning.getReadings()[0].findSensorData(warning.getID()).getSpeed()).ToString("F2") + "\n");
            textbox.Append("Water Level: " + (warning.getReadings()[1].findSensorData(warning.getID()).getLevel() - warning.getReadings()[0].findSensorData(warning.getID()).getLevel()).ToString("F2") + "\n");
            // End of Part 4

            return textbox.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        private void refreshButton_Click(object sender, EventArgs e)
        {
            RefreshDisplay?.Invoke(this, EventArgs.Empty);
        }

        private void newFileButton_Click(object sender, EventArgs e)
        {
            NewFile?.Invoke(this, EventArgs.Empty);
        }

        private void backgroundDataButton_Click(object sender, EventArgs e)
        {
            BackgroundBegin?.Invoke(this, EventArgs.Empty);
        }
    }
}
