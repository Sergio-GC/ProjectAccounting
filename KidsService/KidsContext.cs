using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace KidsService
{
    public class KidsContext : DbContext
    {
        public DbSet<Kid> Kids { get; set; }
        public DbSet<Calendar> Calendars { get; set; }

        MySqlConnectionStringBuilder myBuilder = new MySqlConnectionStringBuilder()
        {
            Database = "pkids",
            UserID = "admin",
            Password = "rootadmin",
            Server = "192.168.0.200",
            Port = 3306,
        };

        public KidsContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseLazyLoadingProxies();
            builder.UseMySQL(myBuilder.ConnectionString);
        }
    }
}
