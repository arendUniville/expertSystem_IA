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

        bool showPersons = false;
        bool showGroups = false;
        bool showMessages = false;


        Console.Write("Bem vindo ao Sábio. Aperte qualquer tecla para continuar.");

        //Verificando escolha
        int choice = VerifyKey();
        if (choice == 2) return;


        //Gerando persons
        List<Person> list = persons.GeneratePersons();


        //Mostrando persons
        if(showPersons)
        {
            person.ShowMyPersons(list);
            Console.Write("Esse são os seus personagens. Clique enter para continuar.");


            //Verificando escolha
            choice = VerifyKey();
            if (choice == 2) return;


            Console.Clear();
            Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n");

        }

        Console.Clear();


        Dictionary<string, string> questions = group.GenerateGroupQuestions(list);
        List<AttrGroup> groups = person.GroupAndCount(list, questions);



        //Mostrando todos os grupos
        if(showGroups)
        {

            Console.WriteLine("\nSábio: Pelas características dos personagens que você criou, eu achei melhor separar eles da seguinte forma:\n\n");
            group.ShowAllGourps(groups);


            //Verificando escolha
            choice = VerifyKey();
            if (choice == 2) return;
            Console.Clear();


        }
        


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
                groupAnimal,

                showMessages
            );



        //Inicia mostrando a pergunta do grupo no qual pertence.
        Console.Write($"Sábio: O personagem que você escolheu {questions[majorGroup]} (s/n): ");


        //Verificando escolha
        choice = VerifyKey();
        if (choice == 2) return;


        Console.Clear();


        //Iniciando lista de possíveis características
        List<AttrGroup> possibleChars = new List<AttrGroup>();


        if (choice == 1)
        {

            //Mostrando o grupo que foi perguntado ao usuário
            //Console.WriteLine($"{groups.OrderByDescending(p => p.Persons.Count).FirstOrDefault()}");


            //Adicionando ao grupo de características possíveis o grupo que foi perguntado ao usuário
            possibleChars.Add(groups.OrderByDescending(p => p.Persons.Count).FirstOrDefault());


            Console.WriteLine("\nSábio: Esses são os possíveis personagens de acordo com a sua resposta:\n");
            Console.WriteLine("====================");

            foreach (AttrGroup g in possibleChars)
            {

                foreach (Person p in g.Persons)
                {
                    Console.WriteLine(p.Name);
                }

            }


            Console.WriteLine("====================");
            Console.Write("\nAperte enter para continuar. ");

            //Verificando escolha
            choice = VerifyKey();
            if (choice == 2) return;



            Console.Clear();





            //Fazendo a pergunta do grupo atual.

            List<Person> actualPersons = possibleChars.First().Persons.ToList();


            

            questions = group.GenerateGroupQuestions(actualPersons);

            foreach(string dic in questions.Values)
            {
                Console.WriteLine(dic);
            }

            Console.ReadLine();


            groups = person.GroupAndCount(actualPersons, questions);


            Console.ReadLine();


            //person.GroupAndCount(list);

            //Console.Clear();



            Console.Write("O seu personagem é o vilão do filme em que ele participa? (s/n): ");
            //Verificando escolha
            choice = VerifyKey();
            if (choice == 2) return;


            if (choice == 1)
            {
                Console.Clear();
                //Console.WriteLine($"O seu personagem é o : {possibleChars.Where(p => p.IsVillain == true).First().Name}");
            }
            else if (choice == 0)
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
        else if (choice == 0)
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
            
            return 2;
        }
        else
        {

            if(keyChar == 's')
            {
                return 1;
            }
            else if(keyChar == 'n')
            {
                return 0;
            }
            else
            {
                return 3;
            }

        }

    }

}