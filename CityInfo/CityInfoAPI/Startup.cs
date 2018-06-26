using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace CityInfoAPI
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                //.AddJsonOptions(op =>
                //{ //Regresa las prop de json en Mayusculas, la primer letra
                //    var castedResolver = op.SerializerSettings.ContractResolver
                //                          as DefaultContractResolver;
                //    castedResolver.NamingStrategy = null;
                //})
                .AddMvcOptions(op=>op.OutputFormatters.Add(
                    new XmlDataContractSerializerOutputFormatter()
                    ))
                ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {           
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStatusCodePages();
            app.UseMvc();

            //app.Run(async (context) =>
            //{
                
            //   // await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
