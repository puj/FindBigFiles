using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindBigFiles.DataStructures
{
    class UIFileInfo
    {
        public UIFileInfo(string path, ulong size)
        {
            Path = path;
            Size = size;
        }

        public string Path { get; set; }
        public ulong Size { get; set; }
    }
}
