using System;
using System.Collections.Generic;

namespace BOSSAZ
{
    class Employee :Person
    {
        public CV Cvs { get; set; }

        
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
                if(int.TryParse(Console.ReadLine(), out a))
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
        }

        public override string ToString()
        {
            return $"{base.ToString()} \n\nCV:{Cvs}";
        }

        public Employee()
        {
            Notification = new List<string>();
        }
    }
}
