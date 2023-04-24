using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace test_project
{
    // this class does not alter any data from the "EnvironmentalDataFile"
    // class, but simply rearranges the data as arrays with the timestamps as
    // a common x-axis and the sensor values as arrays with the y-axis.
    //
    // this eases the difficulty of graphing the readings directly because the 
    // graph can accept an array of x vs. y coordinates
    class dataGraph
    {
        private int numIds;
        List<int> allSensIds = new List<int>();

        private List<double> x_coords = new List<double>();
        private double[,] hum_vals;
        private double[,] wtrlvl_vals;
        private double[,] wndspd_vals;
        private double[,] temp_vals;

        public dataGraph(List<TimeStamp> datapoints)
        {
            // method to count the number of unique ids in the file
            this.CountUniqueSensors(datapoints);

            // method to create translate and create the x_coords
            this.CountTimeStamps(datapoints);

            // each list of lists with have the height of number of sensors and the 
            // length of the x-axis... if any specific sensor does not have a value 
            // at a specific time then the dot will be filled with a -1

        }

        private void CountUniqueSensors(List<TimeStamp> datapoints)
        {
            // create a list of all the local sensor Ids
            foreach (TimeStamp datapoint in datapoints)
            {
                int[] new_sensorIds = datapoint.getIds();
                foreach (int sensorId in new_sensorIds)
                {
                    if (!allSensIds.Contains(sensorId))
                    {
                        allSensIds.Add(sensorId);
                    }
                }
            }

            // count the number of elements in the list
            this.numIds = allSensIds.Count();
        }

        private void CountTimeStamps(List<TimeStamp> datapoints)
        {
            foreach (TimeStamp datapoint in datapoints)
            {
                x_coords.Add(datapoint.getDate().ToOADate());
            }
        }

        private void SortData(List<TimeStamp> datapoints)
        {
            int length = x_coords.Count();

            // all of the sensor arrays should be the same length and height
            hum_vals = new double[this.numIds, length];
            wtrlvl_vals = new double[this.numIds, length];
            wndspd_vals = new double[this.numIds, length];
            temp_vals = new double[this.numIds, length];


        }
    }
}
