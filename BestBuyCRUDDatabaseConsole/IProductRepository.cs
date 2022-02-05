using System.Collections.Generic;

namespace BestBuyCRUDDatabaseConsole
{
    internal interface IProductRepository
    {
        IEnumerable<Products> GetProducts();
        void CreateProducts(string createProducts, double productPrice, int productCategory);
        void UpdateProducts(string updateProducts);
        void DeleteProducts();

    }
}