namespace NZWallks.Models
{
    public class Walk
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Length { get; set; }
        public Guid RegionId { get; set; }
        public Guid WalkDefficulty { get; set; }
        // Navigation properties

        public Region Region { get; set; }
        public IEnumerable<WalkDiffcilty> WalkDiffcilty { get; set; }
    }
}
