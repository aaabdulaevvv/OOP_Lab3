using System.Collections.Generic;
using System.IO;

namespace FileManager
{
    class Buffer
    {
        public Dictionary<string, string> fileCopyPast = new Dictionary<string, string>();    
        private string oldFileName;
        private string oldFilePath;
        private string dirPath;

        public static Dictionary<string, string> groupFileCopyPast = new Dictionary<string, string>();

        public string OldFileName { get { return oldFileName; } }

        public string OldFilePath { get { return oldFilePath; } }

        public string DirPath { set { dirPath = value; } }

        public void FileCopy(string fileName, string filePath)
        {
            fileCopyPast.Clear();
            oldFileName = fileName;
            oldFilePath = filePath;
            fileCopyPast.Add(fileName, filePath);
        }

        public void FilePast(string newFilePath)
        {
            if (fileCopyPast.TryGetValue(oldFileName, out oldFilePath))
            {
                File.Copy(oldFilePath, newFilePath + "\\" + oldFileName);
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Немає скопійованих файлів");
            }
        }

        public void LoadFiles()
        {
            foreach (KeyValuePair<string, string> keyValue in groupFileCopyPast)
            {                
                File.Copy(keyValue.Value, dirPath + "\\" + keyValue.Key);
            }

            System.Windows.Forms.MessageBox.Show($"Файли скопійовано в директорію {dirPath}");
        }
    }
}