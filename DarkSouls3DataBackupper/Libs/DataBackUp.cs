using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkSouls3DataBackupper.Libs
{
    class DataBackUp
    {
        private string saveDataPath;
        private string backUpPath;
        private string backUpFileName;

        public DataBackUp(string saveDataPath, string backUpPath, string backUpFileName)
        {
            this.saveDataPath = saveDataPath;
            this.backUpPath = backUpPath;
            this.backUpFileName = backUpFileName;
        }

        public void Save()
        {
            checkAndCreateBackUpDirectory();
        }

        private void checkAndCreateBackUpDirectory()
        {
            if (System.IO.Directory.Exists(backUpPath))
            {
                return;
            }

            System.IO.Directory.CreateDirectory(backUpPath);
        }
    }
}
