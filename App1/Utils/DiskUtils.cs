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


        static ISortedSetReporter sortedSetReporter;

        internal static string showHumanReadbleFromBytes(ulong size)
        {
            int index = 0;
            string[] abbrev = new String[] {"bytes", "k","m","g","t" };
            ulong units = size;
            double remainder = 0;
            while(units > 1024)
            {
                units = (ulong)Math.Floor(1.0* units / 1024);
                size = size - ((1024)*units);
                index++;
            }
            remainder = size / 1024.0;

            return units + abbrev[index];
        }

        public async static void scanDirectory(StorageFolder directory, ISortedSetReporter sortedSetReporter)
        {
            LinkedList<StorageFolder> queue = new LinkedList<StorageFolder>();
            SortedSet<FileInfo> mSortedFileInfos = new SortedSet<FileInfo>();
            DiskUtils.sortedSetReporter = sortedSetReporter;

            queue.AddLast(directory);

            StorageFolder curr = null;
            while (queue.Count > 0)
            {
                curr = queue.First.Value;
                queue.RemoveFirst();

                List<FileInfo> fileInfos = await getFilesForDirectory(curr);
                IEnumerable<StorageFolder> directories = await getDirectoriesForDirectory(curr);
                foreach(StorageFolder folder in directories)
                {
                    queue.AddLast(folder);
                }

                foreach (FileInfo f in fileInfos)
                {
                    mSortedFileInfos.Add(f);
                }
                sortedSetReporter.ReportSet(mSortedFileInfos);
            }
        }

        public async static Task<List<FileInfo>> getFilesForDirectory(StorageFolder directory)
        {

            IReadOnlyList<StorageFile> files = await directory.GetFilesAsync();
            List<FileInfo> fileInfos = new List<FileInfo>();

            foreach (StorageFile file in files)
            {
                FileInfo fileInfo = new FileInfo(file);
                fileInfos.Add(fileInfo);
            }

            return fileInfos;
        }

        public async static Task<IEnumerable<StorageFolder>> getDirectoriesForDirectory(StorageFolder directory)
        {
            return await directory.GetFoldersAsync();
        }



    }
}
