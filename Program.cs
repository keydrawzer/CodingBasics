using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace CodingBasics
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Host builder
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>(); //  Startup 
                });
    }
}