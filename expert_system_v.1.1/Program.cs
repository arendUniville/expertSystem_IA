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
        Dictionary<string, string> questionsOk = new Dictionary<string, string>();

        bool showPersons = false;
        bool showGroups = false;
        bool showMessages = true;
        bool showGeneratedQuestions = true;


        Console.Write("Bem vindo ao Sábio. Aperte qualquer tecla para continuar.");

        //Verificando escolha
        int choice = VerifyKey();
        if (choice == 2) return;


        //Gerando persons
        List<Person> list = persons.GeneratePersons();


        //Mostrando persons
        if (showPersons)
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


        Dictionary<string, string> questions = group.GenerateGroupQuestions(list, questionsOk);
        List<AttrGroup> groups = person.GroupAndCount(list, questions);



        //Mostrando todos os grupos
        if (showGroups)
        {

            Console.WriteLine("\nSábio: Pelas características dos personagens que você criou, eu achei melhor separar eles da seguinte forma:\n\n");
            group.ShowAllGourps(groups);


            //Verificando escolha
            choice = VerifyKey();
            if (choice == 2) return;
            Console.Clear();


        }



        //Declarando grupos
        AttrGroup groupPower = groups.Where(p => p.Nome == "HavePower").FirstOrDefault();
        AttrGroup groupVillain = groups.Where(v => v.Nome == "IsVillain").FirstOrDefault();
        AttrGroup groupMonster = groups.Where(p => p.Nome == "IsMonster").FirstOrDefault();
        AttrGroup groupAnimal = groups.Where(p => p.Nome == "IsAnimal").FirstOrDefault();



        //Verificando grupo com mais persons
        AttrGroup majorGroup = person.GetMajorGroup
            (
                groupPower,
                groupVillain,
                groupMonster,
                groupAnimal,

                questionsOk,

                showMessages
            );


        Console.WriteLine("STEP 1");

        //Inicia mostrando a pergunta do grupo no qual pertence.
        Console.Write($"Sábio: O personagem que você escolheu {questions[majorGroup.Nome]} (s/n): ");
        questionsOk.Add(majorGroup.Nome, questions[majorGroup.Nome]);

        //Verificando escolha
        choice = VerifyKey();
        if (choice == 2) return;


        Console.Clear();


        //Iniciando lista de possíveis características
        List<AttrGroup> possibleGroups = new List<AttrGroup>();

        //Limpando lista de personagens
        list.Clear();


        if (choice == 1)
        {

            //Mostrando o grupo que foi perguntado ao usuário
            //Console.WriteLine($"{groups.OrderByDescending(p => p.Persons.Count).FirstOrDefault()}");


            //Adicionando ao grupo de características possíveis o grupo que foi perguntado ao usuário
            possibleGroups.Add(groups.OrderByDescending(p => p.Persons.Count).FirstOrDefault());


            Console.WriteLine("\nSábio: Esses são os possíveis personagens de acordo com a sua resposta:\n");
            Console.WriteLine("====================");

            foreach (AttrGroup g in possibleGroups)
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


            List<Person> actualPersons = new List<Person>();


            //Fazendo a pergunta do grupo atual.



            foreach (AttrGroup g in groups)
            {

                List<Person> act = new List<Person>();

                Console.WriteLine($"Group name: {g.Nome}");

                if (!questionsOk.ContainsKey(g.Nome))
                {
                    act = g.Persons.ToList();

                    foreach (Person p in act)
                    {
                        actualPersons.Add(p);
                    }

                }


            }



            //Gerando perguntas
            questions = group.GenerateGroupQuestions(actualPersons, questionsOk);
            //Mostrando perguntas geradas
            if (showGeneratedQuestions)
            {

                foreach (string q in questions.Values)
                {
                    Console.WriteLine(q);
                }

                Console.WriteLine("Gerou as perguntas.");
                //Verificando escolha
                choice = VerifyKey();
                if (choice == 2) return;
            }


            //Gerando grupos
            groups = person.GroupAndCount(actualPersons, questions);
            //Mostrando todos os grupos
            if (showGroups)
            {

                Console.WriteLine("\nSábio: Pelas características dos personagens que você criou, eu achei melhor separar eles da seguinte forma:\n\n");
                group.ShowAllGourps(groups);


                //Verificando escolha
                choice = VerifyKey();
                if (choice == 2) return;
                Console.Clear();

            }


            Console.Clear();


            //Verificando grupo com mais persons
            majorGroup = person.GetMajorGroup
                (
                    groupPower,
                    groupVillain,
                    groupMonster,
                    groupAnimal,

                    questionsOk,

                    showMessages
                );



            //Inicia mostrando a pergunta do grupo no qual pertence.
            Console.Write($"Sábio: O personagem que você escolheu {questions[majorGroup.Nome]} (s/n): ");
            questionsOk.Add(majorGroup.Nome, questions[majorGroup.Nome]);


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

            //Removendo o major group da lista de groups
            foreach(AttrGroup g in groups)
            {
                if(g.Nome != majorGroup.Nome)
                {
                    possibleGroups.Add(g);   
                }
            }


            //Adicionando possíveis grupos
            list = person.PossiblePersons(groups, majorGroup);


            /*
            //Adicionando possíveis grupos
            foreach (AttrGroup g in groups)
            {

                if (g.Nome != majorGroup.Nome)
                {

                    foreach (Person p in g.Persons)
                    {

                        bool discarted = false;

                        //Verifica se possuí atributo do maior grupo anterior
                        if (nameof(p.HavePower) == majorGroup.Nome)
                        {
                            //Caso seja o atributo do maior grupo anterior, verifica se o atributo é verdadeiro
                            if (p.HavePower)
                            {
                                //Se for verdadeiro, o personagem pode ser descartado (Objetivo é remover os personagens que possuem o atributo do grupo anterior)
                                discarted = true;
                            }
                        }

                        if (nameof(p.IsVillain) == majorGroup.Nome)
                        {
                            if (p.IsVillain)
                            {
                                discarted = true;
                            }
                        }

                        if (nameof(p.IsMonster) == majorGroup.Nome)
                        {
                            if (p.IsMonster)
                            {
                                discarted = true;
                            }
                        }

                        if (nameof(p.IsAnimal) == majorGroup.Nome)
                        {
                            if (p.IsAnimal)
                            {
                                discarted = true;
                            }
                        }



                        //Caso o personagem não tenha nenhum atributo do grupo anterior.
                        if (!discarted)
                        {

                            //Verifica se possuí o atributo
                            if (p.HavePower)
                            {

                                //Tenta encontrar o personagem na lista atual
                                bool exist = list.Any(per => per.Name == p.Name);


                                //Caso não foi encontrado na lista
                                if (!exist)
                                {
                                    //Adiciona o personagem a lista
                                    list.Add(p);
                                    Console.WriteLine($"{g.Nome} is added by {p.Name}.");
                                }
                            }

                            if (p.IsVillain)
                            {

                                bool exist = list.Any(per => per.Name == p.Name);

                                if (!exist)
                                {
                                    list.Add(p);
                                    Console.WriteLine($"{g.Nome} is added by {p.Name}.");
                                }
                            }


                            if (p.IsMonster)
                            {

                                bool exist = list.Any(per => per.Name == p.Name);

                                if (!exist)
                                {
                                    list.Add(p);
                                    Console.WriteLine($"{g.Nome} is added by {p.Name}.");
                                }
                            }

                            if (p.IsAnimal)
                            {

                                bool exist = list.Any(per => per.Name == p.Name);

                                if (!exist)
                                {
                                    list.Add(p);
                                    Console.WriteLine($"{g.Nome} is added by {p.Name}.");
                                }
                            }

                        }

                    }

                }

            }

            */
            
            Console.WriteLine("\nSábio: Esses são os possíveis personagens de acordo com a sua resposta:\n");
            Console.WriteLine("====================");


            foreach (Person p in list)
            {
                Console.WriteLine(p.Name);
            }


            Console.WriteLine("====================");
            Console.Write("\nAperte enter para continuar. ");

            //Verificando escolha
            choice = VerifyKey();
            if (choice == 2) return;



            Console.Clear();


            List<Person> actualPersonsQuestions = new List<Person>();





            //Passa por cada grupo e verifica se a pergunta desse grupo já não foi chamada, se não foi, ele vai pegar
            //os pesonagens e adicionando em uma lista para que em seguida seja gerado o grupo de perguntas.
            foreach (AttrGroup g in groups)
            {

                List<Person> act = new List<Person>();

                Console.WriteLine($"Group name: {g.Nome}");

                if (!questionsOk.ContainsKey(g.Nome))
                {
                    act = g.Persons.ToList();

                    foreach (Person p in act)
                    {
                        actualPersonsQuestions.Add(p);
                    }

                }

            }



            //Gerando perguntas
            questions = group.GenerateGroupQuestions(actualPersonsQuestions, questionsOk);
            //Mostrando perguntas geradas
            if (showGeneratedQuestions)
            {

                foreach (string q in questions.Values)
                {
                    Console.WriteLine(q);
                }

                Console.WriteLine("Gerou as perguntas.");
                //Verificando escolha
                choice = VerifyKey();
                if (choice == 2) return;
            }


            //Gerando grupos
            groups = person.GroupAndCount(actualPersonsQuestions, questions);
            //Mostrando todos os grupos
            if (showGroups)
            {

                Console.WriteLine("\nSábio: Pelas características dos personagens que você criou, eu achei melhor separar eles da seguinte forma:\n\n");
                group.ShowAllGourps(groups);


                //Verificando escolha
                choice = VerifyKey();
                if (choice == 2) return;
                Console.Clear();

            }


            Console.Clear();


            //Verificando grupo com mais persons
            majorGroup = person.GetMajorGroup
                (
                    groupPower,
                    groupVillain,
                    groupMonster,
                    groupAnimal,

                    questionsOk,

                    showMessages
                );



            //Inicia mostrando a pergunta do grupo no qual pertence.
            Console.Write($"Sábio: O personagem que você escolheu {questions[majorGroup.Nome]} (s/n): ");
            questionsOk.Add(majorGroup.Nome, questions[majorGroup.Nome]);



            //Mostrando o grupo com mais persons vinculados
            Console.WriteLine($"{groups.OrderByDescending(p => p.Persons.Count).FirstOrDefault()}");


            //Pega o grupo com mais persons vinculados
            possibleGroups.Add(groups.OrderByDescending(p => p.Persons.Count).FirstOrDefault());


            Console.WriteLine("Possiveis chars:\n");

            foreach (AttrGroup g in possibleGroups)
            {

                foreach (Person p in g.Persons)
                {
                    Console.WriteLine(p.Name);
                }

            }



            Console.ReadLine();

            Console.Clear();

            Console.WriteLine("Aqui 1:");

            //questions = group.GenerateGroupQuestions(possibleChars.First().Persons.ToList());


            Console.WriteLine("Aqui 2:");

            groups = person.GroupAndCount(possibleGroups.First().Persons.ToList(), questions);


        }
        else if (choice == 3)
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

            if (keyChar == 's')
            {
                return 1;
            }
            else if (keyChar == 'n')
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