using System.Collections.Generic;
using System.Linq;

namespace BOSSAZ
{
    class CV
    {
        public int Experience { get; set; }

        public string Profession { get; set; }
        
        public string School { get; set; }
        
        public List<string> Skill { get; set; }

        public List<string> Language { get; set; }

        public CV()
        {
            Skill = new List<string>();
            Language = new List<string>();
        }

        private string SkillString()
        {
            string temp = "";
            Skill.ForEach(s => {
                temp = s;
                if (s != Skill.Last())
                    temp += ", ";
            });
            return temp;
        }
        private string LanguageString()
        {
            string temp = "";
            Language.ForEach(l => {
                temp = l;
                if (l != Language.Last())
                    temp += ", ";
            });
            return temp;
        }


        public override string ToString()
        {
            return $@"
Profession: {Profession}
{new string('-', Profession.Length + 12)}
Education: {School} 
{new string('-', School.Length + 11)}
Experience: {Experience} 
{new string('-', 14)}
Skills: {SkillString()} 
{new string('-', SkillString().Length+8)}
Language: {LanguageString()}
{new string('-', LanguageString().Length+10)}";
        }
    }
}
