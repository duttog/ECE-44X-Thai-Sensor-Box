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
        private double humidity;
        private double temperature;
        private double water_level;
        private double wind_speed;
        
        public SensorReadings(double hum, double temp, double wat_lev, double win_spd)
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
            temperature = double.Parse(tokens[1]);
            humidity = double.Parse(tokens[2]);
            water_level = double.Parse(tokens[3]);
            wind_speed = double.Parse(tokens[4]);

        }

        public int getID()
        {
            return this.sensorID;
        }

        public double getHum()
        {
            return this.humidity;
        }

        public double getTemp()
        {
            return this.temperature;
        }

        public double getLevel()
        {
            return this.water_level;
        }

        public double getSpeed()
        {
            return this.wind_speed;
        }




    }
}
