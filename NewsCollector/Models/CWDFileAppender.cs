using log4net.Appender;
using System.IO;

namespace NewsCollector.Models
{
    class CWDFileAppender : FileAppender
    {
        public override string File
        {
            set
            {
                string cwd = Directory.GetCurrentDirectory();
                string newPath = Path.GetFullPath(Path.Combine(cwd, @"..\"));


                base.File = Path.Combine(newPath, value);
            }
        }
    }
}