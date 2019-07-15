using Microsoft.EntityFrameworkCore;
using Nca.Library.Repositories.Database;
using System;

namespace Nca.MigrationTool
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionstring = "Server=PC102;Database=TestDB;Trusted_Connection=True;";

            var optionsBuilder = new DbContextOptionsBuilder<DBContext>();
            optionsBuilder.UseSqlServer(connectionstring);

            Console.WriteLine("App is running...");
            DBContext dbContext = new DBContext(optionsBuilder.Options);

            dbContext.Database.Migrate();

            Console.WriteLine("Done !...");
            Console.ReadKey();
        }
    }
}
