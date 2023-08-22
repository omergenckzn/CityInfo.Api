
using AutoMapper;

namespace CityInfo.Api.Profiles
{
    public class PointOfInterestProfile : Profile
    {
        public PointOfInterestProfile()
        {
            CreateMap<Entities.PointOfInterest,Models.PointOfInterestDto>();
            CreateMap<Models.PointOfInterestForCreationDto, Entities.PointOfInterest>();
            CreateMap<Models.PointForInterestForUpdateDto,Entities.PointOfInterest>();
            CreateMap<Entities.PointOfInterest,Models.PointForInterestForUpdateDto>();
        }

    }
}
