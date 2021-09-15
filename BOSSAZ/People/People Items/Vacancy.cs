using System.Collections.Generic;
using System.Linq;

namespace BOSSAZ
{
    class Vacancy
    {
        public int ID { get; set; }

        public int EmployerID { get; set; } //ID of employer who post this vacancy
        public int Salary { get; set; }

        public int MinExperience { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public List<string> Skill { get; set; }

        public string Profession { get; set; }

        public List<string> Language { get; set; }

        public List<int> EmployeeID { get; set; }// id of employee who want this job

        private string SkillString()
        {
            string temp = "";
            Skill.ForEach(s => { 
                temp += s; 
                if (s != Skill.Last()) 
                    temp += ", "; 
            });
            return temp;
        }
        private string LanguageString()
        {
            string temp = "";
            Language.ForEach(l => { 
                temp += l; 
                if (l != Language.Last()) 
                    temp += ", "; 
            });
            return temp;
        }

        public override string ToString()
        {
            return $@"
Salary: {Salary}
{new string('-',16)}
Min Exprience: {MinExperience}
{new string('-',16)}
Profession: {Profession}
{new string('-',Profession.Length+12)}
Skill: {SkillString()}
{new string('-',SkillString().Length+8)}
Language: {LanguageString()}
{new string('-',LanguageString().Length+10)}
{Title}
{new string('-', Title.Length)}
{Body}
";
        }

        public Vacancy()
        {
            EmployeeID = new List<int>();
        }

    }
}
