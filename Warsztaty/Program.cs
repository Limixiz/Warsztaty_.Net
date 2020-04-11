using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Warsztaty
{
    public class Program
    {

        static List<TaskModel> ReadFromFile()
        {
            string content = System.IO.File.ReadAllText("dane.txt");
            var output = JsonConvert.DeserializeObject<List<TaskModel>>(content);
            return output;
        }
        static void SaveToFile(List<TaskModel> zadania)
        {
            string output = JsonConvert.SerializeObject(zadania);
            System.IO.File.WriteAllText("dane.txt", output);
        }
        public static void Main(string[] args)
        {
            List<TaskModel> zadania = new List<TaskModel>();
            string command = "";
            do
            {
                Console.Write("Czekam na Polecenie: ");
                command = Console.ReadLine();
                Console.WriteLine();
                command = command.ToUpper().Trim();
                if (command == "ADD")
                // if (command == "Add" || command == "add")
                {

                    Console.Write("Podaj opis zadania: ");
                    string OpisZadania = Console.ReadLine();
                    Console.WriteLine();
                    if (string.IsNullOrWhiteSpace(OpisZadania) == false)
                    {
                        Console.Write("Podaj datę początkową w formacie(yyyy-MM-dd): ");
                        string dataStart = Console.ReadLine();
                        Console.WriteLine();
                        bool parseResult = DateTime.TryParse(dataStart, out var d);

                        if (parseResult)
                        {
                            TaskModel nzadanie = new TaskModel(OpisZadania, dataStart);
                            Console.Write("Czy zadanie ma charakter całodniowy?: (TAK/NIE) ");

                            string flag = Console.ReadLine();
                            flag = flag.ToUpper().Trim();
                            if (flag == "NIE")
                            {
                                bool parseResultend = false;

                                Console.Write("Podaj datę zakończenia zadania w formacie(yyyy-MM-dd): ");
                                string dataEnd = Console.ReadLine();
                                Console.WriteLine();
                                parseResultend = DateTime.TryParse(dataEnd, out var b);
                                if (parseResultend)
                                {
                                    nzadanie.Datę_Zakończenia = dataEnd;
                                    Console.WriteLine("Poprawnie dodano nowe zadanie!");
                                    zadania.Add(nzadanie);
                                }
                                else
                                {
                                    Console.WriteLine("Błedny format daty!");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Poprawnie dodano nowe zadanie!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Porażka.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Błędny opis!");
                    }
                }

                if (command == "REMOVE")
                {
                    int index = 0;
                    int padding2 = 20;
                    Console.WriteLine($"{"Index".PadLeft(padding2)}|{"Zadanie".PadLeft(padding2)}|{"Data Rozpoczęcia".PadLeft(padding2)}|{"Data Zakończenia".PadLeft(padding2)}");
                    foreach (var item in zadania)
                    {
                        string a = "";
                        if (item.Datę_Zakończenia != null)
                        {
                            a = item.Datę_Zakończenia.PadLeft(padding2);
                        }
                        string b = item.Datę_Zakończenia?.PadLeft(padding2) ?? "Brak";
                        string c = item.Datę_Zakończenia != null ? item.Datę_Zakończenia.PadLeft(padding2) : "Brak";
                        Console.WriteLine($"{index.ToString().PadLeft(padding2)}|{item.Opis.PadLeft(padding2)}|{item.Data_Rozpoczęcia.PadLeft(padding2)}|{item.Datę_Zakończenia?.PadLeft(padding2)}");
                    }
                    if (command == "SHOWALL")
                    {
                        int padding = 20;
                        Console.WriteLine($"{"Zadanie".PadLeft(padding)}|{"Data Rozpoczęcia".PadLeft(padding)}|{"Data Zakończenia".PadLeft(padding)}");
                        foreach (var item in zadania)
                        {
                            string a = "";
                            if (item.Datę_Zakończenia != null)
                            {
                                a = item.Datę_Zakończenia.PadLeft(padding);
                            }
                            string b = item.Datę_Zakończenia?.PadLeft(padding) ?? "Brak";
                            string c = item.Datę_Zakończenia != null ? item.Datę_Zakończenia.PadLeft(padding) : "Brak";
                            Console.WriteLine($"{item.Opis.PadLeft(padding)}|{item.Data_Rozpoczęcia.PadLeft(padding)}|{item.Datę_Zakończenia?.PadLeft(padding)}");
                        }
                    }
                }

                /*
                 * 
                if (command == "exit")
                {
                    break;
                }
                */
            }
            while (command != "EXIT");
            Console.WriteLine("Program Zamknięty");

        }
    }
    public class TaskModel
    {
        public string Opis { get; }
        public string Data_Rozpoczęcia { get; }
        public string Datę_Zakończenia { get; set; }
        public TaskModel(string name, string admitted)
        {
            Opis = name;
            Data_Rozpoczęcia = admitted;

        }

        public void RemoveTask()
        {




        }
        public void ShowInfo()
        {
            Console.WriteLine($"{Opis}: Przygarnięty: {Data_Rozpoczęcia}, Adoptowany: {(Datę_Zakończenia != null ? Datę_Zakończenia : "Brak")}");
        }
    }
}
}

