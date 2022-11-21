using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;


namespace Models
{
    public class House
    {

        public int? ID { get; set; }

        public string? House_name_type { get; set; }

        public string? Address { get; set; }

        public double? Footage { get; set; }

        [Required(ErrorMessage = "Pick an Image")]
        public IFormFile? HouseImage { get; set; }

        public double? HouseCost { get; set; }

    }
}