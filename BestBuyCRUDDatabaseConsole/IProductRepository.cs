using System.Collections.Generic;

namespace BestBuyCRUDDatabaseConsole
{
    internal interface IProductRepository
    {
        IEnumerable<Products> GetProducts();
        void CreateProducts(string createProducts, decimal productPrice, int productCategory);
        void UpdateProducts(string updateProducts, decimal updatePrice, int updateCatID);
        void DeleteProducts(int productID);

    }
}