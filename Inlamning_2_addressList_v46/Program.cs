using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Inlamning_2_addressList_v46
{
    class Person
    {
        public string name, address, telephone, email;
        public Person(string N, string A, string T, string E)
        {
            name = N; address = A; telephone = T; email = E;
        }
        public Person()
        {
            Console.WriteLine("Adds new person");
            Console.Write("  1. enter name:    ");
            name = Console.ReadLine();
            Console.Write("  2. enter address:  ");
            address = Console.ReadLine();
            Console.Write("  3. enter telphone: ");
            telephone = Console.ReadLine();
            Console.Write("  4. enter email:   ");
            email = Console.ReadLine();
        }
        public string PrintPerson()
        {
            return $"{name},{address},{telephone},{email}";
        }
        public void Print(StreamWriter sw)
        {
            // sw.WriteLine("{0},{1},{2},{3}", name, address, telephone, email);

            sw.WriteLine(PrintPerson());
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<Person> Dict = new List<Person>();
            string fileStream = @"C:\Users\basma\address.lis";

            Console.Write("Loading address list ... ");
            Load(Dict);

            Console.WriteLine("Hello and welcome to the address list");
            Console.WriteLine("Type 'quit' to quit!");
            string command;
            do
            {
                Console.Write("> ");
                command = Console.ReadLine();
                if (command == "quit")
                {
                    Console.WriteLine("Hej då!");
                }
                else if (command == "new")
                {
                    Dict.Add(new Person());
                }
                else if (command == "delete")
                {
                    Console.Write("Who do you want to delete (enter name): ");
                    string wantsToDelete = Console.ReadLine();
                    int found = -1;
                    for (int i = 0; i < Dict.Count(); i++)
                    {
                        if (Dict[i].name == wantsToDelete) found = i;
                    }
                    if (found == -1)
                    {
                        Console.WriteLine("Unfortunately: {0} was not in telephone list", wantsToDelete);
                    }
                    else
                    {
                        Dict.RemoveAt(found);
                    }
                }
                else if (command == "view")
                {
                    for (int i = 0; i < Dict.Count(); i++)
                    {
                        Person P = Dict[i];
                        Console.WriteLine("{0}, {1}, {2}, {3}", P.name, P.address, P.telephone, P.email);
                    }
                }
                else if (command == "change")
                {
                    Console.Write("Who do you want to change (enter name): ");
                    string wantChange = Console.ReadLine();
                    int found = -1;
                    for (int i = 0; i < Dict.Count(); i++)
                    {
                        if (Dict[i].name == wantChange) found = i;
                    }
                    if (found == -1)
                    {
                        Console.WriteLine("Unfortunately: {0} was not in telephone list", wantChange);
                    }
                    else
                    {
                        Console.Write("What do you want to change (name, address, telephone or email): ");
                        string fieldToChange = Console.ReadLine();
                        Console.Write("What do you want to change {0} on {1} to: ", fieldToChange, wantChange);
                        string newValue = Console.ReadLine();
                        switch (fieldToChange)
                        {
                            case "name": Dict[found].name = newValue; break;
                            case "address": Dict[found].address = newValue; break;
                            case "telephone": Dict[found].telephone = newValue; break;
                            case "email": Dict[found].email = newValue; break;
                            default: break;
                        }
                    }
                }
                else if (command == "print")
                {
                    Console.WriteLine(Dict.PrintPerson());
                }
                else
                {
                    Console.WriteLine("Unknown command: {0}", command);
                }
            } while (command != "quit");
        }

        private static void Load(List<Person> Dict)
        {
            using (StreamReader fileStream = new StreamReader(@"C:\Users\basma\address.lis"))
            {
                while (fileStream.Peek() >= 0)
                {
                    string line = fileStream.ReadLine();
                    // Console.WriteLine(line);
                    string[] word = line.Split('#');
                    // Console.WriteLine("{0}, {1}, {2}, {3}", word[0], word[1], word[2], word[3]);
                    Person P = new Person(word[0], word[1], word[2], word[3]);
                    Dict.Add(P);
                }
            }
            Console.WriteLine("Done!"); //List<Person> Dict = new List<Person>();
        }
        private static List<Person> MoveAddressList(string fileStream, List<Person> Dict)
        {
            // List<Person> Dict = new List<Person>();
            using (StreamWriter sr1 = new StreamWriter(fileStream))
            {

                for (int i = 0; i < Dict.Count(); i++)
                {
                    // sr1.WriteLine("{0},{1},{2},{3}", Dict[i].name, Dict[i].address, Dict[i].telephone, Dict[i].email);
                    fileStream[i].Print(sr1);
                }

            }
            return Dict;

        }
    }
}
