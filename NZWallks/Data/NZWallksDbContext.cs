using Microsoft.EntityFrameworkCore;
using NZWallks.Models;

namespace NZWallks.Data
{
    public class NZWallksDbContext :DbContext
    {
        public NZWallksDbContext(DbContextOptions<NZWallksDbContext> options) : base(options)
        {

        }

        DbSet<Region> Regions { get; set; }
        DbSet<Walk> Walks { get; set; }
        DbSet<WalkDifficulty> WalkDiffcilties { get; set; }
    }
}
