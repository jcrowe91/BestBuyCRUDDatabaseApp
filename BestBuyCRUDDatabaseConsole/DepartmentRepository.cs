using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace BestBuyCRUDDatabaseConsole
{
    class DepartmentRepository : IDepartmentRepository
    {
        private readonly IDbConnection _conn;
        
        public DepartmentRepository(IDbConnection conn)
        {
            _conn = conn;
        }

        public void CreateDepartment(string newDepartmentName)
        {
            _conn.Execute($"INSERT INTO DEPARTMENTS (Name) VALUES (@newDepartmentName)",
                new { newDepartmentName = newDepartmentName });
        }

       
        public void DeleteDepartment(int departmentID)
        {
            _conn.Execute("DELETE FROM categories WHERE DepartmentID = @departmentID;",
               new { departmentID = departmentID });

            _conn.Execute("DELETE FROM departments WHERE DepartmentID = @departmentID;",
               new { departmentID = departmentID });
        }

        

        public IEnumerable<Department> GetDepartments()
        {
            List<Department> departments = new List<Department>();
            departments = _conn.Query<Department>("SELECT * FROM departments;").ToList();
            return departments;
        }

        public void UpdateDepartment(string updateDepartment)
        {
            _conn.Execute("UPDATE departments SET Name = (@updateDepartment)",
                new { updateDepartment = updateDepartment });
        }
    }
}
