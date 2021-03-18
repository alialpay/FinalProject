using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]     // Attribute (c#), Annotation (java)    // imzlamama yöntemi, bu bir controller'dır kendini ona göre yapılandır diyoruz
    public class ProductsController : ControllerBase
    {
        // Apilerin içerisinde Dal veya Business sınıflarını görmememiz gerekir. Dolayısıyla sistemde hiç bir katman diğerini new'lemiyor. Yani somut sınıf üzerinde ilerlemiyoruz.
        // Loosely coupled -- gevşek bağımlılık
        // naming convention -- bir field bu şekilde alt çizgi ile verilir. Field'lar default olarak private'dır. 
        // This yazmak yerine alt çizgili versiyonu yani fieldları kullanıyoruz
        // IoC Container -- Inversion of Control
        IProductService _productService;
                                   
        public ProductsController(IProductService productService)   // Bu bana bir manager var demek. Çünkü Iproduct service managerın referansını tutuyor.
        {
            _productService = productService;   // bunu yapmamızın nedeni aşağıdaki Get methodunun ona erişememesi. Bu yüzden productService için bir fielda referans veriyoruz
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            // Swagger
            // Dependency chain --

            Thread.Sleep(1000);

            var result = _productService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _productService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(Product product)
        {
            var result = _productService.Add(product);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
