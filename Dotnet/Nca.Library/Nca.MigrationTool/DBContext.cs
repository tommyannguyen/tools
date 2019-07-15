using Microsoft.EntityFrameworkCore;
using Nca.Library.Repositories.Database;

namespace Nca.MigrationTool
{
    public class DBContext : ApplicationDbContext
    {
        public DBContext() : base()
        {

        }
        public DBContext(DbContextOptions options) : base(options)
        {
        }
    }
}
