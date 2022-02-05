using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.IO;

namespace BestBuyCRUDDatabaseConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            #region ConnectionSetup
            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = config.GetConnectionString("DefaultConnection");
            IDbConnection conn = new MySqlConnection(connectionString);
            #endregion

            Console.WriteLine("Welcome to the BestBuy database!");
            Introduction(conn);
            CRUD(conn);
            Console.ReadLine();


        }
        public static void UserCreate(int userCreate, IDbConnection conn)
        {
            if (userCreate == 1)
            {
                var departmentRepo = new DepartmentRepository(conn);

                Console.WriteLine("What department would you like to add?");
                var newDepartmentName = Console.ReadLine();               

                departmentRepo.CreateDepartment(newDepartmentName);
            }
            else if (userCreate == 2)
            {
                var productsRepo = new ProductRepository(conn);

                Console.WriteLine($"What product would you like to add?");
                var createProduct = Console.ReadLine();

                Console.WriteLine("What would you like the price to be?");
                var productPrice = Convert.ToDecimal(Console.ReadLine());

                Console.WriteLine($"What would you like to set as the Category ID?");
                var categoryID = Convert.ToInt32(Console.ReadLine());

                productsRepo.CreateProducts(createProduct, productPrice, categoryID);
            }
            else if (userCreate == 3)
            {
                var empRepo = new EmployeeRepository(conn);

                Console.WriteLine("What EmployeeID would you like to use?");
                var empID = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("What is the Employee's first name?");
                var firstName = Console.ReadLine();

                Console.WriteLine("What's the Employee's middle initial? Press ENTER for none.");
                var middleInitial = Console.ReadLine();

                Console.WriteLine("What's the Employee's last name?");
                var lastName = Console.ReadLine();

                Console.WriteLine("What's a good email for the Employee?");
                var email = Console.ReadLine();

                empRepo.CreateEmployee(empID, firstName, middleInitial, lastName, email);
            }
        }
        public static void UserUpdate(int userUpdate, IDbConnection conn)
        {
            if (userUpdate == 1)
            {
                var departmentRepo = new DepartmentRepository(conn);
            }
            else if (userUpdate == 2)
            {
                var productsRepo = new ProductRepository(conn);
            }
            else if (userUpdate == 3)
            {
                var empRepo = new EmployeeRepository(conn);
            }
        }
        public static void SeeDepartments(System.Collections.Generic.IEnumerable<Department> departments)
        {
            foreach (var depo in departments)
            {
                Console.WriteLine($"Department ID: {depo.DepartmentID}\tDepartment Name: {depo.Name}");
            }
        }
        public static void SeeProducts(System.Collections.Generic.IEnumerable<Products> products)
        {
            foreach (var prod in products)
            {
                Console.WriteLine($"{prod.ProductID}  {prod.Name} (Price:{prod.Price} || Stock:{prod.StockLevel} || CategoryID {prod.CategoryID})");
                Console.WriteLine();
            }
        }
        public static void SeeEmployees(System.Collections.Generic.IEnumerable<Employee> employees)
        {
            foreach (var person in employees)
            {
                Console.WriteLine($"{person.EmployeeID} {person.FirstName} {person.MiddleInitial} {person.LastName} \tEmail: {person.EmailAddress}");
            }
        }
        public static void IntroUserSelection(string answer, IDbConnection conn)
        {
            if (answer.ToLower() == "y")
            {
                Console.WriteLine("Which table would you like to look at?");
                Console.WriteLine("1.Departments    2.Products    3.Employees");
                var table = Convert.ToInt32(Console.ReadLine());

                if (table == 1)
                {
                    var repoDepartments = new DepartmentRepository(conn);
                    var departments = repoDepartments.GetDepartments();
                    SeeDepartments(departments);
                }
                else if (table == 2)
                {
                    var repoProducts = new ProductRepository(conn);
                    var products = repoProducts.GetProducts();
                    SeeProducts(products);
                }
                else if (table == 3)
                {
                    var repoEmployees = new EmployeeRepository(conn);
                    var employees = repoEmployees.GetEmployees();
                    SeeEmployees(employees);
                }
                else
                {
                    Console.WriteLine("Please choose an availible answer. (PROGRAM WILL END)");
                    Environment.Exit(0);
                }
            }
            else if (answer.ToLower() == "n")
            {
                Console.WriteLine("Thank You! (PROGRAM WILL END)");
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Please choose Y/N (PROGRAM WILL END)");
                Environment.Exit(0);
            }
        }               
        public static void Introduction(IDbConnection conn)
        {
            Console.WriteLine("Would you like to choose a table to view?");
            Console.WriteLine("Y/N?");
            var answer = Console.ReadLine();
            IntroUserSelection(answer, conn);
        }
        public static void CRUD(IDbConnection conn)
        {
            Console.WriteLine("Would you like to create, update or delete an entry in a table?");
            Console.WriteLine("1.Create    2.Update    3.Delete    4.View another Department    5.Exit Program");
            var userChoice = Convert.ToInt32(Console.ReadLine());
            if (userChoice == 1)
            {
                Console.WriteLine("Which table would you like to create in?");
                Console.WriteLine("1.Departments    2.Products    3.Employees");
                var userCreate = Convert.ToInt32(Console.ReadLine());
                UserCreate(userCreate, conn);
            }
            else if (userChoice == 2)
            {
                Console.WriteLine("Which table would you like to update?");
                Console.WriteLine("1.Departments    2.Products    3.Employees");
                var userUpdate = Convert.ToInt32(Console.ReadLine());
                UserUpdate(userUpdate, conn);
            }
            else if (userChoice == 3)
            {
                Console.WriteLine("Which table would you like to delete from?");
                Console.WriteLine("1.Departments    2.Products    3.Employees");
                var userDelete = Convert.ToInt32(Console.ReadLine());
            }
            else if (userChoice == 4)
            {
                Introduction(conn);
            }
            else if (userChoice == 5)
            {
                Console.WriteLine("Have a great day!");
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Please choose an availible response. (PROGRAM WILL END)");
                Environment.Exit(0);
            }
        }
    }
}
