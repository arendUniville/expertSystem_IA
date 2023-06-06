using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace expert_system_v._1._0.Entities;

class Person
{

    public int? Id { get; set; }
    public string Name { get; set; }

    public string Movie { get; set; }
    public string CorCabelo { get; set; } //Pode ser do cabelo quanto do pelo do animal
    public bool IsAnimal { get; set; } = false;
    public bool IsMonster { get; set; } = false;
    public bool IsVillain { get; set; } = false;
    public bool HavePower { get; set; } = false;
    public string UniqueFeature { get; set; }




    public Person() { }

    public Person(int id, string name, string movie, string corCabelo, bool isAnimal, bool isMonster, bool isVillain, string uniqueFeature)
    {
        Id = id;
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Movie = movie ?? throw new ArgumentNullException(nameof(Movie));
        CorCabelo = corCabelo ?? throw new ArgumentNullException(nameof(corCabelo));
        IsAnimal = isAnimal;
        IsMonster = isMonster;
        IsVillain = isVillain;
        UniqueFeature = uniqueFeature;
    }

    public Person(int id, string name, string movie, string corCabelo, bool isAnimal, bool isMonster, bool isVillain, bool havePower, string uniqueFeature)
        : this(id, name, movie, corCabelo, isAnimal, isMonster, isVillain, uniqueFeature)
    {
        HavePower = havePower;
    }



    //Verify
    public bool IsLastPerson(List<Person> list)
    {

        if (list.Count == 1)
        {
            return true;
        }
        else { return false; }

    }

    public void ShowWinnerChoice(List<Person> list)
    {
        Console.WriteLine($"O personagem escolhido é: {list.FirstOrDefault().Name}");
    }


    //Groups
    public List<AttrGroup> GroupAndCount(List<Person> list, Dictionary<string, string> questions)
    {

        List<AttrGroup> groupPersons = new List<AttrGroup>();
        AttrGroup groups;


        //Fazendo contagem
        int cntPower = list.Where(p => p.HavePower).Count();
        int cntVillain = list.Where(p => p.IsVillain).Count();
        int cntMonster = list.Where(p => p.IsMonster).Count();
        int cntAnimal = list.Where(p => p.IsAnimal).Count();


        //Atribuindo grupos
        if (cntPower > 0)
        {
            if (questions.ContainsKey("HavePower"))
            {
                List<Person> allPower = list.Where(p => p.HavePower).ToList();
                groupPersons.Add(
                    new AttrGroup("HavePower", cntPower, questions["HavePower"], allPower)
                );
            }
        }

        if (cntVillain > 0)
        {
            if (questions.ContainsKey("IsVillain"))
            {

                List<Person> allVillain = list.Where(p => p.IsVillain).ToList();
                groupPersons.Add(
                    new AttrGroup("IsVillain", cntVillain, questions["IsVillain"], allVillain)
                );

            }
        }

        if (cntMonster > 0)
        {
            if (questions.ContainsKey("IsMonster"))
            {
                List<Person> allMonster = list.Where(p => p.IsMonster).ToList();
                groupPersons.Add(
                    new AttrGroup("IsMonster", cntMonster, questions["IsMonster"], allMonster)
                );
            }

        }

        if (cntAnimal > 0)
        {
            if (questions.ContainsKey("IsAnimal"))
            {

                List<Person> allAnimal = list.Where(p => p.IsAnimal).ToList();
                groupPersons.Add(
                    new AttrGroup("IsAnimal", cntAnimal, questions["IsAnimal"], allAnimal)
                );

            }

        }


        return groupPersons;

    }

