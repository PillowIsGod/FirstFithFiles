using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleApp1
{
    class WorkWithFile
    {
        public readonly string FileName;

        public WorkWithFile(string fileName)
        {
            FileName = fileName;
        }

        public void WriteCreateFile(bool truncate = false, params string[] linesToAppend)
        {
            FileMode fm = FileMode.Append;

            if (truncate)
            {
                fm = FileMode.Truncate;
            }

            using (FileStream fs = new FileStream(FileName, fm))
            using (StreamWriter sr = new StreamWriter(fs))
            {
                foreach (var item in linesToAppend)
                {
                    sr.WriteLine(item);
                }
            }
        }

        public List<string> GetAllLinesFromFile()
        {

            List<string> lines = new List<string>();
            if (!File.Exists(FileName))
            {
                return lines;
            }
            using (StreamReader sr = new StreamReader(FileName))
            {
                string line = null;
                while ((line = sr.ReadLine()) != null)
                {
                    if (string.IsNullOrEmpty(line))
                    {
                        continue;
                    }
                    lines.Add(line);
                }
                return lines;
            }
        }
        public string GetAllLinesWithWord(string searchWord, string delimiter = "\r\n")
        {
            string line = "";
            List<string> lines = GetAllLinesFromFile();

            string str;
            if ((str = FileNotExistRangeError(lines)) != null)
            {
                return str;
            }



            bool flag = true;
            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].Contains(searchWord))
                {
                    line += (lines[i] + delimiter);


                    flag = false;
                }
            }

            if (flag)
            {
                return "The word you search for does not exist in this file.";
            }

            return line;
        }
        public string GetFileRowByIndex(int indexRow)
        {

            List<string> lines = GetAllLinesFromFile();
            string str;
            if ((str = FileNotExistRangeError(lines, indexRow)) != null)
            {
                return str;
            }
            return lines[indexRow];
        }
        public string LineToDeleteByNumber(int index)
        {
            List<string> lines = GetAllLinesFromFile();
            if (lines == null || lines.Count == 0)
            {
                return "File is empty.";

            }
            if (index < 0 || index > lines.Count - 1)
            {
                return "Your index is wrong, enter number of range 0 - " + (lines.Count - 1);
            }
            lines.RemoveAt(index);
            WriteCreateFile(true, lines.ToArray());
            //using (StreamWriter sr = new StreamWriter(filePath))
            //{
            //    for (int i = 0; i < lines.Count; i++)
            //    {
            //        sr.WriteLine(lines[i]);

            //    }
            //}
            return "Line number " + (index + 1) + " has been deleted.";
        }
        public string DeleteAllLinesByWord(string searchWord)
        {
            List<string> lines = GetAllLinesFromFile();
            if (lines == null || lines.Count == 0)
            {
                return "File is empty.";

            }

            bool flag = true;
            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].Contains(searchWord, StringComparison.OrdinalIgnoreCase))
                {
                    lines.Remove(lines[i]);
                    flag = false;
                }
            }
            if (flag)
            {
                return "Such word does not exist in this file";
            }
            WriteCreateFile(true, lines.ToArray());
            //for (int i = 0; i < lines.Count; i++)
            //{
            //    sr.WriteLine(lines[i]);
            //}
            return "The line with your word has been removed.";
        }
        public string DeleteAllLinesByMeaning(string meaning)
        {
            List<string> lines = GetAllLinesFromFile();
            if (lines == null || lines.Count == 0)
            {
                return "File is empty.";

            }
            if (!lines.Contains(meaning))
            {
                return "Such line does not exist in this file";
            }


            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i] == meaning)
                {
                    lines.Remove(lines[i]);
                }
            }

            WriteCreateFile(true, lines.ToArray());

            return "The line you have just entered was deleted from the file.";
        }



        private string FileNotExistRangeError(List<string> array, int? indexRow = null)
        {
            string line = null;
            if (array == null || array.Count == 0)
            {
                //throw new NullReferenceException( "File is empty, enter some rows");
                return "File is empty, enter some rows";
            }

            if (array.Count == 1)
            {
                //throw new NullReferenceException( "File is empty, enter some rows");
                return "Слишком мало строк, Петр, введи ее!!";
            }


            if (indexRow != null && (indexRow < 0 || indexRow > array.Count - 1))
            {
                //throw new ArgumentOutOfRangeException( "Your index is wrong, enter number of range 0 - " + (array.Count - 1));
                return "Your index is wrong, enter number of range 0 - " + (array.Count - 1);
            }

            return line;
        }

    }
}
