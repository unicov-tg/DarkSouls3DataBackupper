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
    }
}
