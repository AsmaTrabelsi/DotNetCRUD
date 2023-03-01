using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWallks.Models;
using NZWallks.Repositories;
using System.Security.Cryptography.X509Certificates;

namespace NZWallks.Controllers
{
    [ApiController]
    [Route("[controller]")] // controller means the controller name in this exemple is Regions
    public class RegionsController : Controller
    {
        private readonly IRegionrRpository regionrRpository;
        public IMapper mapper { get; }
        public RegionsController(IRegionrRpository regionrRpository, IMapper mapper)
        {
           
            this.regionrRpository = regionrRpository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            // Domain Region model
            var regions = await regionrRpository.GetAllAsync();
            // return DTO Regions
            var regionsDTO = mapper.Map < List< Models.DTO.Region >> (regions);
            return Ok(regionsDTO);
        }
        
    }
}
