using System.Collections.Generic;

namespace BestBuyCRUDDatabaseConsole
{
    internal interface IEmployeeRepository
    {
        IEnumerable<Employee> GetEmployees();
        void CreateEmployee(int empID, string firstName, string middleInitial, string lastName, string email);
        void UpdateEmployee(int empID, string firstName, string middleInitial, string lastName, string email);
        void DeleteEmployee(int empID);
    }
}