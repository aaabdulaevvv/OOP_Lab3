using System.Diagnostics;
using System.Windows.Forms;

namespace FileManager
{
    class Browser
    {
        Load load;
        Create create;
        Delete delete;

        public Browser()
        {
            load = new Load();
            create = new Create();
            delete = new Delete();
        }

        public void OpenFileClick()
        {
            load.OpenFile();
        }

        public void CreateFileClick()
        {
            create.CreateFile();
        }

        public void DeleteFileClick()
        {
            delete.DeleteFile();
        }        
    }

    class Load
    {
        public void OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = "C:\\",
                Filter = "All files(*.*)|*.*" +
                "| TXT files(*.txt)|*.txt " +
                "| Html files (*.html)|*.html" +
                "| Movies (*.mp4)|*.mp4" +
                "| Photo (*.png)|*.png" +
                "| Photo (*.jpg)|*.jpg" +
                "| Photo (*.jpeg)|*.jpeg" +
                "| PDF files (*.pdf)|*.pdf" +
                "| Music (*.mp3)|*.mp3" +
                "| Icons (*.ico)|*.ico",
                FilterIndex = 1,
                RestoreDirectory = true
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Process.Start($"{openFileDialog.FileName}");
            }                
        }
    }

    class Create
    {
        public void CreateFile()
        {
            var createFileForm = new CreateFileForm();
            createFileForm.Show();
        }
    }

    class Delete 
    {
        public void DeleteFile()
        {
            var deleteFileForm = new DeleteForm();
            deleteFileForm.Show();
        }
    }
}
