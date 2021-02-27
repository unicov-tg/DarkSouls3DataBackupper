using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using DarkSouls3DataBackupper.Libs;

namespace DarkSouls3DataBackupper.Models
{
    class DataRestore
    {
        private readonly string saveDataPath;
        private readonly string backUpFilePath;
        private readonly string tempPath;

        public DataRestore(string saveDataPath, string backUpFilePath, string tempPath)
        {
            this.saveDataPath = saveDataPath;
            this.backUpFilePath = backUpFilePath;
            this.tempPath = tempPath;
        }

        public void Restore()
        {
            ClearTempDirecotry();
            UnZipBackUpFile();
            OverWriteSaveData();
        }

        private void ClearTempDirecotry()
        {
            var files = Directory.GetFiles(tempPath);
            foreach(var file in files)
            {
                File.Delete(file);
            }
        }

        private void UnZipBackUpFile()
        {
            ZipFile.ExtractToDirectory(backUpFilePath, tempPath);
        }

        private void OverWriteSaveData()
        {
            foreach(var targetFileName in PathUtility.TargetFileNames)
            {
                var sourcePath = Path.Combine(tempPath, targetFileName);
                var destPath = Path.Combine(saveDataPath, targetFileName);
                File.Copy(sourcePath, destPath, true);
            }
        }
    }
}
