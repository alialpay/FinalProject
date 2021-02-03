using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{   // Dal data access layer, Dao data access object
    // interfaceler default public değildir ama içerisindeki metotlar default olarak publictir
    public interface IProductDal:IEntityRepository<Product>
    {
        
    }
}
