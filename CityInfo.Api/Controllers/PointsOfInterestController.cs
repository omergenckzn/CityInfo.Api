using AutoMapper;
using CityInfo.Api.Models;
using CityInfo.Api.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.Api.Controllers
{
    [Route("api/cities/{cityId}/pointsofinterest")]
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
        public async Task<ActionResult<IEnumerable<PointOfInterestDto>>> GetPointsOfInterest(int cityId)
        {
           
            

            if(!await _cityInfoRepository.CityExistsAsync(cityId))
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




        //[HttpPost]
        //public ActionResult<PointOfInterestDto> CreatePointOfInterest(
        //        int cityId,
        //        PointOfInterestForCreationDto pointOfInterest)
        //{


        //    var city =_citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
        //    if (city == null)
        //    {
        //        return NotFound();
        //    }

        //    var maxPointOfInterestId =
        //        _citiesDataStore.Cities.SelectMany(c => c.PointsOfInterest).Max(p => p.Id);

        //    var finalPointOfInterest = new PointOfInterestDto()
        //    {
        //        Id = ++maxPointOfInterestId,
        //        Name = pointOfInterest.Name,
        //        Description = pointOfInterest.Description
        //    };

        //    city.PointsOfInterest.Add(finalPointOfInterest);
        //    return CreatedAtRoute("GetPointOfInterest", new
        //    {
        //        cityId = cityId,
        //        pointOfInterestId = finalPointOfInterest.Id
        //    },
        //    finalPointOfInterest);
        //}


        //[HttpPut("(pointofinteresid)")]
        //public ActionResult UpdatePointOfInterest(int cityId,
        //    int pointOfInterestId, PointOfInterestForCreationDto pointOfInterest)
        //{

        //    var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);

        //    if (city == null)
        //    {
        //        return NotFound();
        //    }
        //    var pointsOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(c => c.Id == pointOfInterestId);

        //    if (pointsOfInterestFromStore == null)
        //    {
        //        return NotFound();
        //    }

        //    pointsOfInterestFromStore.Name = pointOfInterest.Name;
        //    pointsOfInterestFromStore.Description = pointOfInterest.Description;

        //    return NoContent();


        //}
        //[HttpPatch("{pointOfinterestid}")]
        //public ActionResult PartiallyUpdatePointOfInterest(int cityId, int pointOfInterestId,
        //JsonPatchDocument<PointForInterestForUpdateDto> patchDocument)
        //{

        //    var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);

        //    if (city == null)
        //    {
        //        return NotFound();
        //    }

        //    var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(c => c.Id == pointOfInterestId);

        //    if(pointOfInterestFromStore == null)
        //    {
        //        return NotFound();
        //    }

        //    var pointOfInterestoPatch = new PointForInterestForUpdateDto()
        //    {
              
        //        Name = pointOfInterestFromStore.Name,
        //        Description = pointOfInterestFromStore.Description
        //    };

        //    patchDocument.ApplyTo(pointOfInterestoPatch,ModelState);


        //    if(!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }


        //    if(!TryValidateModel(pointOfInterestoPatch))
        //    {
        //        return BadRequest(ModelState);
        //    }


        //    pointOfInterestFromStore.Name = pointOfInterestoPatch.Name;
        //    pointOfInterestFromStore.Description = pointOfInterestoPatch.Description;
            
        //    return NoContent();
        //}


        //[HttpDelete("{pointOfInterestId}")]

        //public ActionResult DeletePoinrodInterest(int cityId, int pointOfInterestId) 
        //{
        //    var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);

        //    if (city == null)
        //    {
        //        return NotFound();
        //    }

        //    var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(c => c.Id == pointOfInterestId);

        //    if (pointOfInterestFromStore == null)
        //    {
        //        return NotFound();
        //    }

        //    city.PointsOfInterest.Remove(pointOfInterestFromStore);
        //    _mailService.Send("Testing Subject","Your interest is deleted");
        //    return NoContent();

        //}

    }


    
}
