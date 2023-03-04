using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWallks.Data;
using NZWallks.Models.Domain;
using NZWallks.Models.DTO;

namespace NZWallks.Repositories
{
    public class WalkDifficiltyRepository : IWalkDifficiltyRepository
    {
        private readonly NZWallksDbContext nZWallksDbContext;

        public WalkDifficiltyRepository(NZWallksDbContext nZWallksDbContext)
        {
            this.nZWallksDbContext = nZWallksDbContext;
        }

        async Task<Models.Domain.WalkDifficulty> IWalkDifficiltyRepository.AddAsync(Models.Domain.WalkDifficulty walkDifficulty)
        {
            walkDifficulty.Id =  Guid.NewGuid();
            await nZWallksDbContext.WalkDiffcilties.AddAsync(walkDifficulty);
            await nZWallksDbContext.SaveChangesAsync();
            return walkDifficulty;
        }

        async Task<Models.Domain.WalkDifficulty> IWalkDifficiltyRepository.DeleteAsync(Guid id)
        {
            var existingWalkDifficulty = await nZWallksDbContext.WalkDiffcilties.FindAsync(id);
            if (existingWalkDifficulty == null)
            {
                return null;
            }
            nZWallksDbContext.WalkDiffcilties.Remove(existingWalkDifficulty);
            await nZWallksDbContext.SaveChangesAsync();
            return existingWalkDifficulty;

        }

        async Task<IEnumerable<Models.Domain.WalkDifficulty>> IWalkDifficiltyRepository.GetAllAsync()
        {
            return await nZWallksDbContext.WalkDiffcilties.ToListAsync();
        }

        
        async Task<Models.Domain.WalkDifficulty> IWalkDifficiltyRepository.GetAsync(Guid id)
        {
            var walkDifficulty = await nZWallksDbContext.WalkDiffcilties.FirstOrDefaultAsync(x => x.Id == id);

            return walkDifficulty;
        }

        async Task<Models.Domain.WalkDifficulty> IWalkDifficiltyRepository.UpdateAsync(Guid id, Models.Domain.WalkDifficulty walkDifficulty)
        {
           var existingWalkDifficulty= await nZWallksDbContext.WalkDiffcilties.FindAsync(id);
            if(existingWalkDifficulty== null)
            {
                return null;
            }
            existingWalkDifficulty.Code= walkDifficulty.Code;
            await nZWallksDbContext.SaveChangesAsync();
            return existingWalkDifficulty;
        }
    }
}
