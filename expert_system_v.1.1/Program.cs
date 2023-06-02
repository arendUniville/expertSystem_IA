using expert_system_v._1._0.Entities;
using expert_system_v._1._0.Service;
using System;


class Program
{

    public static void Main(string[] args)
    {

        PersonRepository persons = new PersonRepository();
        Person person = new Person();
        AttrGroup group = new AttrGroup();


        List<Person> list = persons.GeneratePersons();


        foreach (Person p in list)
        {
            Console.WriteLine(p.ToString());
        }

        Console.Clear();




        Dictionary<string, string> questions = group.GenerateGroupQuestions(list);



        List<AttrGroup> groups = person.GroupAndCount(list, questions);


        Console.Clear();

        Console.WriteLine("\nInteligência Artificial: Os personagens foram distribuidos nos seguintes grupos:\n\n");


        int cnt = 0;

        foreach (AttrGroup g in groups)
        {
            Console.WriteLine($"Grupo [{cnt}]:\n{g.ToString()}");
            cnt++;
        }


        Console.ReadLine();
        Console.Clear();


        AttrGroup groupPower = groups.Where(p => p.Nome == "HavePower").FirstOrDefault();
        AttrGroup groupVillain = groups.Where(v => v.Nome == "IsVillain").FirstOrDefault();
        AttrGroup groupMonster = groups.Where(p => p.Nome == "IsMonster").FirstOrDefault();
        AttrGroup groupAnimal = groups.Where(p => p.Nome == "IsAnimal").FirstOrDefault();


        string majorGroup = person.GetMajorGroup
            (
                groupPower,
                groupVillain,
                groupMonster,
                groupAnimal
            );




        //Mostra a pergunta do grupo no qual pertence.
        Console.WriteLine($"O personagem que você escolheu {questions[majorGroup]} (s/n)");
        Console.Write("");
        string choice = Console.ReadLine().ToLower();

        Console.Clear();



        List<AttrGroup> possibleChars = new List<AttrGroup>();


        if (choice == "s")
        {

            Console.WriteLine($"{groups.OrderByDescending(p => p.Persons.Count).FirstOrDefault()}");


            Console.ReadLine();

            possibleChars.Add(groups.OrderByDescending(p => p.Persons.Count).FirstOrDefault());


            Console.WriteLine("Possiveis chars:\n");

            foreach (AttrGroup g in possibleChars)
            {

                foreach(Person p in g.Persons)
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



            Console.WriteLine("O seu personagem é o vilão do filme em que ele participa? (s/n) ");
            Console.Write("");
            choice = Console.ReadLine().ToLower();

            if (choice == "s")
            {
                Console.Clear();
                //Console.WriteLine($"O seu personagem é o : {possibleChars.Where(p => p.IsVillain == true).First().Name}");
            }
            else if (choice == "n")
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
            else if (choice == "")
            {
                return;
            }
            else
            {
                Console.WriteLine("Escolha inválida.");
            }

        }
        else if (choice == "n")
        {

            foreach(AttrGroup g in groups)
            {

                if(g.Nome != majorGroup)
                {
                    possibleChars.Add(g);
                }

            }


            Console.WriteLine("Possible groups:");
            foreach(AttrGroup p in possibleChars)
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


    }

}