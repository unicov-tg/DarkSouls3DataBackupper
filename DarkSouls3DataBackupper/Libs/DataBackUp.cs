using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DarkSouls3DataBackupper.Libs
{
    class DataBackUp
    {
        private readonly string[] targetFileNames = new string[2] { "DS30000.sl2", "DS30000.sl3" };

        private readonly string saveDataPath;
        private readonly string backUpPath;
        private readonly string backUpFileName;

        private string TempPath => Path.Combine(backUpPath, "tmp");

        public DataBackUp(string saveDataPath, string backUpPath, string backUpFileName)
        {
            this.saveDataPath = saveDataPath;
            this.backUpPath = backUpPath;
            this.backUpFileName = backUpFileName;
        }

        public void Save()
        {
            CreateWorkingDirectory();
            CopyCurrentDataToTemp();
        }

        private void CreateWorkingDirectory()
        {
            Directory.CreateDirectory(backUpPath);
            Directory.CreateDirectory(TempPath);
        }

        private void CopyCurrentDataToTemp()
        {
            foreach(var target in targetFileNames)
            {
                var sourcePath = Path.Combine(saveDataPath, target);
                var destPath = Path.Combine(TempPath, target);
                File.Copy(sourcePath, destPath);
            }
        }
    }
}
