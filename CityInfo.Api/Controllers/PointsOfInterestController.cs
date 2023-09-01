using AutoMapper;
using CityInfo.Api.Entities;
using CityInfo.Api.Models;
using CityInfo.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.Api.Controllers
{
    [Route("api/cities/{cityId}/pointsofinterest")]
    [Authorize(Policy = "MustBeFromAntwerp")]
    [ApiController]
    public class PointsOfInterestController : ControllerBase
    {

        private readonly ILogger<PointsOfInterestController> _logger;
        private readonly IMailService _mailService;
        ICityInfoRepository _cityInfoRepository;
        IMapper _mapper;

        public PointsOfInterestController(ILogger<PointsOfInterestController> logger,
            IMailService mailService,CitiesDataStore citiesDataStore, ICityInfoRepository cityInfoRepository,IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentNullException();
            _mapper = mapper;
            _cityInfoRepository = cityInfoRepository;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<PointOfInterestDto>>> 
            GetPointsOfInterest(int cityId)
        {
            var cityName = User.Claims.FirstOrDefault(c => c.Type == "city")?.Value;

            if(!await _cityInfoRepository.CityNameMatchesCityId(cityName, cityId))
            {
                return Forbid();
            }
            
            
            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }
            var pointsOfInterestFromRepo = await _cityInfoRepository.GetPointsOfInterestForCityAsync(cityId);
            return Ok(_mapper.Map<IEnumerable<PointOfInterestDto>>(pointsOfInterestFromRepo));

        }




        [HttpGet("{pointofinterestid}", Name = "GetPointOfInterest")]
        public async Task<ActionResult<PointOfInterestDto>> GetPointOfInterest(int cityId, int pointOfInterestId)
        {
            bool isCityExists = await _cityInfoRepository.CityExistsAsync(cityId);

            if (isCityExists == false)
            {
                return NotFound();
            }

            var pointOfInterest = await _cityInfoRepository.GetPointOfInterestForCityAsync(cityId, pointOfInterestId);


            if(pointOfInterest == null)
            {
                return NotFound();
            }


            return Ok(_mapper.Map<PointOfInterestDto>(pointOfInterest));

        }




        [HttpPost]
        public async Task<ActionResult<PointOfInterestDto>> CreatePointOfInterest(
                int cityId,
                PointOfInterestForCreationDto pointOfInterest)
        {

            if(!await _cityInfoRepository.CityExistsAsync(cityId)) 
            {
                return NotFound();
            }


            var finalPointOfInterest = _mapper.Map<Entities.PointOfInterest>(pointOfInterest);

            await _cityInfoRepository.AddPointOfInterestForCityAsync(cityId, finalPointOfInterest);

            
            await _cityInfoRepository.SaveChangesAsync();

            var createdPointOfInterestToReturn = _mapper.Map<Models.PointOfInterestDto>(finalPointOfInterest);
            


            return CreatedAtRoute("GetPointOfInterest", new
            {
                cityId = cityId,
                pointOfInterestId = createdPointOfInterestToReturn.Id
            },
            createdPointOfInterestToReturn);
        }






        [HttpPut("{pointOfInterestId}")]
        public async Task<ActionResult> UpdatePointOfInterest(int cityId,
            int pointOfInterestId, PointOfInterestForCreationDto pointOfInterest)
        {

   
            if(!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }

            var pointOfInterestEntity = await _cityInfoRepository.
                GetPointOfInterestForCityAsync(cityId,pointOfInterestId);

            if(pointOfInterest == null)
            {
                return NotFound();
            }

            _mapper.Map(pointOfInterest, pointOfInterestEntity);

            await _cityInfoRepository.SaveChangesAsync();

            return NoContent();
        }




        [HttpPatch("{pointOfinterestid}")]
        public async Task<ActionResult> PartiallyUpdatePointOfInterest(int cityId, int pointOfInterestId,
        JsonPatchDocument<PointForInterestForUpdateDto> patchDocument)
        {
            if(!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }

            var pointOfInterestEntity = await _cityInfoRepository.
               GetPointOfInterestForCityAsync(cityId, pointOfInterestId);

            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

            var pointOfInterestoPatch = _mapper.
                Map<PointForInterestForUpdateDto>(pointOfInterestEntity);
           

            patchDocument.ApplyTo(pointOfInterestoPatch, ModelState);


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            if (!TryValidateModel(pointOfInterestoPatch))
            {
                return BadRequest(ModelState);
            }



            _mapper.Map(pointOfInterestoPatch, pointOfInterestEntity);
            await _cityInfoRepository.SaveChangesAsync();

            return NoContent();
        }


        [HttpDelete("{pointOfInterestId}")]

        public async Task<ActionResult> DeletePoinrodInterest(int cityId, int pointOfInterestId)
        {
            if(!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }

            var pointOfInterestEntity = await _cityInfoRepository.
                GetPointOfInterestForCityAsync(cityId, pointOfInterestId);

            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

            _cityInfoRepository.DeletePointOfInterest(pointOfInterestEntity);
            await _cityInfoRepository.SaveChangesAsync();
           


            _mailService.Send("Testing Subject", "Your interest is deleted");
            return NoContent();

        }

    }


    
}
