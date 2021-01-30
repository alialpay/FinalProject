using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{   // Dal data access layer, Dao data access object
    // interfaceler default public değildir ama içerisindeki metotlar default olarak publictir
    public interface IProductDal
    {
        List<Product> GetAll();
        void Add(Product product);
        void Update(Product product);
        void Delete(Product product);
        List<Product> GetByCategory(int categoryId);
    }
}
