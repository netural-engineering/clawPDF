using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clawSoft.clawPDF.Workflow
{
    public class LogControl
    {
        private static string _Path = string.Empty;
        private static bool WRITE_LOG = true;
        private static bool DEBUG = true;

        public static void Write(string msg)
        {
            if (WRITE_LOG)
            {
                //_Path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                _Path = @"C:\Users\dob\Desktop";

                try
                {
                    using (StreamWriter w = File.AppendText(Path.Combine(_Path, "log.txt")))
                    {
                        Log(msg, w);
                    }
                    if (DEBUG)
                        Console.WriteLine(msg);
                }
                catch (Exception e)
                {
                    //Handle
                }
            }
        }

        static private void Log(string msg, TextWriter w)
        {
            try
            {
                w.Write(Environment.NewLine);
                w.Write("[{0} {1}]", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
                w.Write("\t");
                w.WriteLine(" {0}", msg);
                w.WriteLine("-----------------------");
            }
            catch (Exception e)
            {
                //Handle
            }
        }
    }
}
