using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Configuration;

namespace KidsService
{
    public class KidsContext : DbContext
    {
        public DbSet<Kid> Kids { get; set; }
        public DbSet<Calendar> Calendars { get; set; }

        string connectionString = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;


        public KidsContext() { }
        public KidsContext(DbContextOptions<KidsContext> dbc) { }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseLazyLoadingProxies();
            builder.UseMySQL(connectionString);
        }
    }
}
