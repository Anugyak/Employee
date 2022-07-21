using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace Employee
{
    class EmployeeOperations
    {
        public string filePath = "D:\\emp.json";
        public void DisplayOptions()
        {
            Console.WriteLine("Enter what you want to do?");
            Console.WriteLine("1:Add new employee data");
            Console.WriteLine("2:Update emplyee data");
            Console.WriteLine("3:Delete employee data");
            Console.WriteLine("4:See employee data");

            int option = int.Parse(Console.ReadLine());
            switch (option)
            {
                case 1:
                    AddEmployee();
                    break;
                case 2:
                    UpdateEmployee();
                    break;
                case 3:
                    DeleteEmployee();
                    break;
                case 4:
                    GetEmployee();
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
        }
        public void AddEmployee()
        {
            Console.WriteLine("Enter id,name and salary of an employee");
            EmployeeModel model = new EmployeeModel();
            model.Id = int.Parse(Console.ReadLine());
            model.Name = Console.ReadLine();
            model.Salaray = float.Parse(Console.ReadLine());

            List<EmployeeModel> empList = new List<EmployeeModel>();
            if (File.Exists(filePath))
            {
                string empLists = File.ReadAllText(filePath);
                if (empLists != null)
                {
                    empList = JsonSerializer.Deserialize<List<EmployeeModel>>(empLists);
                }
            }


            empList.Add(model);


            StreamWriter empStremWriter = new StreamWriter(filePath);
            string json = JsonSerializer.Serialize(empList);
            empStremWriter.WriteLine(json);
            empStremWriter.Close();
            Console.WriteLine("Employee added successfully!");
        }


        public void UpdateEmployee()
        {
            Console.WriteLine("Enter the id of an employee you want to update!");
            int id = int.Parse(Console.ReadLine());
            string empLists = File.ReadAllText(filePath);
            List<EmployeeModel> empList = JsonSerializer.Deserialize<List<EmployeeModel>>(empLists);
            foreach (var emp in empList.ToList())
            {
                if (emp.Id == id)
                {
                    Console.WriteLine("Enter name and salary you want to update");
                    emp.Name = Console.ReadLine();
                    emp.Salaray = float.Parse(Console.ReadLine());


                    string json = JsonSerializer.Serialize(empList);
                    StreamWriter streamWriter = new StreamWriter(filePath);
                    streamWriter.WriteLine(json);
                    streamWriter.Close();
                    Console.WriteLine("Employee updated successfully!");

                    return;
                }
            }
            Console.WriteLine("No employeed with id " + id);
        }

        public void DeleteEmployee()
        {
            Console.WriteLine("Enter the id of an employee you want to delete!");
            int id = int.Parse(Console.ReadLine());
            string empLists = File.ReadAllText(filePath);
            List<EmployeeModel> empList = JsonSerializer.Deserialize<List<EmployeeModel>>(empLists);
            foreach (EmployeeModel emp in empList.ToList())
            {
                if (emp.Id == id)
                {
                    empList.Remove(emp);
                    string json = JsonSerializer.Serialize(empList);
                    StreamWriter streamWriter = new StreamWriter(filePath);
                    streamWriter.WriteLine(json);
                    streamWriter.Close();
                    Console.WriteLine("Employee deleted successfully!");
                    return;
                }
            }

            Console.WriteLine("No employeed with id " + id);
        }
        public void GetEmployee()
        {
            Console.WriteLine("Employee Lists:");
            string empLists = File.ReadAllText(filePath);
            List<EmployeeModel> empList = JsonSerializer.Deserialize<List<EmployeeModel>>(empLists);

            foreach (EmployeeModel emp in empList)
            {
                Console.WriteLine("Id=" + emp.Id);
                Console.WriteLine("Name=" + emp.Name);
                Console.WriteLine("Salary=" + emp.Salaray);
            }
        }
    }
}