    public AttrGroup GetMajorGroup(List<AttrGroup> groups, Dictionary<string, string> questionOk, bool msg)
    {


        AttrGroup gPower = groups.Where(p => p.Nome == "HavePower").FirstOrDefault();
        AttrGroup gVillain = groups.Where(v => v.Nome == "IsVillain").FirstOrDefault();
        AttrGroup gMonster = groups.Where(p => p.Nome == "IsMonster").FirstOrDefault();
        AttrGroup gAnimal = groups.Where(p => p.Nome == "IsAnimal").FirstOrDefault();


        int power;
        int villain;
        int monster;
        int animal;


        power = gPower != null ? gPower.Persons.Count() : 0;

        villain = gVillain != null ? gVillain.Persons.Count() : 0;
        monster = gMonster != null ? gMonster.Persons.Count() : 0;
        animal = gAnimal != null ? gAnimal.Persons.Count() : 0;

        AttrGroup majorityGroup = null;
        int majorGroupValue = 0;


        if (power > majorGroupValue)
        {
            if (!questionOk.ContainsKey("HavePower"))
            {
                majorGroupValue = power;
                majorityGroup = gPower;
            }
        }

        if (villain > majorGroupValue)
        {
            if (!questionOk.ContainsKey("IsVillain"))
            {
                majorGroupValue = villain;
                majorityGroup = gVillain;
            }

        }

        if (monster > majorGroupValue)
        {
            if (!questionOk.ContainsKey("IsMonster"))
            {
                majorGroupValue = monster;
                majorityGroup = gMonster;
            }
        }

        if (animal > majorGroupValue)
        {
            if (!questionOk.ContainsKey("IsAnimal"))
            {
                majorGroupValue = animal;
                majorityGroup = gAnimal;
            }
        }


        if (msg)
        {
            Console.WriteLine($"Total de personagens com poderes: {power}");
            Console.WriteLine($"Total de personagens vilões: {villain}");
            Console.WriteLine($"Total de personagens monstros: {monster}");
            Console.WriteLine($"Total de personagens animais: {animal}");

        }



        return majorityGroup;

    }



