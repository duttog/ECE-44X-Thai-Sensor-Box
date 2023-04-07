using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_project
{
    class SensorReadings
    {
        private int sensorID;
        private float humidity { get; set; }
        private float temperature { get; set; }
        private float water_level { get; set; }
        private float wind_speed { get; set; }
        
        public SensorReadings(float hum, float temp, float wat_lev, float win_spd)
        {
            this.humidity = hum;
            this.temperature = temp;
            this.water_level = wat_lev; 
            this.wind_speed = win_spd;
        }

        public SensorReadings(string sensorData)
        {
            char[] delimChars = { ',', ';' };
            string[] tokens = sensorData.Split(delimChars);

            sensorID = int.Parse(tokens[0]);
            humidity = float.Parse(tokens[1]);
            temperature = float.Parse(tokens[2]);
            water_level = float.Parse(tokens[3]);
            wind_speed = float.Parse(tokens[4]);

        }
    }
}
