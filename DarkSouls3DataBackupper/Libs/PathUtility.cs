using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DarkSouls3DataBackupper.Libs
{
    class PathUtility
    {
        public static readonly string[] TargetFileNames = new string[2] { "DS30000.sl2", "DS30000.sl3" };

        public static string DS3AppDataPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DarkSoulsIII");

        /// <summary>
        /// ダークソウル3のセーブデータパスを取得
        /// </summary>
        /// <returns>セーブデータのパス</returns>
        public static string GetDS3SaveDataPath()
        {
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var path = Path.Combine(appDataPath, "DarkSoulsIII");
            var saveDataDirectoryPath = Directory.GetDirectories(path).First();

            return saveDataDirectoryPath;
        }

        public static bool CheckDirectoryContainsTargetFiles(string path)
        {
            foreach(var target in TargetFileNames)
            {
                if(!File.Exists(Path.Combine(path, target)))
                {
                    return false;
                }
            }

            return true;
        }

        public static string GetTempPath(string backUpPath)
        {
            return Path.Combine(backUpPath, "tmp");
        }

        public static string GetUniqueFileName(string path, string name, string ext)
        {
            if (!File.Exists(Path.Combine(path, name + ext)))
            {
                return name + ext;
            }

            int count = 1;

            while (true)
            {
                var fileName = Path.Combine(path, name + $"({count})" + ext);
                if(!File.Exists(fileName))
                {
                    break;
                }
                count += 1;
            }

            return name + $"({count})" + ext;
        }

        public static bool CheckFileNameContainsInvalidCharacter(string fileName)
        {
            var invalidChars = Path.GetInvalidPathChars().Concat(Path.GetInvalidFileNameChars()).Distinct().ToArray();

            return fileName.IndexOfAny(invalidChars) > 0;
        }
    }
}
