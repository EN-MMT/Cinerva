using Cinerva.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Cinerva.TestConsole
{
    class Program
    {
        static void Header(int x)
        {
            System.Console.WriteLine($"======================= EX {x} =======================");
        }
        static void Main(string[] args)
        {

            var conf = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var connStr = conf.GetConnectionString("Cinerva");

            var host = Host.CreateDefaultBuilder().ConfigureServices((x, y) =>
            {
                y.AddDbContext<CinervaDbContext>(a => a.UseSqlServer(connStr));
                y.AddScoped<IQueryLibrary, QueryLibrary>();
            }).Build();

            var cinerva = host.Services.GetRequiredService<IQueryLibrary>(); //new CinervaDbContext();

            Header(2);
            cinerva.GetAllPropertiesInCity("Vaslui");

            Header(3);
            cinerva.Ex3();
            
            Header(4);
            cinerva.Ex4();

            Header(5);
            cinerva.Ex5();

            Header(6);
            cinerva.Ex6();
            
            Header(8);
            cinerva.Ex8();

            Header(9);
            cinerva.Ex9();

            Header(10);
            cinerva.Ex10();

            Header(11);
            cinerva.Ex11();

            Header(12);
            cinerva.Ex12();

            Header(13);
            cinerva.Ex13();

            Header(14);
            cinerva.Ex14();
        }
    }
}
