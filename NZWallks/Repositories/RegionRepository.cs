using Microsoft.EntityFrameworkCore;
using NZWallks.Data;
using NZWallks.Models.Domain;
using NZWallks.Repositories;

namespace NZWallks.Repositories
{
    public class RegionRepository : IRegionrRpository
    {
        private readonly NZWallksDbContext nZWallksDbContext;
        public RegionRepository(NZWallksDbContext nZWallksDbContext)
        {
            this.nZWallksDbContext = nZWallksDbContext; 
        }
    

         async Task<IEnumerable<Region>> IRegionrRpository.GetAllAsync()
        {
            return  await nZWallksDbContext.Regions.ToListAsync();
        }
    }
}
