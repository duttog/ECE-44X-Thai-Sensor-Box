using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Shapes;

namespace test_project
{
    class TimeStamp
    {
        private DateTime timeValue;
        private List<SensorReadings> sensorValues;
        private int numSensors;


        public TimeStamp(string[] lines)
        {
            /* Construct the DateTime portion of the class */
            int hour, minute, day, month, year;
            
            int comma = lines[0].IndexOf(",");
            int semiColon = lines[0].IndexOf(";");
            int colon = lines[0].IndexOf(":");
            
            hour = Int32.Parse(lines[0].Substring(comma, colon));
            minute = Int32.Parse(lines[0].Substring(colon, semiColon));

            string date_string = lines[0].Substring(0, comma);

            char[] delimiterChars = {',', '-'};
            string[] tokens = date_string.Split(delimiterChars);
            
            day = Int32.Parse(tokens[0]);
            month = Int32.Parse(tokens[1]);
            year = Int32.Parse(tokens[2]);

            timeValue = new DateTime(year, month, day, hour, minute, 0); // accuracy of seconds does not matter


            /* Calculate the number of sensors  */
            numSensors = lines.Length - 1;

            // Might need to check this loop to make sure it operates correctly in edge cases
            for (int i = 1; i < lines.Length; i++)
            {
                if (sensorValues == null)
                {
                    sensorValues = new List<SensorReadings>();
                    sensorValues.Add(new SensorReadings(lines[i]));
                }

                else
                {
                    sensorValues.Add(new SensorReadings(lines[i]));
                }
            }
        }

    }
}
