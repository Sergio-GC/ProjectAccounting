using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace KidsService
{
    public class KidsContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<Kid> Kids { get; set; }
        public DbSet<Calendar> Calendars { get; set; }

        //string connectionString = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
        string connectionString = "Server=192.168.0.200;Port=3306;uid=admin;Password=rootadmin;Database=pkids";

        public KidsContext() {}

        public KidsContext(DbContextOptions<KidsContext> dbc) { }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseLazyLoadingProxies();
            builder.UseMySQL(connectionString);
        }
    }
}
