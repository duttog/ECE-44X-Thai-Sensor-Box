using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_project
{
    class EnvironmentalDataFile
    {
        private List<TimeStamp> dataPoints;
        public List<TimeStamp> getData()
        {
            return dataPoints;
        }


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

            if (fileData == null || fileData.Length == 0)
            {
                // this creates a blank timestamp of 0 
                dataPoints.Add(new TimeStamp());
            }

            else
            {
                foreach (string[] dataPoint in fileData)
                {
                    dataPoints.Add(new TimeStamp(dataPoint));
                }
            }
        }

        private string[][]? seperateTimeStamps(string FileName)
        {
            List<string> newTimeStamp = new List<string>();
            List<string[]> envDataFile = new List<string[]>();
            string[] tempHolder;

            // in the case that we havent read any data yet, we need to open a blank window
            try
            {
                var file = new StreamReader(new FileStream(FileName, FileMode.Open));
                // while not at end of file

                string? newLine;

                do
                {
                    // while entire string is not a newline character
                    do
                    {
                        newLine = file.ReadLine();

                        if (newLine == null)
                        {
                            break;
                        }

                        if (!newLine.Equals(null) && !newLine.Equals(""))
                        {
                            newTimeStamp.Add(newLine);
                        }
                    } while (!newLine.Equals(null) && !newLine.Equals(""));

                    tempHolder = newTimeStamp.ToArray();
                    envDataFile.Add(tempHolder);
                    newTimeStamp.Clear();

                } while (file.Peek() >= 0);

                file.Close();

                if (envDataFile.Count > 1)
                {
                    return envDataFile.ToArray();
                }

                else
                {
                    return null;
                }
            }

            catch (IOException ex)
            {
                return null;
            }


            
        }


        
    }
}
