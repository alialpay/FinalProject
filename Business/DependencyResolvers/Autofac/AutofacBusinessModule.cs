using Autofac;
using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module     // sen artık Autofac modülüsün
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance();   // birisi senden IProductService isterse ProductManager "register" et ProductManager instance'ı ver. newleyip ver
            builder.RegisterType<EfProductDal>().As<IProductDal>().SingleInstance();
        }
    }
}
