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


    public AttrGroup GetMajorGroup(AttrGroup gPower, AttrGroup gVillain, AttrGroup gMonster, AttrGroup gAnimal, Dictionary<string, string> questionOk, bool msg)
    {

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



    public List<Person> RemovePersonByMajorGroup(List<AttrGroup> groups, AttrGroup majorGroup)
    {

        List<Person> list = new List<Person>();


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


        return list;

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
