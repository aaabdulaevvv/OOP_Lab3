using System.Collections.Generic;
using System.IO;

namespace FileManager
{
    class WordsCounter
    {
        public Dictionary<string, int> Counts = new Dictionary<string, int>();
        private string word = "";
        public string wordList;

        public void Counter(string focusPath)
        {
            using (StreamReader Reader = new StreamReader(focusPath))
            {                
                char nextChar;
                Counts.Clear();
                while (!Reader.EndOfStream)
                {
                    nextChar = (char)Reader.Read();

                    if (nextChar == ' ' || nextChar == '\r' || nextChar == '\n' || nextChar == '\t')
                    {
                        isContain();
                        nextChar = (char)Reader.Read();
                        while (nextChar == ' ' || nextChar == '\r' || nextChar == '\n' || nextChar == '\t')
                        {
                            nextChar = (char)Reader.Read();
                        }
                    }
                    word += nextChar;                    
                }
                isContain();

                foreach (KeyValuePair<string, int> keyValue in Counts)
                {
                    wordList += keyValue.Key + " == " + keyValue.Value + "\n";
                }
                System.Windows.Forms.MessageBox.Show(wordList, "List of Words");
                Counts.Clear();
                wordList = null;
            }
        }

        private void isContain()
        {
            if (Counts.ContainsKey(word))
            {
                Counts[word] += 1;
            }
            else
            {
                if (word != " " && word != "\r" && word != "\n" && word != "\t" && word != "")
                    Counts.Add(word, 1);
            }
            word = "";
        }
    }
}
