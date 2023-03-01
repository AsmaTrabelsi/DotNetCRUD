using NZWallks.Models.Domain;

namespace NZWallks.Repositories
{
    public interface IRegionrRpository
    {
        Task<IEnumerable<Region>> GetAllAsync();
    }
}
