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
        AttrGroup majorGroup = new AttrGroup();

        Dictionary<string, string> questionsOk = new Dictionary<string, string>();

        bool showPersons = false;
        bool showGroups = false;
        bool showMessages = false;
        bool showGeneratedQuestions = false;

        int phase = 1;
        bool lastGroup = false;

        bool personFinded = false;

        bool exceptionPerson = false;
        AttrGroup exceptionPersonObj = new AttrGroup();

        List<Person> list = new List<Person>();


        Dictionary<string, string> questions = new Dictionary<string, string>();

        List<AttrGroup> groups = new List<AttrGroup>();











        Console.Write("Bem vindo ao Sábio. Aperte qualquer tecla para continuar.");

        //Verificando escolha
        int choice = VerifyKey();
        if (choice == 2) return;




//(1)---Gerando persons
        if (phase == 1)
            list = persons.GeneratePersons();

        else if (phase == 2)
            Console.WriteLine($"Phase {phase}");

        else if (phase == 3)
            Console.WriteLine($"Phase {phase}");

        else
            Console.WriteLine($"An error occured in generate persons. Phase {phase} - Step 1");


        Console.WriteLine($"Phase {phase}\n\n");


        //Mostrando persons
        if (showPersons)
        {
            person.ShowMyPersons(list);
            Console.Write("\n\nEsse são os seus personagens. Clique enter para continuar.");


            //Verificando escolha
            choice = VerifyKey();
            if (choice == 2) return;


            Console.Clear();
            Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n");

        }





        while (!personFinded)
        {

            exceptionPerson = false;
            majorGroup = null;

//(2)---Gerando perguntas
            questions = group.GenerateGroupQuestions(list, questionsOk, phase);




//(3)---Gerando grupos de características
            
            groups = person.GroupAndCount(list, questions, phase);
            


            Console.Clear();
            Console.WriteLine("Totais: ");
            foreach (AttrGroup g in groups)
            {
                Console.WriteLine($"Grupo {g.Nome} | Total: {g.Total}");
            }


            Console.WriteLine($"Quantidade total de grupos: {groups.Count}");

            //Verificando escolha
            choice = VerifyKey();
            if (choice == 2) return;




//(3.2)---Gerando grupos de características

            //Verificando se restam dois ou menos grupos
            if (groups.Count == 1)
            {
                lastGroup = true;
                phase = 2;

                Console.WriteLine($"Entrou em apenas 1 grupo. Fase {phase}");

            }
            else if (groups.Count == 2)
            {

                Console.Clear();
                Console.WriteLine("Scanning Groups...");


                //Buscando algum grupo que possua apenas 1 person
                List<Person> lastPersonList = person.ScanGroups(groups, questionsOk);

                
                
                Thread.Sleep(1300);



                //Validando se grupo com apenas 1 person foi encontrado
                if (lastPersonList != null)
                {
                    Console.WriteLine($"Personagem {lastPersonList.First().Name} possuí apenas 1 atributo disponível.");
                    exceptionPerson = true;


                    //Buscando a pergunta do grupo do personagem exception
                    exceptionPersonObj = groups.Where(g => g.Total == 1).First();

                }
                else
                {
                    Console.WriteLine("Nenhum personagem restando apenas 1 atributo foi encontrado.");
                }




                //Verificando escolha
                choice = VerifyKey();
                if (choice == 2) return;

            }


            Console.Clear();
            Console.WriteLine("Getting Major Group...");

            Thread.Sleep(1300);

            Console.Clear();




            //Mostrando todos os grupos
            if (showGroups)
            {

                Console.WriteLine("\n\nSábio: Pelas características dos personagens que você criou, eu achei melhor separar eles da seguinte forma:\n\n");
                group.ShowAllGourps(groups);


                //Verificando escolha
                choice = VerifyKey();
                if (choice == 2) return;
                Console.Clear();


            }


//(4)---Buscando grupo de características com mais personagens
            
            if (phase == 1)
            {
                //Caso não exista um personagem exceção (Único restante do grupo)
                if (!exceptionPerson)
                {
                    majorGroup = person.GetMajorGroup(groups, questionsOk, showMessages);
                }
                else
                {
                    //Atribuindo exception ao majorGroup
                    majorGroup = exceptionPersonObj;
                }
            }
            else if (phase == 2) 
            {
                Console.Clear();
                Console.WriteLine("Phase 2");
            }



//(5)---Faz a pergunta do grupo encontrado no passo 4
            if(phase == 1)
            {
                Console.Write($"Sábio: O personagem que você escolheu {questions[majorGroup.Nome]}? (s/n): ");
                questionsOk.Add(majorGroup.Nome, questions[majorGroup.Nome]);
            }
            else if(phase == 2)
            {
                Console.Clear();
                Console.WriteLine("Phase 2");
            }




            //Verificando escolha
            choice = VerifyKey();
            if (choice == 2) return;


            Console.Clear();


            //Limpando lista de personagens
            list.Clear();


            if (choice == 1)
            {

                //Mostrando o grupo que foi perguntado ao usuário
                //Console.WriteLine($"{groups.OrderByDescending(p => p.Persons.Count).FirstOrDefault()}");


                //Adicionando ao grupo de características possíveis o grupo que foi perguntado ao usuário
                if(!exceptionPerson)
                {
                    list = person.RemovePersonOfFalseAttr(groups.OrderByDescending(p => p.Persons.Count).FirstOrDefault().Persons.ToList());
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("===========\nEm desenvolvimento\n===========\n");
                    list = person.RemovePersonOfFalseAttr(majorGroup.Persons.ToList());

                    //Verificando escolha
                    choice = VerifyKey();
                    if (choice == 2) return;

                }


                if (person.IsLastPerson(list))
                {
                    personFinded = true;

                    person.ShowWinnerChoice(list);

                    return;
                }


                //Mostrando possíveis personagens
                person.ShowPossiblePersons(list);


                //Verificando escolha
                choice = VerifyKey();
                if (choice == 2) return;



                Console.Clear();


            }
            else if (choice == 0)
            {

                //Adicionando possíveis grupos
                list = person.RemovePersonOfMajorGroup(groups, majorGroup, choice);

                //Verifica se existe apenas 1 possibilidade.
                if (person.IsLastPerson(list))
                {
                    personFinded = true;

                    person.ShowWinnerChoice(list);

                    return;
                }

                //Mostrando possíveis personagens
                person.ShowPossiblePersons(list);


                //Verificando escolha
                choice = VerifyKey();
                if (choice == 2) return;



            }
            else if (choice == 3)
            {
                Console.Clear();
                Console.WriteLine("Aperte uma tecla possível para continuar.");
            }


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