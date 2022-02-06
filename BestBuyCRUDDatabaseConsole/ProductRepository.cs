using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestBuyCRUDDatabaseConsole
{
    class ProductRepository : IProductRepository
    {
        private readonly IDbConnection _conn;
        public ProductRepository(IDbConnection conn)
        {
            _conn = conn;
        }

        public void CreateProducts(string createProducts, decimal productPrice, int productCategory)
        {
            _conn.Execute("INSERT INTO products (Name, Price, CategoryID) VALUES (@createProducts , @productPrice , @productCategory);",
                new { createProducts = createProducts , productPrice = productPrice , productCategory = productCategory });
        }

        public void DeleteProducts(int productID)
        {
            _conn.Execute("DELETE FROM reviews WHERE ProductID = @productID;",
               new { productID = productID });

            _conn.Execute("DELETE FROM sales WHERE ProductID = @productID;",
               new { productID = productID });

            _conn.Execute("DELETE FROM products WHERE ProductID = @productID;",
               new { productID = productID });
        }

        public IEnumerable<Products> GetProducts()
        {
            List<Products> products = new List<Products>();
            products = _conn.Query<Products>("SELECT * FROM products;").ToList();
            return products;
        }

        public void UpdateProducts(string updateProducts, decimal updatePrice, int updateCatID)
        {
            _conn.Execute("UPDATE products SET Name, Price, CategoryID = (@updateProducts , @updatePrice, @updateCatID);",
                new { updateProducts = updateProducts , updatePrice = updatePrice , updateCatID = updateCatID});
        }
    }
}
