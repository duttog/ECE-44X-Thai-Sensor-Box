using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace test_project
{
    class TimeStamp
    {
        private DateTime timeValue;
        private List<SensorReadings>? sensorValues;
        private int numSensors;
        private int[] sensorIds;


        public TimeStamp(string[] lines)
        {
            /* Construct the DateTime portion of the class */
            int hour, minute, day, month, year;

            int comma = lines[0].IndexOf(",");
            int semiColon = lines[0].IndexOf(";");
            int colon = lines[0].IndexOf(":");

            hour = Int32.Parse(lines[0].Substring(comma + 1, colon - (comma + 1)));
            minute = Int32.Parse(lines[0].Substring(colon + 1, semiColon - (colon + 1)));

            string date_string = lines[0].Substring(0, comma);

            char[] delimiterChars = { ',', '-' };
            string[] tokens = date_string.Split(delimiterChars);

            day = Int32.Parse(tokens[0]);
            month = Int32.Parse(tokens[1]);
            year = 2000 + Int32.Parse(tokens[2]);

            timeValue = new DateTime(year, month, day, hour, minute, 0); // accuracy of seconds does not matter


            /* Calculate the number of sensors  */
            numSensors = lines.Length - 1;
            sensorIds = new int[numSensors];

            // Might need to check this loop to make sure it operates correctly in edge cases
            for (int i = 1; i < lines.Length; i++)
            {
                // need to change this for double digit sensor ids
                sensorIds[i - 1] = lines[i][0] - 48; // the -48 is for ascii char to int conversion
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

        public TimeStamp()
        {
            timeValue = DateTime.Now;
            numSensors = 1;
            sensorIds = new int[] {-1};
            sensorValues = new List<SensorReadings>();
            sensorValues.Add(new SensorReadings());
        }

        public DateTime getDate()
        {
            return this.timeValue;
        }

        public List<SensorReadings>? getSensorData()
        {
            return this.sensorValues;
        }

        public int[] getIds()
        {
            return this.sensorIds;
        }

        public SensorReadings? findSensorData(int sensorId)
        {
            foreach (SensorReadings sensorData in sensorValues)
            {
                if (sensorData.getID() == sensorId)
                {
                    return sensorData;
                }
            }

            return null;
        }


        public List<int[]>? checkDifferences(TimeStamp prevData)
        {
            int currId = 0;
            List<int[]> id_type_pair = new List<int[]>();

            for (int i = 0; i < this.numSensors; i++)
            {
                currId = sensorIds[i];
                var prevSensor = prevData.findSensorData(currId);
                var currSensor = this.findSensorData(currId);

                // the current sensor ID exists in both time stamps
                if (prevSensor != null && currSensor != null)
                {
                    if (currSensor.getTemp() > warningInformation.getMaxTemp())
                    {
                        // temperature got over 50 degrees
                        id_type_pair.Add(new int[2] { currId, 1 });
                    }

                    else if (currSensor.getTemp() - prevSensor.getTemp() > warningInformation.getDeltaTemp() || currSensor.getTemp() - prevSensor.getTemp() < (-1 * warningInformation.getDeltaTemp()))
                    {
                        // temperature has changed more than five degrees
                        id_type_pair.Add(new int[2] { currId, 2 });
                    }

                    else if (currSensor.getHum() - prevSensor.getHum() > warningInformation.getDeltaHum() || currSensor.getTemp() - prevSensor.getTemp() < (-1 * warningInformation.getDeltaHum()))
                    {
                        // temperature has changed more than five degrees
                        id_type_pair.Add(new int[2] { currId, 3 });
                    }
                }
            }

            if (id_type_pair.Count > 0)
            {
                return id_type_pair;
            }

            else
            {
                return null;
            }
        }

    }
}
