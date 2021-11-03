using Menu;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BOSSAZ
{
    static class MainMethods
    {

        static string[] skills = new string[] { "C", "C++", "C#", "SQL", "JAVA", "PHP", "HTML5","CSS", "JavaScript", "Pyton","Microsoft Office", "Adobe Photoshop", "Adobe Illustrator", "Affinity Designer.", "Other", "End" };
        static string[] language = new string[] { "Azerbaijan", "English", "Russian", "Turkish", "French","Other", "End" };
        static string[] profession = new string[] { "Cyber security", "Frontend Developer", "Backend Developer", "Computer Engineer", "Graphic Designer","IT","Copy Writer","Other" };

        public static int SignUp(Users users)
        {
            int index = SelectingMenu.Choose(new string[] { "Employee", "Employer" ,"Back"});
            Console.Clear();
            if (index == 0) //Employee
            {
                var employee = new Employee();
                var cv = new CV();
                employee.FillInfo();
                Console.Clear();
                Console.WriteLine("CV:");
                Console.Write("Profession: ");
                Console.Clear();
                cv.Profession = SelectingMenu.ChooseByString(profession);
                Console.Clear();
                Console.Write("University: ");
                cv.School = Console.ReadLine();
                int a;
                while (true)
                {
                    Console.Write("Experience: ");
                    if (int.TryParse(Console.ReadLine(), out a))
                    {
                        cv.Experience = a;
                        break;
                    }
                }
                Console.Clear();
                List<string> skillList = new();
                while (true)
                {
                    string temp = SelectingMenu.ChooseByString(skills);
                    if (temp == "End")
                        break;
                    else 
                    {
                        skillList.Add(temp);
                    }
                }
                skillList = skillList.Distinct().ToList();
                Console.Clear();
                cv.Skill = skillList;
                List<string> lang = new();
                while (true)
                {
                    string temp = SelectingMenu.ChooseByString(language);
                    if (temp == "End")
                        break;
                    else
                    {
                        lang.Add(temp);
                    }
                }
                lang = lang.Distinct().ToList();
                cv.Language = lang;
                employee.Cvs = cv;
                users.SignUp(employee);
            }
            else if(index==1)//Employer
            {
                var employer = new Employer();
                employer.FillInfo();
                users.SignUp(employer);
            }
            return -1;
        }
        
        public static int PostVacancy(int id,Users users)
        {
            var vacancy = new Vacancy();
            Console.Clear();
            while (true)
            {
                Console.Write("Title (min 10): ");
                vacancy.Title = Console.ReadLine();
                if (vacancy.Title.Length > 9)
                    break;
                Console.WriteLine("Title length is short");
            }
            Console.Write("Body: ");
            vacancy.Body = Console.ReadLine();
            int a;
            while (true)
            {
                Console.Write("Salary: ");
                if (int.TryParse(Console.ReadLine(), out a))
                {
                    vacancy.Salary = a;
                    break;
                }
            }
            while (true)
            {
                Console.Write("Min Experience: ");
                if (int.TryParse(Console.ReadLine(), out a))
                {
                    vacancy.MinExperience = a;
                    break;
                }
            }
            Console.Clear();
            vacancy.Profession = SelectingMenu.ChooseByString(profession);
            Console.Clear();
            List<string> skillList = new();
            while (true)
            {
                string temp = SelectingMenu.ChooseByString(skills);
                if (temp == "End")
                    break;
                else
                {
                    skillList.Add(temp);
                }
            }
            skillList = skillList.Distinct().ToList();
            Console.Clear();
            vacancy.Skill = skillList;
            List<string> lang = new();
            while (true)
            {
                string temp = SelectingMenu.ChooseByString(language);
                if (temp == "End")
                    break;
                else
                {
                    lang.Add(temp);
                }
            }
            lang = lang.Distinct().ToList();
            vacancy.Language = lang;
            users.PostVacancyByID(id, vacancy);
            return 11;
        }

        public static int ShowYourPost(int id,Users users)
        {
            string[] posts = new string[users.GetEmployerByID(id).Vacancies.Count + 1];
            int i = 0;
            if (posts.Length > 1)
            {
                foreach (var vacancy in users.GetEmployerByID(id).Vacancies)
                {
                    posts[i++] = vacancy.Title + " " + vacancy.EmployeeID.Count;
                }
                posts[i] = "Back";
                return SelectingMenu.Choose(posts);
            }
            Console.WriteLine("Post Something\nPress any key to continue.");
            Console.ReadKey();
            return -1;
        }

        public static int ShowPost(int employeeID,List<Vacancy> vacancies,Users users)
        {
            int index = -1;
            int[] ids;
            while (true)
            {
                ids = Choose(vacancies);
                if (ids[0] == -1)
                    break;
                else
                {
                    var emp = users.GetEmployerByID(ids[1]);
                    string str = "Company Name: " + emp.CompanyName + emp.GetVacancyByID(ids[0]).ToString();
                    index = SelectingMenu.Choose(str,new string[] { "Apply for job", "Back" });
                    if(index == 0)
                    {
                        users.ApplyForJob(employeeID, ids[0], ids[1]);
                        break;
                    }
                }
            }
            return 21;
        }

        private static void MakeButton(string item, int maxLength)
        {
            if (maxLength % 2 != 0) maxLength += 7;
            else maxLength += 6;
            Console.Write(" ");
            Console.Write(new string('-', maxLength));
            Console.Write("\n");
            Console.Write("|");
            Console.Write(new string(' ', (maxLength - item.Length) / 2));
            Console.Write(item);
            Console.Write(new string(' ', maxLength - (item.Length + (maxLength - item.Length) / 2)));
            Console.Write("|\n");
            Console.Write(" ");
            Console.Write(new string('-', maxLength));
            Console.Write("\n");
        }

        private static int[] Choose(List<Vacancy> items)
        {
            int size = items.Count + 1;
            bool[] isChoosenLine = new bool[size];
            int index = 0;
            string[] arr = items.Select(it => it.Title).ToArray();
            Array.Resize(ref arr, arr.Length + 1);
            arr[arr.Length - 1] = "Back";
            while (true)
            {
                Console.Clear();
                int j = 0;
                int maxLength = items.Max(vac => vac.Title.Length);
                if (maxLength > 40) maxLength = 40;
                foreach (var item in arr)
                {
                    string temp = item;
                    if(temp.Length > 40)
                    {
                        temp = temp.Substring(0, 40);
                        temp += "...";
                    }
                    if (isChoosenLine[j])
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                    MakeButton(temp, maxLength);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    j++;
                }
                Console.ForegroundColor = ConsoleColor.Gray;
                ConsoleKeyInfo rKey = Console.ReadKey();
                if (rKey.Key == ConsoleKey.UpArrow)
                {
                    index--;
                    if (index == -1)
                        index = size - 1;
                }
                else if (rKey.Key == ConsoleKey.DownArrow)
                {
                    index++;
                    if (index == size)
                        index = 0;
                }
                else if (rKey.Key == ConsoleKey.Enter)
                {
                    if (index == size - 1)
                        return new int[] { -1, -1 };
                    return new int[] { items[index].ID, items[index].EmployerID };
                }
                for (int i = 0; i < size; i++)
                {
                    isChoosenLine[i] = false;
                }
                isChoosenLine[index] = true;

            }
        }

        public static int ShowEmployeeWhoWantJob(int employerID, int vacancyID, Users users)
        {
            int index;
            int index2;
            while (true)
            {
                string vacancy = users.GetEmployerByID(employerID).GetVacancyByID(vacancyID).ToString();
                int size = users.GetEmployerByID(employerID).GetVacancyByID(vacancyID).EmployeeID.Count + 2;
                string[] employee = new string[size];
                int i = 0;
                if (employee.Length > 2)
                {
                    foreach (var emp in users.EmployeeWantJob(employerID, vacancyID))
                    {
                        employee[i++] = emp.Name + " " + emp.Surname;
                    }
                }
                employee[i++] = "Delete Vacancy";
                employee[i] = "Back";
                index = SelectingMenu.Choose(vacancy, employee);
                if (index < size - 2)
                {
                    int[] ids = users.GetEmployerByID(employerID).GetVacancyByID(vacancyID).EmployeeID.ToArray();
                    string cv = users.GetEmployeeByID(ids[index]).Cvs.ToString();
                    index2 = SelectingMenu.Choose(cv, new string[] { "Hire", "Reject", "Back" });
                    if (index2 == 0) //Hire
                    {
                        users.HireForJob(ids[index], vacancyID, employerID);
                        break;
                    }
                    else if (index2 == 1) //Reject
                    {
                        users.RejectForJob(ids[index], vacancyID, employerID);
                        break;
                    }
                }
                if(index == size - 2) //Delete Vacancy
                {
                    users.DeleteVacancy(employerID, vacancyID);
                    break;
                }
                else if(index == size - 1) { break; }//Back
            }
            return 13;
        }

        public static List<Vacancy> SearchText(List<Vacancy> vacancies,string searchText)
        {
            searchText = searchText.ToLower();
            return vacancies.FindAll(vac =>
            {
                if (vac.Title.ToLower().Contains(searchText) || vac.Salary.ToString().Contains(searchText) || vac.MinExperience.ToString().Contains(searchText) || vac.Body.ToLower().Contains(searchText) || vac.Profession.ToLower().Contains(searchText) || vac.Skill.Contains(searchText) || vac.Language.Contains(searchText))
                    return true;
                return false;
            });
        }

        public static List<Vacancy> Filter(List<Vacancy> vacancies, Users users)
        {
            List<Vacancy> tempVacancy = new();
            SortedList<int, List<int>> filter = new SortedList<int, List<int>>();
            while (true)
            {
                List<string> texts = new();
                int index = SelectingMenu.Choose(new string[] { "Profession", "Salary", "Skill", "Language", "Back" });
                Console.Clear();
                switch (index)
                {
                    case 0: //Profession
                        {
                            string[] profs = new string[profession.Length + 1];
                            int j = 0;
                            foreach (var item in profession)
                            {
                                profs[j++] = item;
                            }
                            profs[j++] = "End";
                            while (true)
                            {
                                string prof;
                                prof = SelectingMenu.ChooseByString(profs);
                                if (prof == "End")
                                    break;
                                texts.Add(prof);
                            }
                            texts = texts.Distinct().ToList();
                            if (tempVacancy.Count == 0)
                            {
                                vacancies.ForEach(vac =>
                                {
                                    foreach (var text in texts)
                                    {
                                        if (vac.Profession == text)
                                        {
                                            tempVacancy.Add(vac);
                                        }

                                    }
                                });
                            }
                            else
                            {
                                tempVacancy = tempVacancy.Where(vac =>
                                {
                                    foreach (var text in texts)
                                    {
                                        if (vac.Profession == text)
                                            return true;
                                    }
                                    return false;
                                }).ToList();
                            }
                        }
                        break;
                    case 1://Salary
                        {
                            int salary;
                            while (true)
                            {
                                Console.Write("Min Salary: ");
                                if (int.TryParse(Console.ReadLine(), out int a))
                                {
                                    salary = a;
                                    break;
                                }
                            }
                            if (tempVacancy.Count == 0)
                            {
                                vacancies.ForEach(vac =>
                                {
                                    if (vac.Salary >= salary)
                                    {
                                        tempVacancy.Add(vac);
                                    }
                                });
                            }
                            else
                            {
                                tempVacancy = tempVacancy.Where(vac =>
                                {
                                    if (vac.Salary >= salary)
                                        return true;
                                    return false;
                                }).ToList();
                            }
                        }
                        break;
                    case 2: //Skill
                        {
                            while (true)
                            {
                                string skill;
                                skill = SelectingMenu.ChooseByString(skills);
                                if (skill == "End")
                                    break;
                                texts.Add(skill);
                            }
                            texts = texts.Distinct().ToList();
                            if (tempVacancy.Count == 0)
                            {
                                vacancies.ForEach(vac =>
                                {
                                    foreach (var text in texts)
                                    {
                                        if (vac.Skill.Contains(text))
                                        {
                                            tempVacancy.Add(vac);
                                        }

                                    }
                                });
                            }
                            else
                            {
                                tempVacancy = tempVacancy.Where(vac =>
                                {
                                    foreach (var text in texts)
                                    {
                                        if (vac.Skill.Contains(text))
                                            return true;
                                    }
                                    return false;
                                }).ToList();
                            }
                        }
                        break;
                    case 3: //Language
                        {
                            while (true)
                            {
                                string lang;
                                lang = SelectingMenu.ChooseByString(language);
                                if (lang == "End")
                                    break;
                                texts.Add(lang);
                            }
                            texts = texts.Distinct().ToList();
                            if (tempVacancy.Count == 0)
                            {
                                vacancies.ForEach(vac =>
                                {
                                    foreach (var text in texts)
                                    {
                                        if (vac.Language.Contains(text))
                                        {
                                            tempVacancy.Add(vac);
                                        }

                                    }
                                });
                            }
                            else
                            {
                                tempVacancy = tempVacancy.Where(vac =>
                                {
                                    foreach (var text in texts)
                                    {
                                        if (vac.Language.Contains(text))
                                            return true;
                                    }
                                    return false;
                                }).ToList();
                            }
                        }
                            break;
                    case 4: //Back
                        {
                            return tempVacancy;
                        }
                }
            }
            
        }

        public static int OptionsForEmployer(int id, Users users)
        {
            string[] list = new string[] { "Change Username", "Change Password", "Change Company Name", "Delete Account","Back" };
            int index = SelectingMenu.Choose(list);
            Console.Clear();
            switch (index)
            {
                case 0:
                    {
                        while (true)
                        {
                            Console.Write("New Username: ");
                            users.GetEmployerByID(id).Username = Console.ReadLine();
                            if (users.GetEmployerByID(id).Username.Length > 0)
                            {
                                users.UpdateEmployerJson();
                                return -1;
                            }
                        }
                    }
                case 1:
                    {
                        while (true)
                        {
                            Console.Write("New Password: ");
                            users.GetEmployerByID(id).Password = Console.ReadLine();
                            if (users.GetEmployerByID(id).Password.Length > 0)
                            {
                                users.UpdateEmployerJson();
                                return -1;
                            }
                        }
                    }
                case 2:
                    {
                        Console.WriteLine("New Company Name: ");
                        while (true)
                        {
                            users.GetEmployerByID(id).CompanyName = Console.ReadLine();
                            if (users.GetEmployerByID(id).CompanyName.Length > 0)
                            {
                                users.UpdateEmployerJson();
                                return -1;
                            }
                        }
                    }
                case 3:
                        users.DeleteEmployerByID(id);
                    break;
                case 4: return 11;
            }

            return -1;
        }

        public static int OptionsForEmployee(int id, Users users)
        {
            string[] list = new string[] { "Change Username", "Change Password", "Add Language ","Add Skill", "Delete Account", "Back" };
            int index = SelectingMenu.Choose(list);
            Console.Clear();
            switch (index)
            {
                case 0:
                    {
                        while (true)
                        {
                            Console.Write("New Username: ");
                            users.GetEmployeeByID(id).Username = Console.ReadLine();
                            if (users.GetEmployeeByID(id).Username.Length > 0)
                            {
                                users.UpdateEmployeeJson();
                                return -1;
                            }
                        }
                    }
                case 1:
                    {
                        while (true)
                        {
                            Console.Write("New Password: ");
                            users.GetEmployeeByID(id).Password = Console.ReadLine();
                            if (users.GetEmployeeByID(id).Password.Length > 0)
                            {
                                users.UpdateEmployeeJson();
                                return -1;
                            }
                        }
                    }
                case 2:
                    {
                        while (true)
                        {
                            string temp = SelectingMenu.ChooseByString(language);
                            if (temp == "End")
                                break;
                            else
                            {
                                users.GetEmployeeByID(id).Cvs.Language.Add(temp);
                            }
                        }
                        users.GetEmployeeByID(id).Cvs.Language = users.GetEmployeeByID(id).Cvs.Language.Distinct().ToList();
                        users.UpdateEmployeeJson();
                    }
                    break;
                case 3:
                    {
                        while (true)
                        {
                            string temp = SelectingMenu.ChooseByString(skills);
                            if (temp == "End")
                                break;
                            else
                            {
                                users.GetEmployeeByID(id).Cvs.Skill.Add(temp);
                            }
                        }
                        users.GetEmployeeByID(id).Cvs.Skill = users.GetEmployeeByID(id).Cvs.Skill.Distinct().ToList();
                        users.UpdateEmployeeJson();
                    }
                    break;
                case 4:
                    users.DeleteEmployeeByID(id);
                    break;
                case 5: return 21;
            }

            return -1;

            //Salam
        }
    }
}
