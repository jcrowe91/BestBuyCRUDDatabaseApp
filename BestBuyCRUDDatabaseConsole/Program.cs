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

            var repo = new DepartmentRepository(conn);
            var departments = repo.GetDepartments();


            //make into method
            foreach (var depo in departments)
            {
                Console.WriteLine($"Department ID: {depo.DepartmentID}\tDepartment Name: {depo.Name}");
            }

        }
    }
}
