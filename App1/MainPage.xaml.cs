using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Diagnostics;
using FindBigFiles.Utils;
using System.Threading.Tasks;
using Windows.Storage.Pickers;
using Windows.Storage;
using FindBigFiles.DataStructures;
using Windows.ApplicationModel.Core;
using System.Collections.ObjectModel;
using Windows.UI.Core;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page, ISortedSetReporter
    {
        public static String path = @"C:/";
        public static int NUM_TO_SHOW = 5;
        ObservableCollection<UIFileInfo> mUIFileInfos = new ObservableCollection<UIFileInfo>();


        public MainPage()
        {
            this.InitializeComponent();
            StartFolderPicker();

            for (int i = 0; i < NUM_TO_SHOW; i++)
            {
                mUIFileInfos.Add(null);
            }
        }

        public void ReportSet(SortedSet<FindBigFiles.DataStructures.FileInfo> sortedSet)
        {
            CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => UpdateUIList(sortedSet));
        }


        public void UpdateUIList(SortedSet<FindBigFiles.DataStructures.FileInfo> sortedSet)
        {
            int i = 0;
            foreach (FindBigFiles.DataStructures.FileInfo f in sortedSet)
            {
                if (i >= mUIFileInfos.Count)
                {
                    break;
                }
                mUIFileInfos[i++] = f.GetUIFileInfo();
            }
        }

        public async void StartFolderPicker()
        {

            try
            {
                FolderPicker picker = new FolderPicker() { SuggestedStartLocation = PickerLocationId.HomeGroup };
                picker.FileTypeFilter.Add("*");
                picker.CommitButtonText = "Select Folder";
                StorageFolder folder = await picker.PickSingleFolderAsync();


                listView.ItemsSource = mUIFileInfos;

                if (folder != null)
                {
                    // Application now has read/write access to all contents in the picked folder
                    // (including other sub-folder contents)
                    Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.AddOrReplace("PickedFolderToken", folder);
                    txtStatus.Text = "current scanning... " + folder.Name;

                    Task.Run(() => DiskUtils.scanDirectory(folder, this));
                }
                else
                {
                    Logger.w("Operation cancelled.");
                }
            }
            catch (Exception e)
            {
                Logger.e("UnknownException: " + e);

            }
        }


    }
}
