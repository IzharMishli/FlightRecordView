using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace plane.Scripts
{
    public class CSVHandle
    {
        public Form1 mainForm;
        public CSVHandle(Form1 form)
        {
            this.mainForm = form;
        }

        public void ReadCsv(string file)
        {
            string[] titles = new string[9];
            try
            {
                using (var reader = new StreamReader(@file))
                {
                    int i = 0;
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        var values = line.Split(',');
                        IEnumerable<String> items;
                        if (i == 0)
                        {
                            titles = values;
                            items = titles.Select(a => (String)Enum.Parse(typeof(String), a));

                        }
                        if (i > 0)
                        {
                            DataManager.Data2.Add(Convert.ToInt32(values[0]), new DataPiece(values[0], values[1], values[2], values[3], values[4], values[5]));
                            List<Dictionary<string, float>> fieldsVal = new List<Dictionary<string, float>>();
                        }
                        i++;
                    }
                }
                mainForm.enableStartBtn();
            }
            catch (Exception e)
            {
                Console.WriteLine("error: {0} ", e.ToString());
            }

        }
    }

    
}

public class DataPiece
{
    public int timeStamp, altitude, gpsTime, errors;
    public double lat, lon;

    public DataPiece(string timeStamp, string altitude, string gpsTime, string lat, string lon, string errors)
    {
        this.timeStamp = Convert.ToInt32(timeStamp);
        this.altitude = Convert.ToInt32(altitude);
        this.gpsTime = Convert.ToInt32(gpsTime);
        this.errors = Convert.ToInt32(errors);
        this.lat = Convert.ToDouble(lat);
        this.lon = Convert.ToDouble(lon);

    }
}