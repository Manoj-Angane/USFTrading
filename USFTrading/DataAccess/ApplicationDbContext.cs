using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using USFTrading.Models;

namespace USFTrading.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }
        public DbSet<Company> Company { get; set; }
        public DbSet<Sectors> Sector { get; set; }
        public DbSet<KeyStatGainers> KeyStatGainers { get; set; }
        public DbSet<KeyStatLosers> KeyStatLosers { get; set; }
        public DbSet<Stock> Stock { get; set; }
    }
}
