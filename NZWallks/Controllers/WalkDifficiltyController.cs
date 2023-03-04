using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWallks.Models.Domain;
using NZWallks.Models.DTO;
using NZWallks.Repositories;

namespace NZWallks.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalkDifficiltyController : Controller
    {
        private readonly IWalkDifficiltyRepository walkDifficiltyRepository;
        private IMapper mapper;

        public WalkDifficiltyController(IWalkDifficiltyRepository walkDifficiltyRepository, IMapper mapper)
        {
            this.walkDifficiltyRepository = walkDifficiltyRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalkDifficiltyAsync()
        {
            var walkDifficilties = await walkDifficiltyRepository.GetAllAsync();
            var walkDifficiltiesDTO = mapper.Map<List<Models.DTO.WalkDifficulty>>(walkDifficilties);
            return Ok(walkDifficiltiesDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkDifficultyAsync")]
        public async Task<IActionResult> GetWalkDifficultyAsync(Guid id)
        {
            var walkDifficulty = await walkDifficiltyRepository.GetAsync(id);
            if(walkDifficulty == null)
            {
                return NotFound();
            }
           var walkDifficultyDTO= mapper.Map<Models.DTO.WalkDifficulty>(walkDifficulty);
            return Ok(walkDifficulty);

        }
        [HttpPost]
        public async Task<IActionResult> AddWalkDifficulty([FromBody]AddWalkDifficultyRequest addWalkDifficultyRequest)
        {
            var walkDifficultyDomain = new Models.Domain.WalkDifficulty()
            {
                Code = addWalkDifficultyRequest.code
            };
            walkDifficultyDomain = await walkDifficiltyRepository.AddAsync(walkDifficultyDomain);

            var walkDifficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDifficultyDomain);
            return CreatedAtAction(nameof(GetWalkDifficultyAsync),
                new {id = walkDifficultyDTO.Id},walkDifficultyDTO);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkDifficulty(Guid id, UpdateWalkDifficulty updateWalkDifficulty)
        {
            var walkDifficultyDomain = new Models.Domain.WalkDifficulty()
            {
                Code = updateWalkDifficulty.Code
            }; 
            walkDifficultyDomain = await walkDifficiltyRepository.UpdateAsync(id, walkDifficultyDomain);
            if(walkDifficultyDomain == null)
            {
                return NotFound();
            }
            var walkDifficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDifficultyDomain);
            return Ok(walkDifficultyDTO);   

        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkDifficulty(Guid id)
        {
            var walkDifficulty = await walkDifficiltyRepository.DeleteAsync(id);
            if(walkDifficulty == null)
            {
                return NotFound();
            }
            var walkDifficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDifficulty);
            return Ok(walkDifficultyDTO);
        }
    }
}
