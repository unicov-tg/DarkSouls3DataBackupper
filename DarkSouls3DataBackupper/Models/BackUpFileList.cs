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
        public List<BackUpFile> List { get; private set; }
        
        private readonly string backUpPath;
        

        public BackUpFileList(string backUpPath)
        {
            this.backUpPath = backUpPath;
            ReadBackUpFiles();
        }

        public void ReadBackUpFiles()
        {
            var files = Directory.GetFiles(backUpPath, "*.zip");

            List = files.Select(file => new BackUpFile(file, File.GetCreationTime(file).ToString())).ToList();
        }
    }

    class BackUpFile
    {
        public string Name { get; private set; }
        public string CreatedAt { get; private set; }

        public string FilePath { get; private set; }

        public BackUpFile(string path, string createdAt)
        {
            FilePath = path;
            Name = Path.GetFileNameWithoutExtension(path);
            CreatedAt = createdAt;
        }
    }
}
