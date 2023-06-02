using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace expert_system_v._1._0.Entities;

class Person
{

    public int Id { get; set; }
    public string Name { get; set; }

    public string Movie { get; set; }
    public string CorCabelo { get; set; }
    public bool IsAnimal { get; set; }
    public bool IsMonster { get; set; }
    public bool IsVillain { get; set; }
    public bool HavePower { get; set; }
    public string UniqueFeature { get; set; }



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



    public override string ToString()
    {
        return $"--{Id}---------------" + 
               $"\nNome: {Name}\nFilme: {Movie}\nCor do cabelo: {CorCabelo}\nAnimal: {IsAnimal}\nMonstro: {IsMonster}\nVilão: {IsVillain}\nPoderes: {HavePower}\n" + 
               "--x---------------\n";
    }

}
