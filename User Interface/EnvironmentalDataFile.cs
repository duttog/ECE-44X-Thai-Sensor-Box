using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_project
{
    class EnvironmentalDataFile
    {
        public List<TimeStamp> dataPoints;

        public EnvironmentalDataFile(string FilePath)
        {
            // the program needs to open the file at the designated location,
            // which is given by the FilePath string.

            // the file consists of a list of data points which have a timestamp
            // and each sensor box identification / environmental value
            // each timestamp has to be seperated from the other ones, which have a 
            // single whitespace line
            dataPoints = new List<TimeStamp>();

            string[][] fileData = seperateTimeStamps(FilePath);

            foreach (string[] dataPoint in fileData)
            {
                dataPoints.Add(new TimeStamp(dataPoint));
            }
        }

        private string[][] seperateTimeStamps(string FileName)
        {
            List<string> newTimeStamp = new List<string>();
            List<string[]> envDataFile = new List<string[]>();
            string[] tempHolder;
            var file = new StreamReader(new FileStream(FileName, FileMode.Open));
            
            string? newLine;

            // while not at end of file
            do
            {
                // while entire string is not a newline character
                do
                {
                    newLine = file.ReadLine();

                    if (newLine != null)
                    {
                        newTimeStamp.Add(newLine);
                    }
                } while (newLine != "\n" || newLine != null);

                tempHolder = newTimeStamp.ToArray();
                envDataFile.Add(tempHolder);
                newTimeStamp.Clear();

            } while (newLine != null);

            return envDataFile.ToArray();
        }
    }
}
