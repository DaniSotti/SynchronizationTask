using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLine
{
    public class Logger
    {
        public string CurrentDirectory
        {
            get;
            set;
        }

        public string FileName
        {
            get;
            set;
        }

        public string FilePath
        {
            get;
            set;
        }
        
        public Logger()
        {
            CurrentDirectory = Directory.GetCurrentDirectory();
            FileName = "Log.txt";
            FilePath = Path.Combine(CurrentDirectory, FileName);
        }

        public void WriteLog(string message)
        {
            try
            {
                using StreamWriter write = new StreamWriter(FilePath, true);
                write.WriteLine("\r\nLog Entry: ");
                write.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
                write.WriteLine(" :{0}", message);
                write.WriteLine("------------------------------------------");
               
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to log file: {ex.Message}");
            }
        }
    }
}
