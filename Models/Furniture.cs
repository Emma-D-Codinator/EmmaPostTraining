using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;


namespace Models;
public class Furniture
{
 
    public int FurnitureID { get; set; }

    public string? Furniture_name_type { get; set; }

    public double? FurniturePrice { get; set; }

    [Required(ErrorMessage = "Pick an Image")]
    public IFormFile? FurnitureImage { get; set; }

    public double? FurnitureFootage { get; set; }

    public int? HouseID { get; set; }

    
}
