using System;
using System.Windows;

namespace DarkSouls3DataBackupper
{
    using Libs;
    using System.Diagnostics;
    using System.Threading.Tasks;

    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private Models.BackUpFileList BackUpFileList;

        public MainWindow()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            SetDS3SaveDataPath();
            SetBackUpDataPath();
            SetBackUpFileList();
        }

        private void SetDS3SaveDataPath()
        {
            saveDataDirectoryBox.Text = PathUtility.GetDS3SaveDataPath();
        }

        private void SetBackUpDataPath()
        {
            backUpDirectoryBox.Text = System.IO.Path.Combine(PathUtility.GetDS3SaveDataPath(), "backups");
        }

        private void SetBackUpFileList()
        {
            // TODO: backupDirectoryBoxに入れてるパスをクラス側で持つ
            BackUpFileList = new Models.BackUpFileList(backUpDirectoryBox.Text);
            backUpFileListView.ItemsSource = BackUpFileList.List;
        }

        private void saveDataDirectoryChangeButton_Click(object sender, RoutedEventArgs e)
        {
            var path = showDirectorySelectDialogAndGetPath();
            if (path == "")
            {
                return;
            }

            if (PathUtility.CheckDirectoryContainsTargetFiles(path))
            {
                saveDataDirectoryBox.Text = path;
            }
            else
            {
                MessageBox.Show("指定されたフォルダにセーブデータが存在しません。");
            }
        }

        private void backupDirectoryChangeButton_Click(object sender, RoutedEventArgs e)
        {
            var path = showDirectorySelectDialogAndGetPath();
            if (path != "")
            {
                backUpDirectoryBox.Text = path;
                BackUpFileList.ChangeDiretory(path);
            }
        }

        private string showDirectorySelectDialogAndGetPath()
        {
            var dialog = new System.Windows.Forms.OpenFileDialog() { FileName = "SelectFolder", Filter = "Folder|.", CheckFileExists = false };
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                return System.IO.Path.GetDirectoryName(dialog.FileName);
            }
            return "";
        }

        private async void backUpButton_Click(object sender, RoutedEventArgs e)
        {
            DisableActions();

            try
            {
                var backUp = InitializeDataBackUp();
                Func<bool> action = () =>
                {
                    try
                    {
                        backUp.Save();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    return false;
                };
                if(await Task.Run(action))
                {
                    UpdateBackUpList();
                    backUpFileNameBox.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                EnableActions();
            }
        }

        private DataBackUp InitializeDataBackUp()
        {
            var saveDataPath = saveDataDirectoryBox.Text;
            var backUpPath = backUpDirectoryBox.Text;
            var backUpFilename = backUpFileNameBox.Text;

            if (backUpFilename == "")
            {
                var now = DateTime.Now;
                backUpFilename = now.ToString("yyyyMMddHHmmss");
            }

            return new DataBackUp(saveDataPath, backUpPath, backUpFilename);
        }

        private void UpdateBackUpList()
        {
            BackUpFileList.UpdateBackUpFileList();
        }

        private void restoreButton_Click(object sender, RoutedEventArgs e)
        {
            if (backUpFileListView.SelectedIndex < 0)
            {
                return;
            }

            try
            {
                var backUpCurrentData = backUpCurrentDataCheckBox.IsChecked ?? false;
                if (backUpCurrentData)
                {
                    InitializeDataBackUp().Save();
                    backUpFileNameBox.Text = "";
                }
                var dataRestore = new Models.DataRestore(saveDataDirectoryBox.Text, GetSelectedBackUpFile(), PathUtility.GetTempPath(backUpDirectoryBox.Text));
                dataRestore.Restore();
                MessageBox.Show("セーブデータを復元しました。");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            UpdateBackUpList();
        }

        private string GetSelectedBackUpFile()
        {
            var index = backUpFileListView.SelectedIndex;
            var selectedFile = BackUpFileList.List[index];
            return selectedFile.FilePath;
        }

        private void openSaveDataPathButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start("explorer.exe", saveDataDirectoryBox.Text);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void openBackUpPathButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start("explorer.exe", backUpDirectoryBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DisableActions()
        {
            backUpButton.IsEnabled = false;
            restoreButton.IsEnabled = false;
        }

        private void EnableActions()
        {
            backUpButton.IsEnabled = true;
            restoreButton.IsEnabled = true;
        }
    }
}