    //Verificar personagens
    public List<Person> ScanGroups(List<AttrGroup> groups, Dictionary<string, string> questionsOk)
    {

        List<Person> pResult = new List<Person>();
        AttrGroup minorGroup = null;



        //Buscando o menor grupo
        foreach (AttrGroup g in groups)
        {
            if (g.Total == 1)
            {
                pResult.Add(g.Persons.First());
            }
        }


        if (pResult != null)
        {
            if(pResult.Count == 1) 
            {
                return pResult;
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }


        //Verificando se existe personagem diferente de todos

        /*
        foreach (Person p in minorGroup.Persons)
        {

            int countAttr = 0;


            //Verificando quantos atributos restaram
            if(p.HavePower) 
            {
                if (!questionsOk.ContainsKey("HavePower"))
                {
                    countAttr++;
                }
            }

            if (p.IsVillain)
            {
                if (!questionsOk.ContainsKey("IsVillain"))
                {
                    countAttr++;
                }
            }

            if(p.IsMonster)
            {
                if (!questionsOk.ContainsKey("IsMonster"))
                {
                    countAttr++;
                }
            }

            if(p.IsAnimal)
            {
                if (!questionsOk.ContainsKey("IsAnimal"))
                {
                    countAttr++;
                }
            }


            if(countAttr == 1)
            {
                pResult.Add(p);
            }


        }

        if(pResult == null)
        {
            return null;
        }
        else if(pResult.Count > 1)
        {
            return null;
        }
        else
        {
            return pResult;
        }

        */


    }


    public Dictionary<string, string> GeneratePersonQuestion(List<Person> persons)
    {

        Dictionary<string, string> questions = new Dictionary<string, string>();


        foreach (Person p in persons)
        {

            //Validando atributo cabelo
            if(p.CorCabelo != "None")
            {
                if (p.CorCabelo == "Loiro")
                {
                    if (p.IsAnimal)
                    {
                        if (!questions.ContainsKey("CorPelo_Loiro"))
                        {
                            questions.Add("CorPelo_Loiro", "é um animal com pelo loiro");
                        }
                    }
                    else
                    {
                        if (!questions.ContainsKey("CorCabelo_Loiro"))
                        {
                            questions.Add("CorCabelo_Loiro", "tem cabelo loiro");
                        }
                    }
                }

                if (p.CorCabelo == "Castanho")
                {

                    if (p.IsAnimal)
                    {
                        if (!questions.ContainsKey("CorPelo_Castanho"))
                        {
                            questions.Add("CorPelo_Castanho", "é um animal com pelo castanho");
                        }
                    }
                    else
                    {
                        if (!questions.ContainsKey("CorCabelo_Castanho"))
                        {
                            questions.Add("CorCabelo_Castanho", "tem cabelo castanho");
                        }
                    }

                }

                if (p.CorCabelo == "Preto")
                {
                    if (p.IsAnimal)
                    {
                        if (!questions.ContainsKey("CorPelo_Preto"))
                        {
                            questions.Add("CorPelo_Preto", "é um animal com pelo preto");
                        }
                    }
                    else
                    {
                        if (!questions.ContainsKey("CorCabelo_Preto"))
                        {
                            questions.Add("CorCabelo_Preto", "tem cabelo preto");
                        }
                    }
                }

                if (p.CorCabelo == "Ruivo")
                {
                    if (p.IsAnimal)
                    {
                        if (!questions.ContainsKey("CorPelo_Ruivo"))
                        {
                            questions.Add("CorPelo_Preto", "é um animal com pelo ruivo");
                        }
                    }
                    else
                    {
                        if (!questions.ContainsKey("CorCabelo_Ruivo"))
                        {
                            questions.Add("CorCabelo_Ruivo", "tem cabelo ruivo");
                        }
                    }
                }
            }
            else
            {
                if (p.IsAnimal)
                {
                    if (!questions.ContainsKey("SemPelo"))
                    {
                        questions.Add("SemPelo", "possuí pelo");
                    }
                }
                else
                {
                    if (!questions.ContainsKey("SemCabelo"))
                    {
                        questions.Add("SemCabelo", "possuí cabelo");
                    }
                }
            }


            //Validando atributo filme
            if(p.Movie != "None")
            {
                if(!questions.ContainsKey("ReiLeao"))
                {
                    questions.Add("ReiLeao", "faz parte do filme 'O Rei Leão'");
                }
                
                if(!questions.ContainsKey("VingadoresUltimato"))
                {
                    questions.Add("VingadoresUltimato", "faz parte do filme 'Vingadores Ultimato'");
                }
            }

        }


        return questions;

    }




//Remove or add
public List<Person> RemovePersonOfMajorGroup(List<AttrGroup> groups, AttrGroup majorGroup, int response)
{

    List<Person> list = new List<Person>();

    if (response == 0)
    {
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
    }
    else if (response == 1)
    {
        foreach (AttrGroup g in groups)
        {

            if (g.Nome == majorGroup.Nome)
            {

                foreach (Person p in g.Persons)
                {

                    bool discarted = false;

                    //Verifica se possuí atributo do maior grupo anterior
                    if (nameof(p.HavePower) != majorGroup.Nome)
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

    }

    return list;

}

public List<Person> RemovePersonOfFalseAttr(List<Person> persons)
{

    List<Person> list = new List<Person>();


    foreach (Person p in persons)
    {
        list.Add(p);
    }


    return list;

}




//Prints
public void ShowPossiblePersons(List<Person> list)
{

    Console.WriteLine("\nSábio: Esses são os possíveis personagens de acordo com a sua resposta:\n");
    Console.WriteLine("====================");


    foreach (Person p in list)
    {
        Console.WriteLine(p.Name);
    }


    Console.WriteLine("====================");
    Console.Write("\nAperte enter para continuar. ");

}
public void ShowMyPersons(List<Person> persons)
{

    foreach (Person p in persons)
    {
        Console.WriteLine(p.ToString());
    }

}

public override string ToString()
{
    return $"--{Id}---------------------------------------------------------------------------------------------" +
           $"\nNome: {Name}\nFilme: {Movie}\nCor do cabelo: {CorCabelo}\nAnimal: {IsAnimal}\nMonstro: {IsMonster}\nVilão: {IsVillain}\nPoderes: {HavePower}\n\nCaracteristica única: {UniqueFeature}\n" +
           "--x---------------------------------------------------------------------------------------------\n";
}

}
