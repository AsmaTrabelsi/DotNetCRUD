using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWallks.Models;
using NZWallks.Models.Domain;
using NZWallks.Repositories;
using System.Security.Cryptography.X509Certificates;

namespace NZWallks.Controllers
{
    [ApiController]
    [Route("[controller]")] // controller means the controller name in this exemple is Regions
    public class RegionsController : Controller
    {
        private readonly IRegionrRpository regionRpository;
        public IMapper mapper { get; }
        public RegionsController(IRegionrRpository regionrRpository, IMapper mapper)
        {
           
            this.regionRpository = regionrRpository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRegionsAsync()
        {
            // Domain Region model
            var regions = await regionRpository.GetAllAsync();
            // return DTO Regions
            var regionsDTO = mapper.Map < List< Models.DTO.Region >> (regions);
            return Ok(regionsDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            var region = await regionRpository.GetAsync(id);
            if(region == null)
            {
                return NotFound();
            }
            var regionDTO = mapper.Map<Models.Domain.Region, Models.DTO.Region>(region);

            return Ok(regionDTO);

        }
        [HttpPost]
        public async Task<IActionResult> AddRegionAsync(Models.DTO.AddRegionRequest addRegionRequest)
        {
            // convert DTO Region to Region Domaine
            var region = new Models.Domain.Region()
            {
                Code = addRegionRequest.Code,
                Name = addRegionRequest.Name,   
                Area = addRegionRequest.Area,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Population = addRegionRequest.Population,

            };

            // passe data to database
             region = await regionRpository.AddAsync(region);

            // convert back to DTO 
            var regionDTO = new Models.DTO.Region
            {
                Code = region.Code,
                Name = region.Name,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population,
            };
            // we will use CreatedAtAction() instead of OK() because we are using http post 
            return CreatedAtAction(nameof(GetRegionAsync),new {id=regionDTO.Id},regionDTO);
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            var region = await regionRpository.DeleteAsync(id);

            if(region == null)
            {
                return NotFound();
            }
            // convert region to DTO
            var regionDTO = new Models.DTO.Region
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population,
            };
            return Ok(regionDTO);
            
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id,[FromBody] Models.DTO.UpdateRegionRequest updateRegionRequest)
        {
            // convert DTO to domaine
            var regionDomaine = new Models.Domain.Region()
            {
                Code = updateRegionRequest.Code,
                Name = updateRegionRequest.Name,
                Area = updateRegionRequest.Area,
                Lat = updateRegionRequest.Lat,
                Long = updateRegionRequest.Long,
                Population = updateRegionRequest.Population,
            };

             regionDomaine = await regionRpository.UpdateAsync(id, regionDomaine);
            if(regionDomaine == null) { 
                return NotFound();
             }
            var regionDTO = new Models.DTO.Region()
            {
                Code = regionDomaine.Code,
                Name = regionDomaine.Name,
                Area = regionDomaine.Area,
                Lat = regionDomaine.Lat,
                Long = regionDomaine.Long,
                Population = regionDomaine.Population,
            };
            return Ok(regionDomaine);

        }
        
    }
}
