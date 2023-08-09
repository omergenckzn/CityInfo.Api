using CityInfo.Api.Models;

namespace CityInfo.Api
{
    public class CitiesDataStore
    {

        public List<CityDto> Cities { get; set; }
        public static CitiesDataStore Current { get; set; } = new CitiesDataStore();


        public CitiesDataStore()
        {
            Cities = new List<CityDto>() {
            new CityDto() { Id = 1, Name = "NY", Description = "ASDKASF",
                PointsOfInterest = new List<PointOfInterestDto>(){
            new PointOfInterestDto() { Id = 4, Description = "Zort",Name = "SHazam"}, new PointOfInterestDto() { Id = 4, Description = "safsd",Name = "asdasd"},            } },
            new CityDto() { Id = 2,Description = "sdsaf",Name = "Antewr",
                PointsOfInterest = new List<PointOfInterestDto>(){ new PointOfInterestDto() { Id = 4, Description = "Zort", Name = "SHazam" }, new PointOfInterestDto() { Id = 4, Description = "safsd", Name = "asdasd" }, } },
            new CityDto() { Id = 3,Name = "Paris",Description = "SDFSDF",
                PointsOfInterest = new List < PointOfInterestDto >()
            {
            new PointOfInterestDto() { Id = 4, Description = "Zort",Name = "SHazam"}, new PointOfInterestDto() { Id = 4, Description = "safsd",Name = "asdasd"},
            } },
            };
        }


    }
}
