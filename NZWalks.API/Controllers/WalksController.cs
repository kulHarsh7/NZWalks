using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomAttributes;
using NZWalks.API.Models.DomainModels;
using NZWalks.API.Models.DTO.Regions;
using NZWalks.API.Models.DTO.Walks;
using NZWalks.API.Repositories.Implementation;
using NZWalks.API.Repositories.Interfaces;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;

        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
        }

        //GET: https://localhost:44373/api/Walk
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterValue, [FromQuery] string? orderBy,
        [FromQuery] bool? isAscending, [FromQuery] int pageNumber=1, [FromQuery] int pageSize=5)
        {
            //fetching data from repo
            var walkDomainData = await walkRepository.GetAllWalkAsync(filterOn, filterValue, orderBy, isAscending?? true, pageNumber, pageSize);
            if (walkDomainData == null)
            {
                return NotFound();
            }

            //mapping to Region DTOs
            var walkDTOData = mapper.Map<List<WalksDTO>>(walkDomainData);

            return Ok(walkDTOData);
        }

        //GET: https://localhost:44373/api/Walk/{Id}
        [HttpGet]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> GetByID([FromRoute] Guid Id)
        {
            //fetching data from repo
            var walkDomainData = await walkRepository.GetWalkAsync(Id);
            if (walkDomainData == null)
            {
                return NotFound();
            }

            //mapping to Region DTOs
            var walkDTOData = mapper.Map<WalksDTO>(walkDomainData);

            return Ok(walkDTOData);
        }

        //POST: https://localhost:44373/api/Walk
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] CreateWalksRequestDTO createWalksRequestDTO)
        {
            //map data to domail model.
            var walkDomainData = mapper.Map<Walk>(createWalksRequestDTO);
            // pass data to save in db
            var rwalkDataResult = await walkRepository.CreateWalkAsync(walkDomainData);

            //map to RegionDTO
            var walkDTO = mapper.Map<WalksDTO>(rwalkDataResult);
            return CreatedAtAction(nameof(GetByID), new { Id = walkDTO.Id }, walkDTO);
        }

        //PUT: https://localhost:44373/Walk/{Id}
        [HttpPut]
        [ValidateModel]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> Put([FromRoute] Guid Id, [FromBody] UpdateWalksRequestDTO updateWalksRequestDTO)
        {
            //map UpdateregionDTO to RegionDomain
            var walkDomainData = mapper.Map<Walk>(updateWalksRequestDTO);

            //Pass it to database for update
            var updatedDomainData = await walkRepository.UpdateWalkAsync(Id, walkDomainData);

            if (updatedDomainData == null)
            {
                return NotFound();
            }
            //map to RegionDTO
            var updatedDTOData = mapper.Map<WalksDTO>(updatedDomainData);
            return Ok(updatedDTOData);
        }

        //DELETE: https://localhost:44373/api/Walk/{Id}
        [HttpDelete]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid Id)
        {
            //pass id to delete resource
            var deletedData = await walkRepository.DeleteWalkAsync(Id);
            if (deletedData == null)
            {
                return NotFound();
            }

            //map to regionDTO
            var deletedDataDTO = mapper.Map<WalksDTO>(deletedData);
            return Ok(deletedDataDTO);
        }

    }
}
