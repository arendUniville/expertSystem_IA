using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace expert_system_v._1._0.Entities;

class Person
{

    public int? Id { get; set; }
    public string? Name { get; set; }

    public string? Movie { get; set; }
    public string? CorCabelo { get; set; }
    public bool IsAnimal { get; set; } = false;
    public bool IsMonster { get; set; } = false;
    public bool IsVillain { get; set; } = false;
    public bool HavePower { get; set; } = false;
    public string? UniqueFeature { get; set; }




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
        List<Person> allPower = list.Where(p => p.HavePower).ToList();
        if( cntPower > 0)
        {
            groupPersons.Add(
                new AttrGroup("HavePower", cntPower, questions["HavePower"], allPower)
            );
        }

        if( cntVillain > 0)
        {
            List<Person> allVillain = list.Where(p => p.IsVillain).ToList();
            groupPersons.Add(
                new AttrGroup("IsVillain", cntVillain, questions["IsVillain"], allVillain)
            );
        }

        if( cntMonster > 0)
        {
            List<Person> allMonster = list.Where(p => p.IsMonster).ToList();
            groupPersons.Add(
                new AttrGroup("IsMonster", cntMonster, questions["IsMonster"], allMonster)
            );
        }

        if( cntAnimal > 0)
        {
            List<Person> allAnimal = list.Where(p => p.IsAnimal).ToList();
            groupPersons.Add(
                new AttrGroup("IsAnimal", cntAnimal, questions["IsAnimal"], allAnimal)
            );
        }

        
        Console.Write("\nAperte enter para continuar: ");
        Console.ReadLine();


        return groupPersons;

    }


    public string GetMajorGroup(AttrGroup? gPower, AttrGroup? gVillain, AttrGroup? gMonster, AttrGroup? gAnimal)
    {

        int power;
        int villain;
        int monster;
        int animal;


        power = gPower != null ? gPower.Persons.Count() : 0;
        
        villain = gVillain != null ? gVillain.Persons.Count() : 0; 
        monster = gMonster != null ? gMonster.Persons.Count() : 0;
        animal = gAnimal != null ? gAnimal.Persons.Count() : 0; 

        var majorityGroup = "";
        int majorGroupValue = 0;


        if (power > majorGroupValue)
        {
            majorGroupValue = power;
            majorityGroup = "HavePower";
        }

        if(villain > majorGroupValue)
        {
            majorGroupValue = villain;
            majorityGroup = "IsVillain";
        }

        if (monster > majorGroupValue)
        {
            majorGroupValue = monster;
            majorityGroup = "IsMonster";
        }

        if (animal > majorGroupValue)
        {
            majorGroupValue = animal;
            majorityGroup = "IsAnimal";
        }



        Console.WriteLine($"Total de personagens com poderes: {power}");
        Console.WriteLine($"Total de personagens vilões: {villain}");
        Console.WriteLine($"Total de personagens monstros: {monster}");
        Console.WriteLine($"Total de personagens animais: {animal}");



        return majorityGroup;


    }


    public override string ToString()
    {
        return $"--{Id}---------------" + 
               $"\nNome: {Name}\nFilme: {Movie}\nCor do cabelo: {CorCabelo}\nAnimal: {IsAnimal}\nMonstro: {IsMonster}\nVilão: {IsVillain}\nPoderes: {HavePower}\n\nCaracteristica única: {UniqueFeature}\n" + 
               "--x---------------\n";
    }

}
