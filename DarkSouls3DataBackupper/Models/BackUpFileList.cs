using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
        public readonly string name;
        public readonly string createdAt;

        public BackUpFile(string name, string createdAt)
        {
            this.name = name;
            this.createdAt = createdAt;
        }
    }
}
