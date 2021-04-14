using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Danfohq
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

            //string accountSid = Environment.GetEnvironmentVariable("ACffd0d2ff5831b8adc4a9842f636656d7");
            //string authToken = Environment.GetEnvironmentVariable("6166f81fc449dab8c191ae803960d959");

            //TwilioClient.Init(accountSid, authToken);

            //var message = MessageResource.Create(
            //    body: "Welcome to Danfohq",
            //    from: new Twilio.Types.PhoneNumber("+2347066176828"),
            //    to: new Twilio.Types.PhoneNumber("+2347066176828"));
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        

    }
}
