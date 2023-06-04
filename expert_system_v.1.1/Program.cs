using expert_system_v._1._0.Entities;
using expert_system_v._1._0.Service;
using System;
using System.Reflection.Metadata.Ecma335;

class Program
{

    public static void Main(string[] args)
    {

        PersonRepository persons = new PersonRepository();
        Person person = new Person();
        AttrGroup group = new AttrGroup();





        Console.Write("Bem vindo ao Sábio. Aperte qualquer tecla para continuar.");

        ConsoleKeyInfo keyInfo = Console.ReadKey();
        char keyChar = keyInfo.KeyChar;



        //Gerando persons
        List<Person> list = persons.GeneratePersons();


        //Mostrando persons
        person.ShowMyPersons(list);


        Console.Write("Esse são os seus personagens. Clique enter para continuar.");
        Console.ReadLine();
        Console.Clear();




        Dictionary<string, string> questions = group.GenerateGroupQuestions(list);



        List<AttrGroup> groups = person.GroupAndCount(list, questions);



        Console.Clear();
        Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n");



        //Mostrando todos os grupos
        Console.WriteLine("\nSábio: Pelas características dos personagens que você criou, eu achei melhor separar eles da seguinte forma:\n\n");
        group.ShowAllGourps(groups);



        Console.ReadLine();
        Console.Clear();



        //Criando grupos
        AttrGroup groupPower = groups.Where(p => p.Nome == "HavePower").FirstOrDefault();
        AttrGroup groupVillain = groups.Where(v => v.Nome == "IsVillain").FirstOrDefault();
        AttrGroup groupMonster = groups.Where(p => p.Nome == "IsMonster").FirstOrDefault();
        AttrGroup groupAnimal = groups.Where(p => p.Nome == "IsAnimal").FirstOrDefault();



        //Verificando grupo com mais persons
        string majorGroup = person.GetMajorGroup
            (
                groupPower,
                groupVillain,
                groupMonster,
                groupAnimal
            );




        //Inicia mostrando a pergunta do grupo no qual pertence.
        Console.Write($"O personagem que você escolheu {questions[majorGroup]} (s/n): ");


        //Verificando escolha
        int choice = VerifyKey();
        if (choice == 0) return;


        Console.Clear();


        //
        List<AttrGroup> possibleChars = new List<AttrGroup>();


        if (choice == 1)
        {

            Console.WriteLine($"{groups.OrderByDescending(p => p.Persons.Count).FirstOrDefault()}");


            Console.ReadLine();

            possibleChars.Add(groups.OrderByDescending(p => p.Persons.Count).FirstOrDefault());


            Console.WriteLine("Possiveis chars:\n");

            foreach (AttrGroup g in possibleChars)
            {

                foreach (Person p in g.Persons)
                {
                    Console.WriteLine(p.Name);
                }

            }



            Console.ReadLine();

            Console.Clear();

            Console.WriteLine("Aqui 1:");

            questions = group.GenerateGroupQuestions(possibleChars.First().Persons.ToList());


            Console.WriteLine("Aqui 2:");

            groups = person.GroupAndCount(possibleChars.First().Persons.ToList(), questions);


            Console.ReadLine();


            //person.GroupAndCount(list);

            //Console.Clear();



            Console.Write("O seu personagem é o vilão do filme em que ele participa? (s/n): ");
            //Verificando escolha
            choice = VerifyKey();
            if (choice == 0) return;


            if (choice == 1)
            {
                Console.Clear();
                //Console.WriteLine($"O seu personagem é o : {possibleChars.Where(p => p.IsVillain == true).First().Name}");
            }
            else if (choice == 2)
            {

                //possibleChars = possibleChars.Where(p => p.HavePower == true).ToList();


                //person.GroupAndCount(list);

                Console.Clear();


                bool finalChoice = false;

                while (!finalChoice)
                {


                    /*foreach (Person p in possibleChars)
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
                    */

                }


            }
            else if (choice == 3)
            {
                Console.WriteLine("Escolha inválida.");
            }

        }
        else if (choice == 2)
        {

            foreach (AttrGroup g in groups)
            {

                if (g.Nome != majorGroup)
                {
                    possibleChars.Add(g);
                }

            }


            Console.WriteLine("Possible groups:");
            foreach (AttrGroup p in possibleChars)
            {
                Console.WriteLine(p.ToString());
            }



            Console.ReadLine();

            Console.WriteLine($"{groups.OrderByDescending(p => p.Persons.Count).FirstOrDefault()}");


            Console.ReadLine();

            possibleChars.Add(groups.OrderByDescending(p => p.Persons.Count).FirstOrDefault());


            Console.WriteLine("Possiveis chars:\n");

            foreach (AttrGroup g in possibleChars)
            {

                foreach (Person p in g.Persons)
                {
                    Console.WriteLine(p.Name);
                }

            }



            Console.ReadLine();

            Console.Clear();

            Console.WriteLine("Aqui 1:");

            questions = group.GenerateGroupQuestions(possibleChars.First().Persons.ToList());


            Console.WriteLine("Aqui 2:");

            groups = person.GroupAndCount(possibleChars.First().Persons.ToList(), questions);


        }
        else if(choice == 3)
        {
            Console.Clear();
            Console.WriteLine("Aperte uma tecla possível para continuar.");
        }


    }


    public static int VerifyKey()
    {

        ConsoleKeyInfo keyInfo = Console.ReadKey();
        var keyChar = keyInfo.KeyChar;

        if (keyChar == '1')
        {

            Console.Clear(); 
            Console.WriteLine("\nEnd.\n");
            
            return 0;
        }
        else
        {

            if(keyChar == 's')
            {
                return 1;
            }
            else if(keyChar == 'n')
            {
                return 2;
            }
            else
            {
                return 3;
            }

        }

    }

}