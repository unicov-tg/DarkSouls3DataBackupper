using System;
using System.Windows;

namespace DarkSouls3DataBackupper
{
    using Libs;

    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            SetDS3SaveDataPath();
            SetBackUpDataPath();
        }

        private void SetDS3SaveDataPath()
        {
            saveDataDirectoryBox.Text = PathUtility.GetDS3SaveDataPath();
        }

        private void SetBackUpDataPath()
        {
            backUpDirectoryBox.Text = System.IO.Path.Combine(PathUtility.GetDS3SaveDataPath(), "backups");
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
    }
}
