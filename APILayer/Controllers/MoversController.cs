using BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using Models;
namespace APILayer.Controllers;

[ApiController]
[Route("[controller]")]
public class MoversController : ControllerBase
{

    
    private readonly BusinessLogicLayer _PostProduct;

    public MoversController()
    {

        this._PostProduct = new BusinessLogicLayer();

    }




    //Insert house into the inventory
    [HttpPost("Create-House")]

    public async Task<ActionResult> insertHouseAsync([FromForm] House home)
    {


        // Convert image to byte array
        var image = home.HouseImage;

        byte[]? Imagebyte = Array.Empty<byte>();

        if (image != null)
        {
            await using var memoryStream = new MemoryStream();
            await image!.CopyToAsync(memoryStream);
            Imagebyte = memoryStream.ToArray();

        }

        //Check for space between strings in the house name
        if (home.House_name_type.Any(Char.IsWhiteSpace))
        {
            home.House_name_type = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(home.House_name_type.ToLower());
            home.House_name_type = home.House_name_type.Replace(" ", string.Empty);
        }


        House? house1 = await this._PostProduct.insertHouseAsync(home, Imagebyte);


        //Check if records is returned for house
        if (house1 != null)
        {
          
            return Ok(new { status = true, message = "House Posted Successfully" });

        }
        else
        {

            return BadRequest("This house already exists in the DataBase.");
        }


    }//EoM




    //insert furniture into database
    [HttpPost("Create-Furniture")]

    public async Task<ActionResult> insertFurnitureAsync( [FromForm] Furniture furniture)
    {

        int? HouseID = furniture.HouseID;
         
        // Convert furniture image to byte array
        var image = furniture.FurnitureImage;

        byte[]? ImagebyteF = Array.Empty<byte>();

        if (image != null)
        {
            await using var memoryStream = new MemoryStream();
            await image!.CopyToAsync(memoryStream);
            ImagebyteF = memoryStream.ToArray();

        }

        //Check for space between strings in the furniture name, and if any,
        //capitalizes the first letter and removes the white spaces.
        if (furniture.Furniture_name_type.Any(Char.IsWhiteSpace))
        {
            furniture.Furniture_name_type = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(furniture.Furniture_name_type.ToLower());    
            furniture.Furniture_name_type = furniture.Furniture_name_type.Replace(" ", string.Empty);
        }

        Furniture? furniture1 = await this._PostProduct.insertFurnitureAsync(furniture, ImagebyteF, HouseID);

        

        //Check if record is returned for furniture

        if (furniture1 != null)
        {

            return Ok(new { status = true, message = "Furniture Posted Successfully" });

        }
        else
        {

            return BadRequest("You have exceeded the house furniture limit");
        }

      
    }//EoM



    //Count the number of times a furniture type occurs
    [HttpGet("Count-furniturebytype")]
    public async Task<ActionResult<int?>> countFurnitureTypeAsync([FromQuery] string Furniture_name_type)
    {

        int? tc = await this._PostProduct.countFurnitureTypeAsync(Furniture_name_type);

        return Ok(tc);

    }//EOM




    //Gets the total count of the square footage for the furniture in a specific house
    [HttpGet("Sum-furniturefootage")]
    public async Task<ActionResult<double>> countFurnitureFootageAsync([FromQuery]int? HouseID)
    {

        double? f = await this._PostProduct.countFurnitureFootageAsync(HouseID);

        return Ok(f);

    }//EOM



    //Get furnitures using a specified house as the input parameter
    [HttpGet("get-allfurnituresbyhouse")]
    public async Task<ActionResult<List<FurnitureDto?>>> getAllFurnitureByHouseAsync([FromQuery] string House_name_type)
    {

        List<FurnitureDto?> FunitureList = await this._PostProduct.getAllFurnitureByHouseAsync(House_name_type);


        return Ok(FunitureList);

    }
    //EoM




    //Get house by ID
    [HttpGet("get-housebyId")]
    public async Task<ActionResult<HouseDto?>> getHouseByIdAsync(int ID)
    {
        
        HouseDto? p = await this._PostProduct.getHouseByIdAsync(ID);


        return Ok(p);

    }//EOM




    //Get all homes
    [HttpGet("get-allhouses")]
    public async Task<ActionResult<List<HouseDto?>>> getAllHomesAsync()
    {

        List<HouseDto?> HouseList = await this._PostProduct.getAllHomesAsync();

       
        return Ok(HouseList);

    }
    //EoM
    
     


}




