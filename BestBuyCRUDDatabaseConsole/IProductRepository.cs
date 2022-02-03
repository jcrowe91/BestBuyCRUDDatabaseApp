using System.Collections.Generic;

namespace BestBuyCRUDDatabaseConsole
{
    internal interface IProductRepository
    {
        IEnumerable<Products> GetProducts();
        void CreateProducts();
        void UpdateProducts();
        void DeleteProducts();

    }
}