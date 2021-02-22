using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolvers.Autofac;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)                                            
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())         //  bu kýsým ben baþka bir conrainer kullanmak istiyorumu ifade eder
            .ConfigureContainer<ContainerBuilder>(builder => 
            {
                builder.RegisterModule(new AutofacBusinessModule());
            })                                                                      // buraya kadar yeni konfigurasyon iþlemi
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
