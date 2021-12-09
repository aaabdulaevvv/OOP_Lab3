using System;
using System.IO;
using System.Windows.Forms;

namespace FileManager
{
    public partial class DeleteForm : Form
    {
        public DeleteForm()
        {
            InitializeComponent();
        }

        private void ChooseButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            PathTextBox.Text = openFileDialog1.FileName;
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (PathTextBox.Text == "")
            {
                MessageBox.Show("Оберіть файл!");
            }
            else
            {
                if (MessageBox.Show("Ви справді бажаєте видалити?", "Attention!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    File.Delete(PathTextBox.Text);
                }
            }

            this.Close();
        }

        private void DeleteForm_Load(object sender, EventArgs e)
        {

        }

        private void PathTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
