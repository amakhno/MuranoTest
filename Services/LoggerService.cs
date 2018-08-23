using Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class LoggerService : ILoggerService
    {
        private readonly string _pathToFile;

        public LoggerService()
        {
            _pathToFile = ConfigurationManager.AppSettings["PathToLog"];
            _pathToFile = System.IO.Path.GetFullPath(_pathToFile);
            if (!System.IO.File.Exists(_pathToFile))
            {
                try
                {
                    System.IO.File.Create(_pathToFile);
                }
                catch
                {
                    _pathToFile = null;
                }
                
            }
        }

        public void Write(string actionName)
        {
            if (string.IsNullOrEmpty(_pathToFile))
            {
                //На случай если нет доступа к файлам, для удобства отладки закомментирую чтобы catch не тормозил процесс
                /*try
                {
                    using (var writer = System.IO.File.AppendText(_pathToFile))
                    {
                        writer.WriteLine($"{DateTime.UtcNow}: {actionName}\n");
                    }
                }
                catch
                {
                    throw new Exception("Error access to log file!");
                }*/
            }            
        }
    }
}
