using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Threading.Tasks;


namespace ConsoleApp1
{
    class Doter
    {
        public int MMR;
        public string Clikuha;
        public int Hours;

        public string GetDoter()
        {
            return $"{Clikuha}: {MMR}";
        }

        public bool IsPidrila()
        {
            if(MMR < 4499 && Hours > 500)
            {
                return true;
            }

            return false;
        }

        public Doter(int mmr, int hours, string clikuha)
        {
            MMR = mmr;
            Hours = hours;
            Clikuha = clikuha;
        }
    }

    class Program
    {
        static WorkWithFile workWithMenu;
        static WorkWithFile workWithUsersData;


        static string DoterToConsole(int mmr, string name)
        {
            return name + ": " + mmr;
        }

        public static void Main(string[] args)
        {
            ToLog(new string('-', 30));

        

            List<Doter> doters = new List<Doter>();
            doters.Add(new Doter(1500, 10000, "Zhigir"));
            doters.Add(new Doter(4500, 30, "Bazhila"));
            doters.Add(new Doter(3500, 5345, "Andretian"));

            doters = doters.OrderBy(x => x.MMR).ToList();

            foreach (var item in doters)
            {
                ToLog(item.GetDoter() + " - he is pidr? " + item.IsPidrila());
            }
                       

        
            

            Console.ReadKey();

            string fileWithMenuItems = @"C:\Users\Zhenya\source\repos\ConsoleApp1\ConsoleApp1\Menus.txt";
            workWithMenu = new WorkWithFile(fileWithMenuItems);
            string fileName = @"C:\Users\Zhenya\source\repos\ConsoleApp1\ConsoleApp1\MenuItems.txt";
            workWithUsersData = new WorkWithFile(fileName);


            Console.OutputEncoding = Encoding.UTF8;
            if (!File.Exists(fileWithMenuItems))
            {
                workWithMenu.WriteCreateFile(false,
                    "Добавить строку в файл", "Вывести строки из файла", "Вывести строку по номеру",
                    "Вывести все строки со словом", "Удалить строку из файла по номеру",
                    "Удалить строку из файла по значению", "Удалить все строки из файла со словом");
            }

            List<string> menuItems = workWithMenu.GetAllLinesFromFile();

            if (menuItems == null || menuItems.Count == 0)
            {
                Console.WriteLine("Bratishka - file is empty or not exist");
                return;
            }
            string generatedMenu = ConcatLines(menuItems);


            Console.WriteLine();

            ToLog(generatedMenu);
            int vibor = 0;
            while (vibor != -1)
            {
                if (!File.Exists(fileName))
                {

                    workWithUsersData.WriteCreateFile(false, "Значение 1", "Значение 2", "Значение 3");
                }

                var userNumberRusult = GetUserResponse("Please, choose element of menu: ", true, 1, 7, "Пункты меню не содержат ваш выбор");
                Console.Clear();
                ToLog(generatedMenu);
                var answerToUser = AnswerToUserChoice(userNumberRusult);
                ToLog(answerToUser);
            }
        }

        static int GetUserResponse(string question, bool needTocheckRange = false, int minRange = 0, int maxRange = 10, string errorMessage = "")
        {
            int responseNumber = -1;

            while (responseNumber == -1)
            {

                ToLog(question);
                string response = ToRead();
                if (int.TryParse(response, out responseNumber))
                {
                    if (needTocheckRange)
                    {
                        if (responseNumber < minRange || responseNumber > maxRange)
                        {
                            ToLog(errorMessage);

                            responseNumber = -1;
                            continue;
                        }
                    }

                    break;
                }
                else
                {
                    ToLog("Please, write a number.");
                }

                responseNumber = -1;
            }

            return responseNumber;
        }
        static string ToRead(string quetion = null)
        {
            if (!string.IsNullOrEmpty(quetion))
            {
                ToLog(quetion);
            }
            string Rostik = Console.ReadLine();
            return Rostik;
        }
        static void ToLog(string message)
        {
            Console.WriteLine(message);
        }

        static string ConcatLines(List<string> fileContent, string delimiter = "\r\n")
        {
            string generatedOutput = "";
            for (int i = 0; i < fileContent.Count; i++)
            {
                generatedOutput +=
                    (i + 1 + "." + fileContent[i] + delimiter);
            }
            return generatedOutput;
        }

        //string Lines = null;
        //if (!File.Exists(path))
        //{
        //    return Lines;
        //}
        //using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
        //    sw.WriteLine(line);
        //return line;
        static string AnswerToUserChoice(int choice)
        {
            List<string> lines;
            string caseAnswer = "";
            switch (choice)
            {
                case 1:
                    string line = ToRead("Please, enter the text: ");
                    workWithUsersData.WriteCreateFile(false, line);
                    break;
                case 2:
                    lines = workWithUsersData.GetAllLinesFromFile();
                    caseAnswer = ConcatLines(lines);
                    break;
                case 3:
                    var userResponse = GetUserResponse("Please, enter the number of the line you would like to output: ");
                    caseAnswer = workWithUsersData.GetFileRowByIndex(userResponse - 1);
                    break;
                case 4:
                    string searchWord = ToRead("Please, enter the word, with which you want to output the lines: ");
                    caseAnswer = workWithUsersData.GetAllLinesWithWord(searchWord);
                    break;
                case 5:
                    var userResponse1 = GetUserResponse("Please select a line you want to delete: ");
                    caseAnswer = workWithUsersData.LineToDeleteByNumber(userResponse1 - 1);
                    break;
                case 6:
                    string meaning = ToRead("Please, enter the line which you would like to delete: ");
                    caseAnswer = (workWithUsersData.DeleteAllLinesByMeaning(meaning));
                    break;
                case 7:
                    string searchWord1 = ToRead("Please, enter the word, with which you want to delete the lines: ");
                    caseAnswer = (workWithUsersData.DeleteAllLinesByWord(searchWord1));
                    break;
                default:
                    caseAnswer = ("Wrong choice, try again");
                    break;
            }
            return caseAnswer;
        }

    }
}
