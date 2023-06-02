using expert_system_v._1._0.Entities;
using expert_system_v._1._0.Service;
using System;


class Program
{

    public static void Main(string[] args)
    {

        PersonRepository persons = new PersonRepository();  

        List<Person> list = persons.GeneratePersons();



        foreach(Person p in list)
        {

            Console.WriteLine(p.ToString());

        }

        Console.WriteLine($"Total de personagens com poderes: {list.Where(p => p.HavePower == true).Count()}");
        Console.WriteLine($"Total de personagens vilões: {list.Where(p => p.IsVillain == true).Count()}");
        Console.WriteLine($"Total de personagens monstros: {list.Where(p => p.IsMonster == true).Count()}");
        Console.WriteLine($"Total de personagens animais: {list.Where(p => p.IsAnimal == true).Count()}");


        Console.Write("\n\nAperte enter para continuar: ");
        Console.ReadLine();
        Console.Clear();


        Console.Write("O personagem que você escolheu tem algum poder? (s/n) ");
        Console.Write("");
        string choice = Console.ReadLine().ToLower();

        Console.Clear();

        List<Person> possibleChars = new List<Person>();


        if(choice == "s")
        {

            possibleChars = list.Where(p => p.HavePower == true).ToList();


            Console.WriteLine("Possiveis chars:\n");

            foreach(Person p in possibleChars)
            {
                Console.WriteLine(p.Name);
            }


            Console.WriteLine($"\nTotal de personagens com poderes: {possibleChars.Where(p => p.HavePower == true).Count()}");
            Console.WriteLine($"Total de personagens vilões: {possibleChars.Where(p => p.IsVillain == true).Count()}");
            Console.WriteLine($"Total de personagens monstros: {possibleChars.Where(p => p.IsMonster == true).Count()}");
            Console.WriteLine($"Total de personagens animais: {possibleChars.Where(p => p.IsAnimal == true).Count()}");


            Console.Write("\n\nAperte enter para continuar: ");
            Console.ReadLine();
            Console.Clear();


            Console.WriteLine("O seu personagem é o vilão do filme em que ele participa? (s/n) ");
            Console.Write("");
            choice = Console.ReadLine().ToLower();

            if(choice == "s")
            {
                Console.Clear();
                Console.WriteLine($"O seu personagem é o : {possibleChars.Where(p => p.IsVillain == true).First().Name}");
            }
            else if(choice == "n")
            {

                possibleChars = possibleChars.Where(p => p.HavePower == true).ToList();


                Console.WriteLine($"Total de personagens com cabelo preto: {possibleChars.Where(p => p.CorCabelo == "Preto").Count()}");
                Console.WriteLine($"Total de personagens com cabelo loiro: {possibleChars.Where(p => p.CorCabelo == "Loiro").Count()}");
                Console.WriteLine($"Total de personagens com cabelo ruivo: {possibleChars.Where(p => p.CorCabelo == "Ruivo").Count()}");
                Console.WriteLine($"Total de personagens com cabelo castanho: {possibleChars.Where(p => p.CorCabelo == "Castanho").Count()}");


                Console.Write("\n\nAperte enter para continuar: ");
                Console.ReadLine();
                Console.Clear();

                bool finalChoice = false;

                while (!finalChoice)
                {


                    foreach (Person p in possibleChars)
                    {

                        Console.WriteLine($"O personagem escolhido é {p.UniqueFeature}? (s/n)");
                        choice = Console.ReadLine();

                        if (choice == "s")
                        {
                            Console.Clear();
                            Console.WriteLine($"O seu personagem é o {p.Name}");

                            finalChoice = true;

                            break;
                        }
                        else
                        {
                            Console.Clear();
                        }

                    }


                }


            }
            else
            {
                Console.WriteLine("Escolha inválida.");
            }

        }


    }

}