using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace test_project
{
    class warningInformation
    {
        private static int MaxTemp = 50;
        private static int DeltaTemp = 5;
        private static double DeltaHum = 10;

        public static int getMaxTemp()
        {
            return MaxTemp;
        }

        public static int getDeltaTemp()
        {
            return DeltaTemp;
        }

        public static double getDeltaHum()
        {
            return DeltaHum;
        }


        private TimeStamp initial_reading;
        private TimeStamp final_reading;
        private int warning_type;
        private int sensId;

        /*
         * List of warning types:
         * 1. Temperature over 50 degrees
         * 2. Temperature change of 5 degrees
         * 3. Change in humidity of 10%
         */

        private int warningType;

        public warningInformation(TimeStamp point1, TimeStamp point2, int type, int sensId)
        {
            // copy the two points
            this.initial_reading = point1;
            this.final_reading = point2;
            this.warning_type = type;
            this.sensId = sensId;
        }


        public string getTime()
        {
            return final_reading.ToString();
        }

        public int getWarningType() {
            return warning_type;
        }

        public TimeStamp[] getReadings()
        {
            return new TimeStamp[] {initial_reading, final_reading};
        }

        public string ConstructWarningIcon()
        {
            var icon = new StringBuilder();

            icon.Append(this.final_reading.getDate().ToString());
            icon.Append(" Sensor ID: ");
            icon.Append(this.sensId.ToString());
            icon.Append(" Warning Type: ");
            
            switch (warning_type)
            {
                case 1:
                    icon.Append("Max Temperature Exceeded!");
                    break;


                case 2:
                    icon.Append("Change in Temperature Exceeded!");
                    break;

                case 3:
                    icon.Append("Change in Humidity Exceeded!");
                    break;

                default:
                    icon.Append("Unrecognized Warning Type!");
                    break;

            }

            return icon.ToString();
        }

    }
}
