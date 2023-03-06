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
        private readonly IRegionrRpository regionrRpository;
        private readonly IWalkDifficiltyRepository walkDifficiltyRepository;
        public IMapper mapper;
        public WalksController(IWalkRepository wallkRepository, IMapper mapper, IRegionrRpository regionrRpository, IWalkDifficiltyRepository walkDifficiltyRepository)
        {
            this.mapper = mapper;
            this.wallkRepository = wallkRepository;
            this.regionrRpository = regionrRpository;
            this.walkDifficiltyRepository = walkDifficiltyRepository;
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
            //validate the data
            var isValidate = await ValidateAddWalkAsync(addWalkRequest);
            if (!isValidate)
            {
                return BadRequest(ModelState);
            }
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
            //validate data
            var isValidate = await ValidateUpdateWalkAsync(updateWalkRequest);
            if (!isValidate)
            {
                return BadRequest(ModelState);
            }
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

        #region Private methods
        private async Task<bool> ValidateAddWalkAsync(AddWalkRequest addWalkRequest)
        {
            if(addWalkRequest == null)
            {
                ModelState.AddModelError(nameof(addWalkRequest), $"{nameof(addWalkRequest)} is required");
                return false;
            }

            if (string.IsNullOrWhiteSpace(addWalkRequest.Name))
            {
                ModelState.AddModelError(nameof(addWalkRequest.Name), $"{nameof(addWalkRequest.Name)} cannot be null or empty");
            }
            if(addWalkRequest.Length <= 0)
            {
                ModelState.AddModelError(nameof(addWalkRequest.Length), $"{nameof(addWalkRequest.Length)} cannot be less then 0");
            }

            var region =  await regionrRpository.GetAsync(addWalkRequest.RegionId);
            if(region == null)
            {
                ModelState.AddModelError(nameof(addWalkRequest.RegionId), $"{nameof(addWalkRequest.RegionId)} is invalide");

            }
            var walkDificulty = await walkDifficiltyRepository.GetAsync(addWalkRequest.WalkDifficultyId);
            if (walkDificulty == null)
            {
                ModelState.AddModelError(nameof(addWalkRequest.WalkDifficultyId), $"{nameof(addWalkRequest.WalkDifficultyId)} is invalide");

            }
            if (ModelState.ErrorCount> 0)
            {
                return false; 
            }
            return true;
        }

        private async Task<bool> ValidateUpdateWalkAsync(UpdateWalkRequest updateWalkRequest)
        {
            if (updateWalkRequest == null)
            {
                ModelState.AddModelError(nameof(updateWalkRequest), $"{nameof(updateWalkRequest)} is required");
                return false;
            }

            if (string.IsNullOrWhiteSpace(updateWalkRequest.Name))
            {
                ModelState.AddModelError(nameof(updateWalkRequest.Name), $"{nameof(updateWalkRequest.Name)} cannot be null or empty");
            }
            if (updateWalkRequest.Length <= 0)
            {
                ModelState.AddModelError(nameof(updateWalkRequest.Length), $"{nameof(updateWalkRequest.Length)} cannot be less then 0");
            }

            var region = await regionrRpository.GetAsync(updateWalkRequest.RegionId);
            if (region == null)
            {
                ModelState.AddModelError(nameof(updateWalkRequest.RegionId), $"{nameof(updateWalkRequest.RegionId)} is invalide");

            }
            var walkDificulty = await walkDifficiltyRepository.GetAsync(updateWalkRequest.WalkDifficultyId);
            if (walkDificulty == null)
            {
                ModelState.AddModelError(nameof(updateWalkRequest.WalkDifficultyId), $"{nameof(updateWalkRequest.WalkDifficultyId)} is invalide");

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
