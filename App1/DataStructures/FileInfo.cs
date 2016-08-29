using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.FileProperties;

namespace FindBigFiles.DataStructures
{
    public class FileInfo : IComparable<FileInfo>
    {
        UIFileInfo uiFileInfo = null;
        StorageFile file;
        ulong size;

        public FileInfo(StorageFile file)
        {
            this.file = file;

            file.GetBasicPropertiesAsync().Completed += new AsyncOperationCompletedHandler<BasicProperties>(SetProperties);
        }

        private void SetProperties(IAsyncOperation<BasicProperties> asyncInfo, AsyncStatus asyncStatue)
        {
            size = asyncInfo.GetResults().Size;
        }


        public UIFileInfo GetUIFileInfo()
        {
            if (uiFileInfo == null)
            {
                uiFileInfo = new UIFileInfo(file.Path, size);
            }
            return uiFileInfo;
        }



        public override string ToString()
        {
            return file.Name + " - " + size;
        }

        public int CompareTo(FileInfo other)
        {
            return (int)(other.size - size);
        }
    }
}
