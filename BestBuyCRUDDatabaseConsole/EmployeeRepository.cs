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
            _conn.Execute("INSERT INTO employees (EmployeeID, FirstName, MiddleInitial, LastName, EmailAddress) VALUES (@empID, @firstName, @middleInitial, @lastName, @email);",
                new { empID = empID, firstname = firstName, middleInitial = middleInitial, lastName = lastName, email = email });
        }

        public void DeleteEmployee(int empID)
        {
            _conn.Execute("DELETE FROM employees WHERE EmployeeID = @empID;",
               new { empID = empID });

            _conn.Execute("DELETE FROM sales WHERE EmployeeID = @empID;",
               new { empID = empID });            
        }

        public IEnumerable<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();
            employees = _conn.Query<Employee>("SELECT * FROM employees;").ToList();
            return employees;
        }

        public void UpdateEmployee(int empID, string firstName, string middleInitial, string lastName, string email)
        {
            _conn.Execute("UPDATE employees SET (EmployeeID, FirstName, MiddleInitial, LastName, EmailAddress = (@empID, @firstName, @middleInitial, @lastName, @email);",
                new {empID = empID , firstName = firstName , middleInitial = middleInitial , lastName = lastName , email = email });
        }
    }
}
