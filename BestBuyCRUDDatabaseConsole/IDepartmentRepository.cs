using System.Collections.Generic;

namespace BestBuyCRUDDatabaseConsole
{
    interface IDepartmentRepository
    {
        IEnumerable<Department> GetDepartments();
        void CreateDepartment(string newDepartmentName);       
        void UpdateDepartment(string updateDepartment);
        void DeleteDepartment(int departmentID);
    }
}