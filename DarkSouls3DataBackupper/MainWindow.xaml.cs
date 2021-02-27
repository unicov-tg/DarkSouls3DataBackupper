﻿using System;
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
            if (path != "")
            {
                saveDataDirectoryBox.Text = path;
            }
        }
        private void backupDirectoryChangeButton_Click(object sender, RoutedEventArgs e)
        {
            var path = showDirectorySelectDialogAndGetPath();
            if (path != "")
            {
                backUpDirectoryBox.Text = path;
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
            backUpButton.IsEnabled = false;

            var backUp = InitializeDataBackUp();
            Action action = () =>
            {
                try
                {
                    backUp.Save();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            };
            await Task.Run(action);

            UpdateBackUpList();
            backUpFileNameBox.Text = "";
            backUpButton.IsEnabled = true;
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
            try
            {
                var dataRestore = new Models.DataRestore(saveDataDirectoryBox.Text, GetSelectedBackUpFile(), PathUtility.GetTempPath(backUpDirectoryBox.Text));
                dataRestore.Restore();
                MessageBox.Show("セーブデータを復元しました。");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
    }
}
