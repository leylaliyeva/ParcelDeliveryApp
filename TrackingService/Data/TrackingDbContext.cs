using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TrackingService.Models;

namespace TrackingService.Data
{
    public class TrackingDbContext : DbContext
    {
        public TrackingDbContext(DbContextOptions<TrackingDbContext> options) : base(options) { }

        public DbSet<TrackingInfo> TrackingInfos { get; set; }
        public DbSet<Courier> Couriers { get; set; }
    }
}
