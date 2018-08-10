using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CityInfoAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureLogging(loggin => 
                 {
                     loggin.AddProvider(new NLog.Extensions.Logging.NLogLoggerProvider());
                 }).Build();
    }

    //Staging-production-development
    //Inicia en Program-ConfigureServices-Configure
    //.ConfigureLogging Srve para registrar el logger agregando un provedor(provider)
    //Propiedades sobre el nlog.config y darle Copiar si es posterior para que lo copie en la carpeta bin
}
