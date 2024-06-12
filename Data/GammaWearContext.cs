using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GammaWear.Models;

namespace GammaWear.Data
{
    public class GammaWearContext : DbContext
    {

        public GammaWearContext(DbContextOptions<GammaWearContext> options)
            : base(options)
        {
        }

        public DbSet<Material> Materials { get; set; }
        public DbSet<OutdoorSport> OutdoorSports { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<SockStyle> SockStyles { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<GammaWear.Models.Sock> Socks { get; set; } = default!;

        
    }
}
