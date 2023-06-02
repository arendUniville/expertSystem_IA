using expert_system_v._1._0.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace expert_system_v._1._0.Service;

class PersonRepository
{

    //Classe feita apenas para gerar os personagens


    public List<Person> GeneratePersons()
    {

        List<Person> persons = new List<Person>();



        persons.Add(new Person(1, "Simba", "O Rei Leão", "None", true, false, false, "é o personagem principal do filme que ele participa"));
        persons.Add(new Person(2, "Timão", "O Rei Leão", "None", true, false, false, "é o amigo "));
        persons.Add(new Person(3, "Pumba", "O Rei Leão", "None", true, false, false, "não sei"));
        persons.Add(new Person(4, "Scar", "O Rei Leão", "None", true, false, true, "é o vilão principal do filme que ele participa"));

        persons.Add(new Person(5, "Homem de Ferro", "Vingadores Ultimato", "Preto", false, false, false, true, "é um personagem que usa de técnologias avançadas como armadura de defesa"));
        persons.Add(new Person(6, "Capitão America", "Vingadores Ultimato", "Loiro", false, false, false, true, "é um personagem que usa apenas um escudo e sua força como forma de defesa"));
        persons.Add(new Person(7, "Thor", "Vingadores Ultimato", "Loiro", false, false, false, true, "é um personagem que é um deus mitológico que utiliza um martelo como defesa"));
        persons.Add(new Person(8, "Viúva Negra", "Vingadores Ultimato", "Ruivo", false, false, false, true, "não sei"));
        persons.Add(new Person(9, "Hulk", "Vingadores Ultimato", "Preto", false, false, false, true, "é um personagem que caso fique irritado, pode virar uma fera verde"));
        persons.Add(new Person(10, "Gavião Arqueiro", "Vingadores Ultimato", "Castanho", false, false, false, true, "é um personagem que utiliza um arco e flecha como defesa"));
        persons.Add(new Person(11, "Homem-Aranha", "Vingadores Ultimato", "Castanho", false, false, false, true, "é um personagem que foi mordido por uma aranha"));
        persons.Add(new Person(12, "Thanos", "Vingadores Ultimato", "None", false, false, true, true, "é o vilão principal do último filme da saga"));




        return persons;

    }


}
