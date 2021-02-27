using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.ObjectModel;

namespace DarkSouls3DataBackupper.Models
{
    class BackUpFileList
    {
        public ObservableCollection<BackUpFile> List { get; private set; }
        
        private string backUpPath;

        public BackUpFileList(string backUpPath)
        {
            this.backUpPath = backUpPath;
            List = new ObservableCollection<BackUpFile>();
            UpdateBackUpFileList();
        }

        public void ChangeDiretory(string path)
        {
            backUpPath = path;
            UpdateBackUpFileList();
        }

        public void UpdateBackUpFileList()
        {
            List.Clear();

            foreach(var file in GetBackUpFiles())
            {
                List.Add(file);
            }
        }

        private IEnumerable<BackUpFile> GetBackUpFiles()
        {
            var files = Directory.GetFiles(backUpPath, "*.zip");
            return files.Select(file => new BackUpFile(file, File.GetCreationTime(file))).OrderByDescending(file => file.CreatedAt);
        }
    }

    class BackUpFile
    {
        public string Name { get; private set; }
        public string CreatedAtString => CreatedAt.ToString();

        public string FilePath { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public BackUpFile(string path, DateTime createdAt)
        {
            FilePath = path;
            Name = Path.GetFileNameWithoutExtension(path);
            CreatedAt = createdAt;
        }
    }
}
