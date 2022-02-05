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
            
            Console.ReadLine();


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
                Console.WriteLine();
            }
        }
        public static void IntroUserSelection(string answer, IDbConnection conn)
        {
            if (answer.ToLower() == "y")
            {
                Console.WriteLine("Which table would you like to look at?");
                Console.WriteLine("1. Departments\t2.Products\t3.Employees");
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
        public static void CreateProducts(IDbConnection conn)
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
        public static void Introduction(IDbConnection conn)
        {
            Console.WriteLine("Would you like to choose a table to view?");
            Console.WriteLine("Y/N?");
            var answer = Console.ReadLine();
            IntroUserSelection(answer, conn);
        }
    }
}
