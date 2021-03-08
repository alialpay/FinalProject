using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IProductService
    {
        IDataResult<List<Product>> GetAll();
        IDataResult<List<Product>> GetAllByCategoryId(int id);
        IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max);
        IDataResult<List<ProductDetailDto>> GetProductDetails();
        IDataResult<Product> GetById(int productId);
        IResult Add(Product product);
        IResult Update(Product product);
        IResult AddTransactionalTest(Product product);      // Transaction : Uygulamalarda tutarlılığı korumak için yapılan bir yöntem. örneğin enginin hesabından
                                                            // 10 lira kereme yollandığında enginden 10 düşülür, kereme 10 eklenmesi gerekir. enginden 10 gidip kereme 10 gelmezse işlemin geri alınması gerekir


        // RESTFUL --> HTTP --> (TCP)
    }
}
