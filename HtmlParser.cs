using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FileManager
{
    class HtmlParser
    {
        private HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
        private List<string> depFilesPath = new List<string>();
        private List<string> nameDepFiles = new List<string>();

        private string path;
        private string name;
        private string dirPath;

        public string Path { set { path = value; } }
        public string Name { get { return name; }}

        public string DirPath { set { dirPath = value; } }

        public void Copy()
        {
            doc.Load(path);
            nameDepFiles.Clear();
            name = path.Substring(path.LastIndexOf('\\') + 1);

            MessageBox.Show("path = " + path);

            if (doc.DocumentNode != null)
            {
                foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//a"))
                {
                    depFilesPath.Add(node.GetAttributeValue("href", null));
                }

                TrimName();

                MessageBox.Show("Файли скопійовано");
            }            
        }

        public void PastDepFiles()
        {         
            File.Copy(path, dirPath + "\\" + name);            

            if (dirPath != null && dirPath != "")
            {
                for (int index = 0; index < depFilesPath.Count; index++)
                {
                    MessageBox.Show("depFilesPath[index] = " + depFilesPath[index]);
                    MessageBox.Show("dirPath + '' + nameDepFiles[index] = " + dirPath + '\\' + nameDepFiles[index]);
                    if (depFilesPath[index] != null)
                    {
                        File.Copy(depFilesPath[index], dirPath + '\\' + nameDepFiles[index]);
                    }
                }
            }
            MessageBox.Show("Копію відтворено");
        }  
        
        public int GetIndex(ListView FilesListView)
        {
            ListViewItem item = FilesListView.Items
                             .Cast<ListViewItem>()
                             .FirstOrDefault(x => x.Text == name);

            return FilesListView.Items.IndexOf(item);
        }

        private void TrimName()
        {
            foreach (string file in depFilesPath)
            {
                string tmpFile;
                tmpFile = file.Substring(file.LastIndexOf('\\') + 1);
                nameDepFiles.Add(tmpFile);
                MessageBox.Show(tmpFile);
            }
        }
    }
}
