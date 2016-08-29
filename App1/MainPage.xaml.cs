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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static String path = @"C:/";

        public MainPage()
        {
            this.InitializeComponent();
            StartFolderPicker();
        }

        public async void StartFolderPicker()
        {

            try
            {
                FolderPicker picker = new FolderPicker() { SuggestedStartLocation = PickerLocationId.HomeGroup};
                picker.FileTypeFilter.Add("*");
                picker.CommitButtonText = "Select Folder";
                StorageFolder folder = await picker.PickSingleFolderAsync();

                if (folder != null)
                {
                    // Application now has read/write access to all contents in the picked folder
                    // (including other sub-folder contents)
                    Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.AddOrReplace("PickedFolderToken", folder);
                    txtStatus.Text = "current scanning... " + folder.Name;

                    Task.Run(() => DiskUtils.scanDirectory(folder, listView));
                }
                else
                {
                    Logger.w("Operation cancelled.");
                }
            }catch(Exception e)
            {
                    Logger.e("UnknownException: " + e);

            }
        }

    }
}
