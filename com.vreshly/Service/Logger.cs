using System;
namespace com.vreshly.Service
{
    public class Logger : ILogger
    {
        public Logger()
        {
        }

        private int LogFileSize = 5;
        private string LogBasePath = AppDomain.CurrentDomain.BaseDirectory + @"/Logs/";
        private readonly object _lock = new object();
        private readonly object _infolock = new object();
        public void Error(Exception ex)
        {
            lock (_lock)
            {
                if (System.IO.File.Exists($"{LogBasePath}error_log.txt"))
                {
                    System.IO.FileInfo t = new System.IO.FileInfo($"{LogBasePath}error_log.txt");
                    if (t.Length > LogFileSize * 1024 * 1024)
                    {
                        t.MoveTo($"{LogBasePath}error_log_{DateTime.Now.ToString("ddMMyyyyHHmmss")}.txt");
                    }
                }
                
                var logDetails = $"An error occurred Exception Message : {ex.Message } with stack trace : {ex.StackTrace} and Inner Message : {ex.InnerException}";
                System.IO.File.AppendAllText($"{LogBasePath}error_log.txt", DateTime.Now.ToString() + " " + logDetails + Environment.NewLine);
            }

        }

        public void Info(string info)
        {
            lock (_infolock)
            {
                if (System.IO.File.Exists($"{LogBasePath}info_log.txt"))
                {
                    System.IO.FileInfo t = new System.IO.FileInfo($"{LogBasePath}info_log.txt");
                    if (t.Length > LogFileSize * 1024 * 1024)
                    {
                        t.MoveTo($"{LogBasePath}info_log_{DateTime.Now.ToString("ddMMyyyyHHmmss")}.txt");
                    }
                }
                System.IO.File.AppendAllText($"{LogBasePath}info_log.txt", DateTime.Now.ToString() + " " + info + Environment.NewLine);

            }

        }

    }
}
