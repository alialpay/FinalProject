using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.CCS;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcers.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{

    public class ProductManager : IProductService
    {
        IProductDal _productDal;        // bir entitymanager yani buradaki ProductManager kendisi haricindeki başka bir Dal'ı enjekte edemez. ICategoryDal _categoryDal şeklinde kodlama yapılamaz
        ICategoryService _categoryService;      // bir managerin içerisinde kendi dalı hariç başka bir dal enjeksiyonu yapamayız. Onun yerine servisi enjekte edebiliriz
        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
            
        }

        //[LogAspect] --> AOP
        //Claim
        
        //[SecuredOperation("product.add,admin")]
        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Add(Product product)
        {
            //business codes

            IResult result = BusinessRules.Run(CheckIfProductNameExists(product.ProductName), // isterse bin tane böyle iş kuralı olsun. Bu şekilde gönderebiliriz
                CheckIfProductCountOfCategoryCorrect(product.CategoryId),
                CheckIfCategoryLimitExceded());                    // burada polymorphizm var

            if (result != null)
            {
                return result;
            }
            _productDal.Add(product);

            return new SuccessResult(Messages.ProductAdded);

        }

        //[CacheAspect]
        public IDataResult<List<Product>> GetAll()
        {
            if (DateTime.Now.Hour == 5)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }

            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductsListed);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id));
        }

        [CacheAspect]
        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        public new IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }

        [CacheRemoveAspect("IProductService.Get")]
        public IResult Update(Product product)
        {
            throw new NotImplementedException();
        }

        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId) // aynı kategori içinde en fazla 15 ürün olsun kuralı
        {
            // Select count(*) from products where categoryId=1
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
            if (result >= 15)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();
        }

        private IResult CheckIfProductNameExists(string productName) // aynı isimde ürün eklenemez kuralı
        {
            // Select count(*) from products where categoryId=1
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }
            return new SuccessResult();
        }
        private IResult CheckIfCategoryLimitExceded() 
        {

            var result = _categoryService.GetAll();
            if (result.Data.Count > 15)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);
            }
            return new SuccessResult();
        }

        public IResult AddTransactionalTest(Product product)
        {
            throw new NotImplementedException();
        }
    }
}

/*
Cross Cutting Conserns
    Validation
    Log
    Cache
    Transaction
    Authorization
    ...
*/