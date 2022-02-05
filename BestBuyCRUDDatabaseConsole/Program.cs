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

            Console.WriteLine("Welcome to the Best Buy Database! Would you like to continue and see all departments and products?");
            Console.WriteLine("Y/N?");
            var answer = Console.ReadLine();
            IntroUserSelection(answer, conn);
            Console.ReadLine();
            CreateProducts(conn);


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
                Console.WriteLine($"{prod.ProductID}  {prod.Name} (Price:{prod.Price} || Stock:{prod.StockLevel})");
                Console.WriteLine();
            }
        }
        public static void IntroUserSelection(string answer, IDbConnection conn)
        {
            if (answer.ToLower() == "y")
            {
                var repoDepartments = new DepartmentRepository(conn);
                var departments = repoDepartments.GetDepartments();
                SeeDepartments(departments);

                var repoProducts = new ProductRepository(conn);
                var products = repoProducts.GetProducts();
                SeeProducts(products);
            }
            else if (answer.ToLower() == "n")
            {
                Console.WriteLine("Thank You! (PROGRAM WILL END)");
            }
            else
            {
                Console.WriteLine("Please choose Y/N (PROGRAM WILL END)");
            }

        }
        public static void CreateProducts(IDbConnection conn)
        {
            var productsRepo = new ProductRepository(conn);

            Console.WriteLine($"What product would you like to add?");
            var createProduct = Console.ReadLine();

            Console.WriteLine("What would you like the price to be?");
            var productPrice = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine($"What would you like to set as the Category ID?");
            var categoryID = Convert.ToInt32(Console.ReadLine());

            productsRepo.CreateProducts(createProduct, productPrice, categoryID);
        }
    }
}
