using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
            //validate data
            if (!ValidateAddWalkDifficulty(addWalkDifficultyRequest))
            {
                return BadRequest(ModelState);
            }
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
        #region Private methods
        private bool ValidateAddWalkDifficulty(AddWalkDifficultyRequest addWalkDifficultyRequest)
        {
            if(addWalkDifficultyRequest == null)
            {
                ModelState.AddModelError(nameof(addWalkDifficultyRequest),$"{nameof(addWalkDifficultyRequest)} is required");
                return false;
            }

            if (string.IsNullOrWhiteSpace(addWalkDifficultyRequest.code))
            {
                ModelState.AddModelError(nameof(addWalkDifficultyRequest.code), $"{nameof(addWalkDifficultyRequest.code)} cannot be empty or null");

            }
            if (ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }

        private bool ValidateUpdateWalkDifficulty(UpdateWalkDifficulty updateWalkDifficulty)
        {
            if (updateWalkDifficulty == null)
            {
                ModelState.AddModelError(nameof(updateWalkDifficulty), $"{nameof(updateWalkDifficulty)} is required");
                return false;
            }

            if (string.IsNullOrWhiteSpace(updateWalkDifficulty.Code))
            {
                ModelState.AddModelError(nameof(updateWalkDifficulty.Code), $"{nameof(updateWalkDifficulty.Code)} cannot be empty or null");

            }
            if (ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}
