using System.ComponentModel.DataAnnotations;

namespace hello_world_web.models
{
    public class PointOfInterestForCreationDto
    {
        [Required(ErrorMessage = "you should provide a name value.")]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(200, ErrorMessage = "you should type max 200 characters")]
        public string Description { get; set; }
    }
}