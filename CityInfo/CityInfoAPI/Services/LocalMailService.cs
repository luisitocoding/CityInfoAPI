using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfoAPI.Services
{
    public class LocalMailService:IMailService
    {
        private string _mailTo = Startup.configuration["mailSettings:mailToAddress"];

        private string _mailFrom = Startup.configuration["mailSettings:mailToFromAddress"];
        //ConfigureService en Startup Dependency Injecton 
        public void Send(string subject, string message)
        {
            Debug.WriteLine($"Mail from {_mailFrom} to {_mailTo}, With Local Service");
            Debug.WriteLine($"Subject {subject}");
            Debug.WriteLine($"Message {message}");
        }
    }
}
