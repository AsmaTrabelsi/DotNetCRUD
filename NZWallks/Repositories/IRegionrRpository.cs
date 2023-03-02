using NZWallks.Models.Domain;

namespace NZWallks.Repositories
{
    public interface IRegionrRpository
    {
        public Task<IEnumerable<Region>> GetAllAsync();
        public Task<Region> GetAsync(Guid id);

        public Task<Region> AddAsync(Region region);

        public Task<Region> DeleteAsync(Guid id);

        public Task<Region> UpdateAsync(Guid id, Region region);
    }
}
