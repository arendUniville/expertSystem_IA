using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace expert_system_v._1._0.Entities;

class AttrGroup
{

    public string? Nome { get; set; }
    public int? Total { get; set; }
    public string? Pergunta { get; set; }

    public List<Person>? Persons { get; set; }




    public AttrGroup() { }

    public AttrGroup(string nome, int total, string pergunta, List<Person> persons)
    {
        Nome = nome;
        Total = total;
        Pergunta = pergunta;
        Persons = persons;
    }



    public Dictionary<string, string> GenerateGroupQuestions(List<Person> persons, Dictionary<string, string> lasGroup, int phase)
    {

        Dictionary<string, string> questions = new Dictionary<string, string>();

        if (phase == 1)
        {
            foreach (Person person in persons)
            {

                if (person.HavePower)
                {

                    if (!lasGroup.ContainsKey("HavePower"))
                    {
                        if (!questions.ContainsKey("HavePower"))
                        {
                            questions.Add("HavePower", "possuí algum poder?");
                            //Console.WriteLine(questions["HavePower"]);
                        }
                    }


                }

                if (person.IsVillain)
                {
                    if (!lasGroup.ContainsKey("IsVillain"))
                    {

                        if (!questions.ContainsKey("IsVillain"))
                        {
                            questions.Add("IsVillain", "é um vilão no filme que ele participa?");
                            //Console.WriteLine(questions["IsVillain"]);
                        }

                    }
                }

                if (person.IsMonster)
                {
                    if (!lasGroup.ContainsKey("IsMonster"))
                    {

                        if (!questions.ContainsKey("IsMonster"))
                        {
                            questions.Add("IsMonster", "é um monstro?");
                            //Console.WriteLine(questions["IsMonster"]);
                        }

                    }
                }

                if (person.IsAnimal)
                {
                    if (!lasGroup.ContainsKey("IsAnimal"))
                    {
                        if (!questions.ContainsKey("IsAnimal"))
                        {
                            questions.Add("IsAnimal", "é um animal?");
                            //Console.WriteLine(questions["IsAnimal"]);
                        }

                    }
                }

            }
        }

        return questions;

    }



    public void ShowAllGourps(List<AttrGroup> groups)
    {

        int cnt = 1;

        foreach (AttrGroup g in groups)
        {
            Console.WriteLine($"Grupo [{cnt}]:\n{g.ToString()}");
            cnt++;
        }

    }


    public override string ToString()
    {

        string resultado = "";

        int cnt = 0;
        int markerBreak = 0;
        int skip = 3;

        foreach (Person p in Persons)
        {
            if(cnt == 0)
            {
                resultado += "\n[\n " + p.Name + ", ";
            }
            else if(cnt > (markerBreak + skip))
            {
                resultado += " " + p.Name + ", \n";
                skip += 3;
            }
            else
            {
                resultado += " " + p.Name + ", ";
            }

            cnt++;
        }

        

        return $"Atributo: {Nome}\nTotal: {Total}\nPergunta: {Pergunta}" +
               $"\nPersons:{resultado}\n]\n";
    }


}
