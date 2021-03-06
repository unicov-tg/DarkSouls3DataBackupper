﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace DarkSouls3DataBackupper.Libs
{
    class DataBackUp
    {
        private readonly string saveDataPath;
        private readonly string backUpPath;
        private readonly string backUpFileName;

        private string TempPath => PathUtility.GetTempPath(backUpPath);

        public DataBackUp(string saveDataPath, string backUpPath, string backUpFileName)
        {
            this.saveDataPath = saveDataPath;
            this.backUpPath = backUpPath;
            this.backUpFileName = PathUtility.GetUniqueFileName(backUpPath, backUpFileName, ".zip");
        }

        public void Save()
        {
            CheckValidFileName();
            CheckSaveDataPathContainsTargetFiles();
            CreateWorkingDirectory();
            CopyCurrentDataToTemp();
            CreateZip();
        }

        private void CheckValidFileName()
        {
            if (PathUtility.CheckFileNameContainsInvalidCharacter(backUpFileName))
            {
                throw new Exception("バックアップファイル名に使用できない文字が含まれています。");
            }
        }

        private void CheckSaveDataPathContainsTargetFiles()
        {
            if (!PathUtility.CheckDirectoryContainsTargetFiles(saveDataPath))
            {
                throw new Exception("指定されたフォルダにセーブデータが存在しません。");
            }
        }

        private void CreateWorkingDirectory()
        {
            Directory.CreateDirectory(backUpPath);
            Directory.CreateDirectory(TempPath);
        }

        private void CopyCurrentDataToTemp()
        {
            foreach(var target in PathUtility.TargetFileNames)
            {
                var sourcePath = Path.Combine(saveDataPath, target);
                var destPath = Path.Combine(TempPath, target);
                File.Copy(sourcePath, destPath, true);
            }
        }

        private void CreateZip()
        {
            var backUpDestination = Path.Combine(backUpPath, backUpFileName);
            ZipFile.CreateFromDirectory(TempPath, backUpDestination);
        }
    }
}
