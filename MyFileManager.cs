using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace FileManager
{
    public partial class MyFileManager : Form
    {
        private readonly string[] MainFolderPath = { "C:\\", "D:\\" };
        private readonly string[] FileTypes = {"All files [*.]", 
                                               "Text files [*.txt]", 
                                               "Html files [*.html]",
                                               "Movies [*.mp4]",
                                               "Photo [*.png]",
                                               "Photo [*.jpg]",
                                               "Photo [*.jpeg]",
                                               "PDF files [*.pdf]",
                                               "Music [*.mp3]",
                                               "Icons [*.ico]"
        };
        private string leftFileType = "";
        private string rightFileType = "";        

        private string leftCurrPath = "";
        private string rightCurrPath = "";

        private string leftSelectedFile;
        private string rightSelectedFile;
        private string focusSelectedFile;

        private string leftPath;
        private string rightPath;

        private string focusFilePath;
        private string focusFolderPath;
        private string focusSide;

        readonly ContentLoader LoadContent = new ContentLoader();
        readonly Browser browser = new Browser();
        readonly Buffer buffer = new Buffer();
        readonly WordsCounter counter = new WordsCounter();
        readonly HtmlParser hParser = new HtmlParser();

        public MyFileManager()
        {
            InitializeComponent();                        
        }

        private void MyFileManager_Load(object sender, System.EventArgs e)
        {
            LeftFoldersComboBox.Items.AddRange(MainFolderPath);
            RightFoldersComboBox.Items.AddRange(MainFolderPath);

            LeftTypeComboBox.Items.AddRange(FileTypes);
            RightTypeComboBox.Items.AddRange(FileTypes);

            LeftBackButton.Enabled = false;
            RightBackButton.Enabled = false;

            LeftDirPath.Text = null;
            RightDirPath.Text = null;
        }

        private void MyFileManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Ви справді бажаєте вийти?", "Attention!", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                e.Cancel = false;
            }
            else
                e.Cancel = true;
        }

        #region LeftSide

        private void LeftFoldersComboBox_TextChanged(object sender, EventArgs e)
        {
            if (LeftFoldersComboBox.Text != "")
            {
                leftPath = LeftFoldersComboBox.Text;
                LeftDirPath.Text = leftPath;

                LeftDirListView.Clear();
                LeftFilesListView.Clear();

                LeftBackButton.Enabled = true;

                LoadContent.LoadFilesAndDirectories(LeftDirListView, LeftFilesListView, leftPath);
            }
        }

        private void LeftDirListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {            
            for (int i = 0; i < LeftDirListView.Items.Count; i++)
            {
                if (LeftDirListView.Items[i].Selected)
                {
                    leftCurrPath = LeftDirListView.Items[i].Text;
                }
            }
            leftPath += leftCurrPath + "\\";
            LeftDirPath.Text = leftPath;
            LoadContent.LoadFilesAndDirectories(LeftDirListView, LeftFilesListView, leftPath);
        }

        private void LeftTypeComboBox_TextChanged(object sender, EventArgs e)
        {
            LoadContent.GetFileType(LeftTypeComboBox, ref leftFileType);
            LoadContent.TypeFilter(LeftFilesListView, leftPath, leftFileType);
        }

        private void LeftBackButton_Click(object sender, EventArgs e)
        {
            if (LeftFoldersComboBox.Text == LeftDirPath.Text || leftPath == null) { }
            else
            {
                leftPath = leftPath.Remove(leftPath.Length - 1);
                leftPath = leftPath.Substring(0, leftPath.LastIndexOf('\\'));
                leftPath += "\\";
                LeftDirPath.Text = leftPath;
                LoadContent.LoadFilesAndDirectories(LeftDirListView, LeftFilesListView, leftPath);
            }
        }

        private void LeftDirPath_TextChanged(object sender, EventArgs e)
        {
            LeftTypeComboBox.Text = FileTypes[0];
        }

        private void LeftFilesListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Process.Start(leftPath + leftSelectedFile);
        }

        private void LeftFilesListView_MouseClick(object sender, MouseEventArgs e)
        {
            leftSelectedFile = LeftFilesListView.SelectedItems[0].Text;
            focusFilePath = leftPath + leftSelectedFile;
            focusSelectedFile = leftSelectedFile;
        }

        private void LeftUpdateButton_Click(object sender, EventArgs e)
        {
            LoadContent.Update(LeftDirListView, LeftFilesListView, leftPath);
        }

        private void LeftFilesListView_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'c' || e.KeyChar == 'с')
                CopyClick();
            if (e.KeyChar == 'd' || e.KeyChar == 'в')
                DeleteClick();
            if (e.KeyChar == 'o' || e.KeyChar == 'щ')
                OpenForEdit();
        }

        private void LeftDirListView_MouseClick(object sender, MouseEventArgs e)
        {
            focusFolderPath = leftPath + LeftDirListView.SelectedItems[0].Text;
            focusSide = "left";
        }

        private void LeftDirListView_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'v' || e.KeyChar == 'м')
                PasteClick();
        }

        private void LeftFilesListView_Leave(object sender, EventArgs e)
        {
            focusFilePath = null;
            counter.wordList = null;
        }

        private void LeftFilesListView_BeforeLabelEdit(object sender, LabelEditEventArgs e)
        {
            BeforeLabEdit(LeftFilesListView, LeftDirPath);
        }

        private void LeftFilesListView_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            AfterLabEdit(LeftDirPath, e);

            LoadContent.Update(LeftDirListView, LeftFilesListView, leftPath);
        }

        #endregion LeftSide

        #region RightSide

        private void RightFoldersComboBox_TextChanged(object sender, EventArgs e)
        {
            if (RightFoldersComboBox.Text != "")
            {
                rightPath = RightFoldersComboBox.Text;
                RightDirPath.Text = rightPath;

                RightDirListView.Clear();
                RightFilesListView.Clear();

                RightBackButton.Enabled = true;

                LoadContent.LoadFilesAndDirectories(RightDirListView, RightFilesListView, rightPath);
            }
        }

        private void RightDirListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < RightDirListView.Items.Count; i++)
            {
                if (RightDirListView.Items[i].Selected)
                {
                    rightCurrPath = RightDirListView.Items[i].Text;
                }
            }
            rightPath += rightCurrPath + "\\";
            RightDirPath.Text = rightPath;
            LoadContent.LoadFilesAndDirectories(RightDirListView, RightFilesListView, rightPath);
        }

        private void RightTypeComboBox_TextChanged(object sender, EventArgs e)
        {
            LoadContent.GetFileType(RightTypeComboBox, ref rightFileType);
            LoadContent.TypeFilter(RightFilesListView, rightPath, rightFileType);
        }

        private void RightDirPath_TextChanged(object sender, EventArgs e)
        {
            RightTypeComboBox.Text = FileTypes[0];
        }

        private void RightBackButton_Click(object sender, EventArgs e)
        {
            if (RightFoldersComboBox.Text == RightDirPath.Text || rightPath == null) { }
            else
            {
                rightPath = rightPath.Remove(rightPath.Length - 1);
                rightPath = rightPath.Substring(0, rightPath.LastIndexOf('\\'));
                rightPath += "\\";
                RightDirPath.Text = rightPath;
                LoadContent.LoadFilesAndDirectories(RightDirListView, RightFilesListView, rightPath);
            }
        }

        private void RightFilesListView_MouseClick(object sender, MouseEventArgs e)
        {
            rightSelectedFile = RightFilesListView.SelectedItems[0].Text;
            focusFilePath = rightPath + rightSelectedFile;
            focusSelectedFile = rightSelectedFile;
        }

        private void RightFilesListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Process.Start(rightPath + rightSelectedFile);
        }

        private void RughtUpdateButton_Click(object sender, EventArgs e)
        {
            LoadContent.Update(RightDirListView, RightFilesListView, rightPath);           
        }

        private void RightDirListView_MouseClick(object sender, MouseEventArgs e)
        {
            focusFolderPath = rightPath + RightDirListView.SelectedItems[0].Text; ;
            focusSide = "right";
        }

        private void RightFilesListView_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'c' || e.KeyChar == 'с')
                CopyClick();
            if (e.KeyChar == 'd' || e.KeyChar == 'в')
                DeleteClick();
            if (e.KeyChar == 'o' || e.KeyChar == 'щ')
                OpenForEdit();
        }

        private void RightDirListView_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'v' || e.KeyChar == 'м')
                PasteClick();
        }

        private void RightFilesListView_Leave(object sender, EventArgs e)
        {
            focusFilePath = null;
            counter.wordList = null;
        }

        private void RightFilesListView_BeforeLabelEdit(object sender, LabelEditEventArgs e)
        {
            BeforeLabEdit(RightFilesListView, RightDirPath);
        }

        private void RightFilesListView_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            AfterLabEdit(RightDirPath, e);

            LoadContent.Update(RightDirListView, RightFilesListView, rightPath);
        }

        #endregion RightSide

        #region ToolStripMenuItem

        public void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            browser.OpenFileClick();
        }

        private void CreateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            browser.CreateFileClick();
        }

        private void DelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            browser.DeleteFileClick();
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsCanVoid())
            {
                buffer.FileCopy(focusSelectedFile, focusFilePath);
                MessageBox.Show("Скопійовано!");
            }
        }

        private void MoveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (focusFilePath != "" || focusFilePath != null)
                PasteClick();
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsCanVoid())
                DeleteClick();
        }

        private void EditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsCanVoid())
                OpenForEdit();
        }

        private void LabCopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsCanVoid())
            {
                if (focusFilePath.EndsWith(".txt"))
                {
                    var SpecCopy = new SpecificCopy();
                    SpecCopy.CopyParser(buffer, focusSelectedFile, focusFilePath);
                }
                else
                    MessageBox.Show("Даний файл не має розширення txt");
            }
        }

        private void CounterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsCanVoid())
            {
                if (focusFilePath.EndsWith(".txt"))
                {
                    counter.Counter(focusFilePath);
                }
                else
                    MessageBox.Show("Файл не має розширення txt");
            }                
        }

        private void LabCopy_Click(object sender, EventArgs e)
        {
            if (LeftDirPath.Text != null && LeftDirPath.Text != "")
            {
                var LabSearch = new SearchByFeatures
                {
                    DirPath = LeftDirPath.Text
                };
                LabSearch.Show();
            }
            else if (RightDirPath.Text != null && RightDirPath.Text != "")
            {
                var LabSearch = new SearchByFeatures
                {
                    DirPath = RightDirPath.Text
                };
                LabSearch.Show();
            }
            else
                MessageBox.Show("Оберіть директорію!");
        }

        private void LabPaste_Click(object sender, EventArgs e)
        {
            if (focusFolderPath != null)
            {
                buffer.DirPath = focusFolderPath;
                buffer.LoadFiles();
            }
            else
                MessageBox.Show("Місце збереження не обрано");
        }

        private void CopyHtml_Click(object sender, EventArgs e)
        {
            if (IsCanVoid() && focusFilePath.EndsWith(".html"))
            {
                hParser.Path = focusFilePath;
                hParser.Copy();
            }
        }

        private void MoveHtml_Click(object sender, EventArgs e)
        {
            if (focusFolderPath != null)
            {
                hParser.DirPath = focusFolderPath;

                MessageBox.Show("focusFolderPath + '' + hParser.Name = " + focusFolderPath + "\\" + hParser.Name);
                if (File.Exists(focusFolderPath + "\\" + hParser.Name))
                    MessageBox.Show("Даний файл вже існує!");
                else
                {
                    hParser.PastDepFiles();
                    int index;

                    if (focusSide == "left")
                    {
                        LoadContent.Update(LeftDirListView, LeftFilesListView, leftPath);

                        index = hParser.GetIndex(LeftFilesListView);
                        if (index != 0)
                        {
                            LeftFilesListView.Items[index].BeginEdit();
                        }
                    }
                    else
                    {
                        LoadContent.Update(RightDirListView, RightFilesListView, rightPath);

                        index = hParser.GetIndex(RightFilesListView);
                        if (index != 0)
                        {
                            RightFilesListView.Items[index].BeginEdit();
                        }                        
                    }
                }
            }
            else
                MessageBox.Show("Місце збереження не обрано");          
        }

        private void MainHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadContent.GetMainHelp();
        }

        private void LabHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadContent.GetLabHelp();
        }

        private void StartHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadContent.GetStartHelp();
        }

        #endregion ToolStripMenuItem

        #region Function

        private void PasteClick()
        {
            if (focusFolderPath != null)
            {
                if (File.Exists($"{focusFolderPath + "\\" + buffer.OldFileName}"))
                {
                    MessageBox.Show("Даний файл вже існує!");
                }
                else
                {
                    if (buffer.fileCopyPast.Count != 0)
                    {
                        buffer.FilePast(focusFolderPath);
                        MessageBox.Show("Копію створено!");
                    }
                    else
                        MessageBox.Show("Немає скопійованих файлів");
                }
            }
            else
                MessageBox.Show("Місце збереження не обрано");
        }

        private void CopyClick()
        {
            if (IsCanVoid())
            {
                buffer.FileCopy(focusSelectedFile, focusFilePath);
                MessageBox.Show("Скопійовано!");
            }                
        }

        private void DeleteClick()
        {
            if (IsCanVoid())
            {
                if (MessageBox.Show("Ви справді бажаєте видалити?", "Attention!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    File.Delete(focusFilePath);
                }
            }             
        }

        private void OpenForEdit()
        {
            if (IsCanVoid())
                Process.Start(focusFilePath);
        }

        private bool IsCanVoid()
        {
            if (focusFilePath == "" || focusFilePath == null)
            {
                MessageBox.Show("Файл не обрано!");
                return false;
            }
            else
                return true;
        }

        private void BeforeLabEdit(ListView sideFilesListView, TextBox sideDirPath)
        {
            foreach (ListViewItem lvi in sideFilesListView.SelectedItems)
            {
                focusSelectedFile = sideDirPath.Text + lvi.Text;
            }
        }

        private void AfterLabEdit(TextBox sideDirPath, LabelEditEventArgs e)
        {
            string fileType = focusSelectedFile.Substring(focusSelectedFile.IndexOf("."), 4);
            string destPath = "";

            switch (fileType)
            {
                case ".txt":
                    destPath = NewPath(".txt", sideDirPath, e);
                    break;
                case ".htm":
                    destPath = NewPath(".html", sideDirPath, e);
                    break;
                case ".jpg":
                    destPath = NewPath(".jpg", sideDirPath, e);
                    break;
                case ".ico":
                    destPath = NewPath(".ico", sideDirPath, e);
                    break;
                case ".jpe":
                    destPath = NewPath(".jpeg", sideDirPath, e);
                    break;
                case ".pdf":
                    destPath = NewPath(".pdf", sideDirPath, e);
                    break;
                default:
                    MessageBox.Show("Тип файлу не вказано");
                    break;
            }       

            File.Move(focusSelectedFile, destPath);
        }

        private string NewPath(string type, TextBox sideDirPath, LabelEditEventArgs e)
        {
            if (!e.Label.EndsWith(type))
                return sideDirPath.Text + e.Label + type;
            else
                return sideDirPath.Text + e.Label;
        }

        #endregion Function

        private void ProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Лабораторну роботу №3" +
                  "\n\"Файловий менеджер\"" +
                  "\nВиконав студент групи К-24" +
                  "\nАбдулаєв Андрій", "Про автора");
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}