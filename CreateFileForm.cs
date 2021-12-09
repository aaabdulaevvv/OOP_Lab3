using System;
using System.IO;
using System.Windows.Forms;

namespace FileManager
{
    public partial class CreateFileForm : Form
    {
        private readonly string[] FileTypes = {".txt",
                                               ".html",
                                               ".mp4",
                                               ".png",
                                               ".jpg",
                                               ".jpeg",
                                               ".pdf" };
        public CreateFileForm()
        {
            InitializeComponent();
        }

        private void CreateFileForm_Load(object sender, EventArgs e)
        {
            TypeComboBox.Items.AddRange(FileTypes);
            NameTextBox.Text = null;
            TypeComboBox.Text = null;
            PathTextBox.Text = null;
        }

        private void ChooseButton_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    PathTextBox.Text = fbd.SelectedPath;
                }
            }
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (NameTextBox.Text == "" 
                    || TypeComboBox.Text == "" 
                    || PathTextBox.Text == "")
                {
                    MessageBox.Show("Назва, тип або шлях не вказано\n" +
                    "Для створення файлу вкажіть всі атрибути.");
                }
                else
                {
                    string path = PathTextBox.Text + "\\" + NameTextBox.Text + TypeComboBox.Text;

                    if (File.Exists(path))
                        File.Delete(path);

                    using (FileStream fs = File.Create(path)) { }

                    MessageBox.Show("File was created");
                    this.Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("CreateFileException");
            }
        }

        private void NameTextBox_Leave(object sender, EventArgs e)
        {
            bool isInvalidSymbol = (NameTextBox.Text.IndexOfAny("!@#$%^&*()=+/\\\"|`~№;:?,.".ToCharArray()) == -1);
            if (isInvalidSymbol) { }
            else
            {
                MessageBox.Show("Ви використали заборонений символ" +
                    "\nТакі як: \'#\',\'$\',\'@\'...");
                NameTextBox.Text = null;
            }
        }

        private void TypeLabel_Click(object sender, EventArgs e)
        {

        }

        private void NameTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void TypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
