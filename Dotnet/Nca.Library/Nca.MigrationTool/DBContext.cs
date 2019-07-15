using Microsoft.EntityFrameworkCore;
using Nca.Library.Repositories.Database;
using System.Threading.Tasks;

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

    public class DBContextInit
    {
        private readonly DBContext _dbContext;

        public DBContextInit(DBContext dBContext) {
            _dbContext = dBContext;
        }

        public async Task RunAsync()
        {

        }
    }
}
