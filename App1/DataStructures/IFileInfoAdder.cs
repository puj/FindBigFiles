using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindBigFiles.DataStructures
{
    interface ISortedSetReporter
    {
        void ReportSet(SortedSet<FileInfo> sortedSet);
    }
}
