using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Inlamning_2_addressList_v46
{
    /* CLASS: Person
    * PURPOSE: a person entry in the address list*/
    class Person
    {
        public string name, address, telephone, email;
        public Person(string N, string A, string T, string E)
        {
            name = N; address = A; telephone = T; email = E;
        }
        /* METHOD: Person (public structor)
        * PURPOSE: user enter the input of name, address, telphone number and email.
        PARAMETERS: None */
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
        /* METHOD: PrintPerson
        * PURPOSE: write a the person's entred info to the address list file
        * PARAMETERS: None
        * RETURN VALUE: void */
        public string PrintPerson()
        {
            return $"{name},{address},{telephone},{email}";
        } 
    }
    /* CLASS: Program
   * PURPOSE: program commands made to reach the file in List<Person> Dict and make changes*/
    class Program
    {
        static void Main(string[] args)
        {
            List<Person> Dict = new List<Person>();
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
                        Dict[i].PrintPerson();
                        //Console.WriteLine("{0}, {1}, {2}, {3}", P.name, P.address, P.telephone, P.email);
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
                else
                {
                    Console.WriteLine("Unknown command: {0}", command);
                }
            } while (command != "quit");
        }
        /* METHOD: Load (static path)
         * PURPOSE: To find and read a path file. 
         * The file lines of will be read as a text and split by #.
         * Each split creates a line of info for one person in List<Person> Dict
         * PARAMETERS: List<Person> Dict to add new created objects
         * RETURN VALUE: None*/
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
            Console.WriteLine("Done!");
        }    
    }
}
