using System;
using System.Collections.Generic;
using System.Linq;

namespace BOSSAZ
{
    class Employer: Person
    {
        public string CompanyName { get; set; }

        public List<Vacancy> Vacancies { get; set; }

        public int SetVacancyID()
        {
            if (Vacancies.Count == 0)
                return 0;
            else
                return Vacancies.Last().ID + 1;
        }

        public Vacancy GetVacancyByID(int id)
        {
            return Vacancies.Find(vac => vac.ID == id);
        }

        public void DeleteVacancyByID(int id)
        {
            Vacancies.Remove(GetVacancyByID(id));
        }

        public override void FillInfo()
        {
            while (true)
            {
                Console.Write("Name: ");
                Name = Console.ReadLine();
                if (Name.Length > 0)
                    break;
            }
            while (true)
            {
                Console.Write("Surname: ");
                Surname = Console.ReadLine();
                if (Surname.Length > 0)
                    break;
            }
            while (true)
            {
                Console.Write("Age: ");
                int a;
                if (int.TryParse(Console.ReadLine(), out a))
                {
                    try
                    {
                        Age = a;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        continue;
                    }
                    break;
                }
            }
            while (true)
            {
                Console.Write("Mail: ");
                try
                {
                    Mail = Console.ReadLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
                break;
            }
            while (true)
            {
                Console.Write("Phone: ");
                try
                {
                    Phone = Console.ReadLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
                break;
            }
            while (true)
            {
                Console.Write("Username: ");
                Username = Console.ReadLine();
                if (Username.Length > 0)
                    break;
            }
            while (true)
            {
                Console.Write("Password: ");
                try
                {
                    Password = Console.ReadLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
                break;
            }
            while (true)
            {
                Console.Write("Company Name: ");
                CompanyName = Console.ReadLine();
                if (CompanyName.Length > 0)
                    break;
            }
        }

        public Employer()
        {
            Vacancies = new List<Vacancy>();
            Notification = new List<string>();
        }
    }
}
