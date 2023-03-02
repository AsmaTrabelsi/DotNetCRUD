using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWallks.Models.Domain;
using NZWallks.Models.DTO;
using NZWallks.Repositories;

namespace NZWallks.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalksController : Controller
    {
        // constructor injection
        private readonly IWalkRepository wallkRepository;
        public IMapper mapper;
        public WalksController(IWalkRepository wallkRepository, IMapper mapper)
        {
            this.mapper = mapper;
            this.wallkRepository = wallkRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllWallksAsync()
        {
            var wallks = await wallkRepository.GetAllAsync();
            var wallksDTO = mapper.Map<List<Models.DTO.Wallk>>(wallks);
            return Ok(wallksDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkAsync")]
        public async Task<IActionResult> GetWalkAsync(Guid id)
        {
            var walk = await wallkRepository.GetAsync(id);

            if (walk == null)
            {
                return NotFound();
            }

            var walkDTO = mapper.Map<Models.DTO.Wallk>(walk);
            return Ok(walkDTO);
        }

        [HttpPost]

        public async Task<IActionResult> AddWalkAsync([FromBody] AddWalkRequest addWalkRequest)
        {
            var walk = new Models.Domain.Walk
            {
                Length = addWalkRequest.Length,
                Name =addWalkRequest.Name,
                RegionId =addWalkRequest.RegionId,
                WalkDifficultyId=addWalkRequest.WalkDifficultyId
            };
            walk = await wallkRepository.AddAsync(walk);

            var walkDTO = new Models.DTO.Wallk()
            {
                Id = walk.Id,
                Length = walk.Length,
                Name = walk.Name,
                RegionId = walk.RegionId,
                WalkDifficultyId = walk.WalkDifficultyId
            };
            return CreatedAtAction(nameof(GetWalkAsync), new {id= walkDTO.Id},walkDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id, [FromBody] UpdateWalkRequest updateWalkRequest)
        {
            var walkDomain = new Models.Domain.Walk()
            {
                Length = updateWalkRequest.Length,
                Name = updateWalkRequest.Name,
                RegionId = updateWalkRequest.RegionId,
                WalkDifficultyId = updateWalkRequest.WalkDifficultyId
            };
            walkDomain= await wallkRepository.UpdateAsync(id, walkDomain);

            if(walkDomain == null)
            {
                return NotFound("Walk with this id not found");
            }

            var walkDTO = new Models.DTO.Wallk()
            {
                Id = walkDomain.Id,
                Length = walkDomain.Length,
                Name = walkDomain.Name,
                RegionId = walkDomain.RegionId,
                WalkDifficultyId = walkDomain.WalkDifficultyId
            };
            return Ok(walkDTO);


        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkAsync(Guid id)
        {
           var walkDomain= await wallkRepository.DeleteAsync(id);
            if(walkDomain== null)
            {
                return NotFound();
            }
           var walkDTO= mapper.Map<Models.DTO.Wallk>(walkDomain);
            return Ok(walkDTO);
        }
    }
}
