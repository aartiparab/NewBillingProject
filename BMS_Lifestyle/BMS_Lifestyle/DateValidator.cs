using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BMS_Lifestyle
{
    class DateValidator
    {
        string directory = null;

        DateTime fileDate;
        DateTime chkDate;
        TimeSpan diff;

        public int dateValid()
        {
            {

                directory = Directory.GetCurrentDirectory();

                FileInfo objFile = new FileInfo(directory + "\\Dtm.dat");

                chkDate = DateTime.Now;

                if (objFile.Exists)
                {
                    FileStream fs = new FileStream(directory + "\\Dtm.dat", FileMode.Open, FileAccess.ReadWrite);

                    StreamWriter sw = new StreamWriter(fs);

                    StreamReader sr = new StreamReader(fs);

                    string str = sr.ReadToEnd();

                    fileDate = Convert.ToDateTime(str);

                    diff = new TimeSpan();

                    diff = chkDate - fileDate;

                    double d = diff.TotalDays;

                    sw.Flush();
                    fs.Flush();
                    sw.Close();

                    if (d < 0)
                    {
                        //MessageBox.Show("Your system Clock has been set back. Return the clock to the Correct time", "ProAccIn", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return 0;
                    }
                    else
                    {
                        FileStream fs1 = new FileStream(directory + "\\Dtm.dat", FileMode.Create, FileAccess.ReadWrite);

                        StreamWriter sw1 = new StreamWriter(fs1);

                        sw1.Write(DateTime.Now);

                        sw1.Flush();
                        sw1.Close();
                        fs1.Close();

                        return 1;
                    }
                }

                else
                {
                    FileStream fs2 = new FileStream(directory + "\\Dtm.dat", FileMode.Create, FileAccess.ReadWrite);

                    StreamWriter sw2 = new StreamWriter(fs2);

                    sw2.Write(DateTime.Now);

                    sw2.Flush();
                    sw2.Close();
                    fs2.Close();

                    return 1;
                }
            }
        }
    }
}
