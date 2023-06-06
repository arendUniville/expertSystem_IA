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
        bool showMessages = false;
        bool showGeneratedQuestions = false;

        int phase = 1;
        bool lastGroup = false;

        bool personFinded = false;

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

//(2)---Gerando perguntas
            questions = group.GenerateGroupQuestions(list, questionsOk, phase);



//(3)---Gerando grupos de características
            if (phase == 1)
            {
                groups = person.GroupAndCount(list, questions);
            }


//(3.2)---Gerando grupos de características

            //Verificando se é o último grupo
            if (groups.Count == 1)
            {
                lastGroup = true;
                phase = 2;
            }


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


//(4)---Buscando grupo de características com mais personagens (Aqui pode ser melhorado passando uma lista de AttrGroup ao invés de um por um)
            AttrGroup majorGroup = person.GetMajorGroup(groups, questionsOk, showMessages);


//(5)---Faz a pergunta do grupo encontrado no passo 4
            Console.Write($"Sábio: O personagem que você escolheu {questions[majorGroup.Nome]} (s/n): ");
            questionsOk.Add(majorGroup.Nome, questions[majorGroup.Nome]);

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
                list = person.RemovePersonOfFalseAttr(groups.OrderByDescending(p => p.Persons.Count).FirstOrDefault().Persons.ToList());


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