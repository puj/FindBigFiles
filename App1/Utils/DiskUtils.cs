using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FindBigFiles.DataStructures;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.FileProperties;
using FindBigFiles.DataStructures;
using Windows.UI.Xaml.Controls;
using Windows.UI.Core;
using Windows.ApplicationModel.Core;

namespace FindBigFiles.Utils
{
    class DiskUtils
    {

        static ListView listView;

        public async static void scanDirectory(StorageFolder directory, ListView listview)
        {
            listView = listview;
            await getFilesForDirectory(directory);
            await getDirectoriesForDirectory(directory);
        }

        public async static Task<List<FileInfo>> getFilesForDirectory(StorageFolder directory)
        {

            IReadOnlyList<StorageFile> files = await directory.GetFilesAsync();
            List<FileInfo> fileInfos = new List<FileInfo>();

            foreach (StorageFile file in files)
            {
                FileInfo fileInfo = new FileInfo(file);
                CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => listView.Items.Add(fileInfo.GetUIFileInfo()));
                fileInfos.Add(fileInfo);

            }

            return fileInfos;
        }

        public async static Task<FileInfo[]> getDirectoriesForDirectory(StorageFolder directory)
        {

            IReadOnlyList<StorageFolder> directories = await directory.GetFoldersAsync();
            return null;
        }



    }
}
