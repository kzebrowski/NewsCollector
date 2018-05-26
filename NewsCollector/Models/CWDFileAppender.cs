using log4net.Appender;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace NewsCollector.Models
{
    class CWDFileAppender : FileAppender
    {
        public override string File
        {
            set
            {
                string cwd = Directory.GetCurrentDirectory();
                string newPath = Path.GetFullPath(Path.Combine(cwd, @"..\..\..\"));

                base.File = Path.Combine(newPath, value);
            }
        }
    }
}