using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestBuyCRUDDatabaseConsole
{
    class EmployeeRepository : IEmployeeRepository
    {
        private readonly IDbConnection _conn;

        public EmployeeRepository(IDbConnection conn)
        {
            _conn = conn;
        }

        public void CreateEmployee(int empID, string firstName, string middleInitial, string lastName, string email)
        {
            _conn.Execute("INSERT INTO employees (EmployeeID, FirstName, MiddleInitial, LastName, EmailAddress) VALUES (@empID, @firstName, @middleItitial, @lastName, @email);",
                new { empID = empID, firstname = firstName, middleInitial = middleInitial, lastName = lastName, email = email });
        }

        public void DeleteEmployee()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();
            employees = _conn.Query<Employee>("SELECT * FROM employees;").ToList();
            return employees;
        }

        public void UpdateEmployee()
        {
            throw new NotImplementedException();
        }
    }
}
