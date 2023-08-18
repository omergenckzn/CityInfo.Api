using System.ComponentModel.DataAnnotations;

namespace CityInfo.Api.Models
{
    public class PointOfInterestForCreationDto
    {
        [Required(ErrorMessage = "You Should Provide a name value")]
        [MaxLength(30)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(30)]
        public string? Description { get; set; }
    }
}
