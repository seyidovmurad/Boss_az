using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BOSSAZ
{
    class Users
    {
        List<Employee> Employee;

        List<Employer> Employer;

        public void SignUp(Employee employee)
        {
            if (isUserExist(employee))
                throw new Exception("This username already exist");
            if (this.Employee.Count == 0)
                employee.ID = 0;
            else
                employee.ID = this.Employee.Last().ID + 1;
            this.Employee.Add(employee);
            UpdateEmployeeJson();
        }

        public void SignUp(Employer employer)
        {
            if (isUserExist(employer))
                throw new Exception("This username already exist");
            if (this.Employer.Count == 0)
                employer.ID = 0;
            else
                employer.ID = this.Employer.Last().ID + 1;
            this.Employer.Add(employer);
            UpdateEmployerJson();
        }

        public Person SignIn(string username, string password)
        {
            foreach (var item in Employee)
            {
                if (item.Username == username && item.Password == password)
                    return item;
            }
            foreach (var item in Employer)
            {
                if (item.Username == username && item.Password == password)
                    return item;
            }
            throw new Exception("Username or password is wrong.");
        }

        private bool isUserExist(Person person)
        {
            foreach (var employee in this.Employee)
            {
                if (person.Username == employee.Username)
                    return true;
            }
            foreach (var employer in this.Employer)
            {
                if (person.Username == employer.Username)
                    return true;
            }
            return false;
        }

        public void PostVacancyByID(int id, Vacancy vacancy)
        {
            vacancy.EmployerID = id;
            vacancy.ID = GetEmployerByID(id).SetVacancyID();
            GetEmployerByID(id).Vacancies.Add(vacancy);
            UpdateEmployerJson();
        }

        public List<Vacancy> GetAllVacancies()
        {
            List<Vacancy> vacancies = new List<Vacancy>();
            foreach (var employer in this.Employer)
            {
                foreach (var vacancy in employer.Vacancies)
                {
                    vacancies.Add(vacancy);
                }
            }
            return vacancies;
        }

        public Employer GetEmployerByID(int id)
        {
            return Employer.Find(emp => emp.ID == id);
        }

        public Employee GetEmployeeByID(int id)
        {
            return Employee.Find(emp => emp.ID == id);
        }

        public List<Employee> EmployeeWantJob(int employerID, int vacancyID)
        {
            List<int> ids = GetEmployerByID(employerID).GetVacancyByID(vacancyID).EmployeeID;
            return Employee.Where(emp =>
            {
                foreach (var id in ids)
                {
                    if (emp.ID == id)
                        return true;
                }
                return false;
            }).ToList();
        }

        public void ApplyForJob(int employeeID, int vacancyID, int employerID)
        {
            if (!GetEmployerByID(employerID).GetVacancyByID(vacancyID).EmployeeID.Exists(i => i == employeeID))
            {
                var emp = GetEmployeeByID(employeeID);
                GetEmployerByID(employerID).GetVacancyByID(vacancyID).EmployeeID.Add(employeeID);
                GetEmployerByID(employerID).Notification.Add(emp.Name + " " + emp.Surname + " Applied for job you posted");
                UpdateEmployerJson();
            }
        }

        public void HireForJob(int employeeID,int vacancyID,int employerID)
        {
            var emp = GetEmployerByID(employerID);
            GetEmployeeByID(employeeID).Notification.Add(emp.CompanyName + " hired you for " + "\"" + emp.GetVacancyByID(vacancyID).Title + "\"");
            DeleteVacancy(employerID,vacancyID);
            UpdateEmployerJson();
            UpdateEmployeeJson();
        }

        public void RejectForJob(int employeeID, int vacancyID, int employerID)
        {
            var emp = GetEmployerByID(employerID);
            GetEmployeeByID(employeeID).Notification.Add(emp.CompanyName + " reject your application for " + "\"" + emp.GetVacancyByID(vacancyID).Title + "\"");
            GetEmployerByID(employerID).GetVacancyByID(vacancyID).EmployeeID.Remove(employeeID);
            UpdateEmployerJson();
            UpdateEmployeeJson();
        }

        public void UpdateEmployerJson()
        {
            var jsonFile = JsonConvert.SerializeObject(this.Employer, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText("employer.json", jsonFile);
        }

        public void UpdateEmployeeJson()
        {
            var jsonFile = JsonConvert.SerializeObject(this.Employee, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText("employee.json", jsonFile);
        }

        public void DeleteVacancy(int empoyerID,int vacancyID)
        {
            GetEmployerByID(empoyerID).DeleteVacancyByID(vacancyID);
            GetEmployerByID(empoyerID).Vacancies.ForEach(vac =>
            {
                if (vac.ID > vacancyID)
                    vac.ID--;
            });
            UpdateEmployerJson();
        }

        public void DeleteEmployerByID(int id)
        {
            Employer.Remove(GetEmployerByID(id));
            Employer.ForEach(emp =>
            {
                if (emp.ID > id)
                {
                    emp.Vacancies.ForEach(vac => vac.EmployerID--);
                    emp.ID--;
                }
            });
            UpdateEmployerJson();
        }

        public void DeleteEmployeeByID(int id)
        {
            Employee.Remove(GetEmployeeByID(id));
            Employee.ForEach(emp =>
            {
                if (emp.ID > id)
                    emp.ID--;
            });
            Employer.ForEach(emp =>
            {
                emp.Vacancies.ForEach(vac =>
                {
                vac.EmployeeID.Remove(id);
                vac.EmployeeID.ForEach(eid => { if (eid > id) eid--; });
                });
            });
            UpdateEmployeeJson();
            UpdateEmployerJson();
        }

        public Users()
        {
            if (!File.Exists("employee.json"))
            {
                File.WriteAllText("employee.json", "");
            }
            if (!File.Exists("employee.json"))
            {
                File.WriteAllText("employer.json", "");
            }

            Employee = new List<Employee>();
            Employer = new List<Employer>();

            var jsonStr = File.ReadAllText("employee.json");
            var jsonStr2 = File.ReadAllText("employer.json");

            if(jsonStr.Length > 0)
            Employee = JsonConvert.DeserializeObject<List<Employee>>(jsonStr);

            if(jsonStr2.Length > 0)
            Employer = JsonConvert.DeserializeObject<List<Employer>>(jsonStr2);

        }
    }
}
