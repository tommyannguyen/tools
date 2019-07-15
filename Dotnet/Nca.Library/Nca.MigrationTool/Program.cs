using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Nca.MigrationTool
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var connectionstring = "Server=PC102;Database=TestDB;Trusted_Connection=True;";

            var optionsBuilder = new DbContextOptionsBuilder<DBContext>();
            optionsBuilder.UseSqlServer(connectionstring);

            Console.WriteLine("App is running...");
            DBContext dbContext = new DBContext(optionsBuilder.Options);

            dbContext.Database.Migrate();


            var initData = new DBContextInit(dbContext);
            await initData.RunAsync();
            Console.WriteLine("Done !...");
            Console.ReadKey();
        }
    }
}
