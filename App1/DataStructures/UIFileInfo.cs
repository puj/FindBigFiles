using FindBigFiles.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindBigFiles.DataStructures
{
    public class UIFileInfo
    {

        public UIFileInfo(string path, string size)
        {
            Path = path;
            Size = size;
        }

        public UIFileInfo(string path, ulong size)
        {
            Path = path;
            Size = DiskUtils.showHumanReadbleFromBytes(size);
        }

        public string Path { get; set; }
        public string Size { get; set; }
    }
}
