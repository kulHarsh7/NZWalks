using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomAttributes;
using NZWalks.API.Models.DomainModels;
using NZWalks.API.Models.DTO.Regions;
using NZWalks.API.Repositories.Interfaces;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper) {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        //GET: https://localhost:44373/api/Regions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //fetching data from repo
            var regionDomainData = await regionRepository.GetAllRegionsAsync();
            if(regionDomainData == null)
            {
                return NotFound();
            }

            //mapping to Region DTOs
            var regionDTOData = mapper.Map<List<RegionsDTO>>(regionDomainData);

            return Ok(regionDTOData);
        }

        //GET: https://localhost:44373/api/Regions/{Id}
        [HttpGet]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> GetByID([FromRoute] Guid Id )
        {
            //fetching data from repo
            var regionDomainData = await regionRepository.GetRegionAsync(Id);
            if(regionDomainData == null)
            {
                return NotFound();
            }

            //mapping to Region DTOs
            var regionDTOData = mapper.Map<RegionsDTO>(regionDomainData);

            return Ok(regionDTOData);
        }

        //POST: https://localhost:44373/api/Regions
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] CreateRegionsRequestDTO createRegionsRequestDTO)
        {
            //map data to domail model.
            var regionDomainData = mapper.Map<Region>(createRegionsRequestDTO);
            // pass data to save in db
            var regionDataResult = await regionRepository.CreateRegionAsync(regionDomainData);

            //map to RegionDTO
            var regionDTO = mapper.Map<RegionsDTO>(regionDataResult);
            return CreatedAtAction(nameof(GetByID), new { Id = regionDTO.Id }, regionDTO);
        }

        //PUT: https://localhost:44373/Regions/{Id}
        [HttpPut]
        [ValidateModel]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> Put([FromRoute] Guid Id, [FromBody] UpdateRegionsRequestDTO updateRegionsRequestDTO)
        {
            //map UpdateregionDTO to RegionDomain
            var regionDomainData = mapper.Map<Region>(updateRegionsRequestDTO);

            //Pass it to database for update
            var updatedDomainData = await regionRepository.UpdateRegionAsync(Id, regionDomainData);

            if(updatedDomainData == null)
            {
                return NotFound();
            }
            //map to RegionDTO
            var updatedDTOData = mapper.Map<RegionsDTO>(updatedDomainData);
            return Ok(updatedDTOData);
        }

        //PUT: https://localhost:44373/api/Regions/{Id}
        [HttpDelete]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid Id)
        {
            //pass id to delete resource
            var deletedData = await regionRepository.DeleteRegionAsync(Id);
            if(deletedData == null)
            {
                return NotFound();
            }

            //map to regionDTO
            var deletedDataDTO = mapper.Map<RegionsDTO>(deletedData);
            return Ok(deletedDataDTO);
        }

    }
}
