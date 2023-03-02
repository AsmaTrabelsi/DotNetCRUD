using Microsoft.EntityFrameworkCore;
using NZWallks.Data;
using NZWallks.Models.Domain;

namespace NZWallks.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly NZWallksDbContext nZWallksDbContext;
        public WalkRepository(NZWallksDbContext zWallksDbContext)
        {
            this.nZWallksDbContext = zWallksDbContext;
        }

        async Task<Walk> IWalkRepository.AddAsync(Walk walk)
        {
            walk.Id = Guid.NewGuid();
            await nZWallksDbContext.Walks.AddAsync(walk);
            await nZWallksDbContext.SaveChangesAsync();
            return walk;
        }

        async Task<Walk> IWalkRepository.DeleteAsync(Guid id)
        {
            var walk = await nZWallksDbContext.Walks.FindAsync(id);
            if(walk != null)
            {
                nZWallksDbContext.Walks.Remove(walk);
                await nZWallksDbContext.SaveChangesAsync();
                return walk;

            }
            return null;

        }

        async Task<IEnumerable<Walk>> IWalkRepository.GetAllAsync()
        {
            return await nZWallksDbContext.Walks.
                Include(x => x.Region)
                .Include(x => x.WalkDifficulty).ToListAsync();
        }

        async Task<Walk> IWalkRepository.GetAsync(Guid id)
        {
            return await nZWallksDbContext.Walks.Include(x => x.Region)
                .Include(x => x.WalkDifficulty).FirstOrDefaultAsync(x=> x.Id == id);
           

        }

        async Task<Walk> IWalkRepository.UpdateAsync(Guid id, Walk walk)
        {
            var existingWalk = await  nZWallksDbContext.Walks.FindAsync(id);
            if(existingWalk != null)
            {
                existingWalk.Length = walk.Length;
                existingWalk.WalkDifficultyId = walk.WalkDifficultyId;
                existingWalk.Name = walk.Name;
                existingWalk.RegionId = walk.RegionId;
                await nZWallksDbContext.SaveChangesAsync();
            }
            return null;
        }

        
    }
}
