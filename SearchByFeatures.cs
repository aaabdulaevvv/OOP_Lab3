using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace FileManager
{
    public partial class SearchByFeatures : Form
    {
        private List<string> filePath = new List<string>();
        private List<string> fileName = new List<string>();

        string printList;

        private string dirPath;

        public string DirPath { set { dirPath = value; } }

        private readonly string[] FileTypes = {".txt",
                                               ".html",
                                               ".mp4",
                                               ".png",
                                               ".jpg",
                                               ".jpeg",
                                               ".pdf" };

        public SearchByFeatures()
        {
            InitializeComponent();
        }

        private void CopyButton_Click(object sender, EventArgs e)
        {
            Search(dirPath);
            this.Close();
        }

        private void Search(string directoryPath)
        {
            if (TypeComboBox.Text != null && TypeComboBox.Text != "")
            {
                CopyByType(directoryPath);
            }

            foreach (KeyValuePair<string, string> file in Buffer.groupFileCopyPast)
            {
                printList += ($"{file.Key} == {file.Value}" + "\n\n");
            }

            MessageBox.Show(printList);
        }

        private void CopyByType(string directoryPath)
        {
            filePath.Clear();
            string[] filesInDir = Directory.GetFiles(directoryPath);
            foreach (var characterfile in filesInDir)
            {
                if (characterfile.EndsWith(TypeComboBox.Text))
                {
                    filePath.Add(characterfile);
                }
            }

            TrimName();

            for (int index = 0; index < fileName.Count; index++)
            {
                Buffer.groupFileCopyPast.Add(fileName[index], filePath[index]);
            }
        }

        private void TrimName()
        {
            foreach (string file in filePath)
            {
                string tmpFile;
                tmpFile = file.Substring(file.LastIndexOf('\\') + 1);
                tmpFile = tmpFile.Remove(tmpFile.LastIndexOf('.'), TypeComboBox.Text.Length);
                fileName.Add(tmpFile);
            }
        }

        private void SearchByFeatures_Load(object sender, EventArgs e)
        {
            TypeComboBox.Items.AddRange(FileTypes);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void TypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
