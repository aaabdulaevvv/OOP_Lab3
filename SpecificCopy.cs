using System.IO;

namespace FileManager
{
    class SpecificCopy
    {
        private string firstLine = "";
        private string secondLine;

        public void CopyParser(Buffer buffer, string focusSelectedFile, string focusFilePath)
        {            
            using (StreamReader sstream = new StreamReader(focusFilePath))
            {
                using (StreamWriter swriter = new StreamWriter("tmpFile.txt"))
                {
                    foreach (string line in File.ReadLines(focusFilePath))
                    {
                        secondLine = line;
                        if (firstLine.Equals(secondLine))
                        {
                            //ignor
                        }
                        else
                        {
                            swriter.WriteLine(secondLine);
                        }
                        firstLine = secondLine;
                    }
                }
                                
                buffer.FileCopy(focusSelectedFile, "tmpFile.txt");
                System.Windows.Forms.MessageBox.Show("Скопійовано!");
            }
        }
    }
}
