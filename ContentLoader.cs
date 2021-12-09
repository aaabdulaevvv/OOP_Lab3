using System;
using System.IO;
using System.Windows.Forms;

namespace FileManager
{
    class ContentLoader
    {
        private int icoIndex;

        public void LoadFilesAndDirectories(ListView LvDir, ListView LvFile, string sidePath)
        {
            DirectoryInfo fileList;
            LvDir.Clear();
            LvFile.Clear();
            try
            {
                fileList = new DirectoryInfo(sidePath);
                DirectoryInfo[] dirs = fileList.GetDirectories();
                FileInfo[] files = fileList.GetFiles();

                for (int j = 0; j < dirs.Length; j++)
                {
                    LvDir.Items.Add(dirs[j].Name, 2);
                }
                for (int j = 0; j < files.Length; j++)
                {
                    LvFile.Items.Add(files[j].Name, 13);
                }

                if (LvDir.Items.Count == 0)
                {
                    LvDir.Items.Add("No such directories");
                }
                if (LvFile.Items.Count == 0)
                {
                    LvFile.Items.Add("No such files");
                }
            }
            catch (Exception) 
            {
                MessageBox.Show("Your PC Haven't such Hard Drive");
            }
        }

        public void SearchFiles(ListView Lv, string sidePath)
        {
            DirectoryInfo fileList;
            Lv.Clear();

            try
            {
                fileList = new DirectoryInfo(sidePath);
                FileInfo[] files = fileList.GetFiles();

                for (int j = 0; j < files.Length; j++)
                {
                    Lv.Items.Add(files[j].Name, icoIndex);
                }

                if (Lv.Items.Count == 0)
                {
                    Lv.Items.Add("No such files");
                }
            }
            catch (Exception) 
            {                
                MessageBox.Show("No such files for filtering");
            }
        }

        public void GetFileType(ComboBox Cb, ref string sideFileType)
        {            
            switch (Cb.Text)
            {
                case "All files [*.]":
                    sideFileType = "";
                    icoIndex = 13;
                    break;
                case "Text files [*.txt]":
                    sideFileType = ".txt";
                    icoIndex = 9;
                    break;
                case "Html files [*.html]":
                    sideFileType = ".html";
                    icoIndex = 1;
                    break;                
                case "Movies [*.mp4]":
                    sideFileType = ".mp4";
                    icoIndex = 6;
                    break;
                case "Photo [*.jpeg]":
                    sideFileType = ".jpeg";
                    icoIndex = 3;
                    break;
                case "Photo [*.jpg]":
                    sideFileType = ".jpg";
                    icoIndex = 4;
                    break;
                case "Photo [*.png]":
                    sideFileType = ".png";
                    icoIndex = 8;
                    break;
                case "PDF files [*.pdf]":
                    sideFileType = ".pdf";
                    icoIndex = 7;
                    break;
                case "Music [*.mp3]":
                    sideFileType = ".mp3";
                    icoIndex = 5;
                    break;
                case "Icons [*.ico]":
                    sideFileType = ".ico";
                    icoIndex = 1;
                    break;
                default:
                    break;
            }
        }

        public void TypeFilter(ListView Lv, string path, string sideFileType)
        {
            Lv.Clear();
            SearchFiles(Lv, path);

            for (int index = Lv.Items.Count - 1; -1 < index; index--)
            {
                if (Lv.Items[index].Text.EndsWith(sideFileType) == false)
                {
                    Lv.Items[index].Remove();
                }
            }
        }

        public void Update(ListView LvDir, ListView LvFile, string sidePath)
        {
            if (sidePath != null)
            {
                LvDir.Clear();
                LvFile.Clear();
                LoadFilesAndDirectories(LvDir, LvFile, sidePath);
            }
        }

        public void GetMainHelp()
        {
            MessageBox.Show("1) Щоб відкрити файл, натисніть Файл -> Відкрити, й оберіть файл." +
                "\n2) Щоб створити файл, натисніть Файл -> Створити, і вкажіть необхідні дані." +
                "\n3) Щоб видалити файл натисніть Файл -> Видалити, і оберіть файл для втдалення." +
                "\n4) Щоб скопіювати файл, натисніть на файл, тоді оберіть Операції -> 'c' Скопіювати" +
                " або клікніть на файл та натисніть кнопку 'c'." +
                "\n5) Щоб вставити файл в папку, клікніть на потрібну папку, тоді оберіть Операції -> 'v' Перемістити" +
                " або клікніть на папку та натисніть кнопку 'v'." +
                "\n6) Щоб видалити файл, натисніть на файл, тоді оберіть Операції -> 'd' Видалити" +
                " або клікніть на файл та натисніть кнопку 'd'." +
                "\n7) Щоб скопіювати файл, натисніть на файл, тоді оберіть Операції -> 'o' Відкрити" +
                " або клікніть на файл та натисніть кнопку 'o'." +
                "\n8) Для того щоб дізнатись інформацію про автора оберіть Програма -> Про власника", "Основний функціонал");
        }

        public void GetLabHelp()
        {
            MessageBox.Show("1) 9. Копіювати .txt => Оберіть фалй для копіювання, " +
                "розширення txt, далі оберіть Lab Операції і натисність Копіювати .txt." +
                "\n2) 2. Підрахунок слів => Оберіть фалй для підрахунку слів, " +
                "розширення txt, далі оберіть Lab Операції і натисність Підрахунок слів." +
                "\n3) 3. Копіювати групу файлів => перейдіть в директорію, де вам потрібно скопіювати файли," +
                "далі оберіть Lab Операції --> Копіювати групу файлів --> Копіювати" +
                " у вікні що з'явиться, оберіть розширення файлів, для копіювання." +
                " Аби створити ці копії, оберіть директорію, " +
                " далі оберіть Lab Операції --> Копіювати групу файлів --> Вставити" +
                "\n4) 9. Копіювати HTML => перейдіть в директорію, де вам потрібно скопіювати файли," +
                "далі оберіть необхідний файл, далі оберіть Lab Операції --> Копіювати HTML --> Копіювати" +
                " Аби створити ці копії, оберіть директорію, " +
                " далі оберіть Lab Операції --> Копіювати HTML --> Перемістити", "Лабораторна");
        }

        public void GetStartHelp()
        {
            MessageBox.Show("1) Для початку роботи, оберіть диск, на якому будуте працювати." +
                "\n2) Щоб відкрити теку, двічі натисніть на неї." +
                "\n3) Аби повернутись на один крок назад, натисніть на кнопку '<<'." +
                "\n4) Щоб відкрити файл, двічі натисніть на нього." +
                "\n5) Файл відкриється у тому редакторі, який вибраний за замовчуванням." +
                "\n6) Для оновлення контенту після створення чи видалення файлу, натисніть кнопку Оновити." +
                "\n7) Аби побачти файли лише певного типу, оберіть тип у випадаючому вікні.", "Початок роботи");
        }
    }
}
