using KwiatkiBeatkiAPI.Models.Settings;
using Microsoft.EntityFrameworkCore;

namespace KwiatkiBeatkiAPI.DatabaseContext
{
    public class KwiatkiBeatkiDbContext : DbContext
    {
        private readonly DatabaseInfo _databaseInfo;

        public KwiatkiBeatkiDbContext(DatabaseInfo databaseInfo)
        {
            _databaseInfo = databaseInfo;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_databaseInfo.ConnectionString);
        }
    }
}
