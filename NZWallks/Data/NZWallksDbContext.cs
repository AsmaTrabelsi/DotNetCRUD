using Microsoft.EntityFrameworkCore;
using NZWallks.Models.Domain;

namespace NZWallks.Data
{
    public class NZWallksDbContext :DbContext
    {
        
        public NZWallksDbContext(DbContextOptions<NZWallksDbContext> options) : base(options)
        {

        }

        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<WalkDifficulty> WalkDiffcilties { get; set; }
    }
}
