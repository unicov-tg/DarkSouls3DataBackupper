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

        public static string GetTempPath(string backUpPath)
        {
            return Path.Combine(backUpPath, "tmp");
        }
    }
}
