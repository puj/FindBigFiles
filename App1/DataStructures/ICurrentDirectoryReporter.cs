using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindBigFiles.DataStructures
{
    interface ICurrentDirectoryReporter
    {
        void ReportCurrentDirectory(string currentDirectoryName);
    }
}
