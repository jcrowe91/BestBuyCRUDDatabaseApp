﻿using System;
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

        public void CreateProducts()
        {
            throw new NotImplementedException();
        }

        public void DeleteProducts()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Products> GetProducts()
        {
            
        }

        public void UpdateProducts()
        {
            throw new NotImplementedException();
        }
    }
}
