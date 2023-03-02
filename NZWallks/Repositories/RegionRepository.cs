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

         async Task<Region> IRegionrRpository.AddAsync(Region region)
        {
            region.Id = Guid.NewGuid();
             await nZWallksDbContext.AddAsync(region);
           await  nZWallksDbContext.SaveChangesAsync();
            return region;

        }

        async Task<Region> IRegionrRpository.DeleteAsync(Guid id)
        {
            var region = await nZWallksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if(region == null)
            {
                return null;
            }
            // delete region from database
            nZWallksDbContext.Regions.Remove(region);
            nZWallksDbContext.SaveChangesAsync();
            return region;
        }

        async Task<IEnumerable<Region>> IRegionrRpository.GetAllAsync()
        {
            return  await nZWallksDbContext.Regions.ToListAsync();
        }

         async Task<Region> IRegionrRpository.GetAsync(Guid id)
        {
            return await nZWallksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        async Task<Region> IRegionrRpository.UpdateAsync(Guid id, Region region)
        {
            // find the region 
            var existingRegion = await nZWallksDbContext.Regions.FirstOrDefaultAsync(x=> x.Id == id);
            if(existingRegion == null)
            {
                return null;
            }
            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.Area= region.Area;
            existingRegion.Lat = region.Lat;
            existingRegion.Long = region.Long;
            existingRegion.Population = region.Population;
            await nZWallksDbContext.SaveChangesAsync();
            return existingRegion;

        }
    }
}
