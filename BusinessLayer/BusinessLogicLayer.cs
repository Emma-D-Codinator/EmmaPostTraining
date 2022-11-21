using RepoLayer;
using Models;


namespace BusinessLayer;
public class BusinessLogicLayer
{
    private readonly RepoAccessLayer _repoLayer;

    public BusinessLogicLayer()
    {

        this._repoLayer = new RepoLayer.RepoAccessLayer();
    }

    //Check for existing home before inserting 
    public async Task<House?> insertHouseAsync(House home, byte[]? Imagebyte)
    {
        bool m = await this._repoLayer.CheckExisitngHomeAsync(home);

        if (m)
        {
            return null;
        }
        else
        {


            House home1 = await this._repoLayer.insertHouseAsync(home, Imagebyte);

            
                return home1;

          
        }


    }



    //Insert furniture
    public async Task<Furniture?> insertFurnitureAsync(Furniture furniture, byte[]? ImagebyteF, int? HouseID)
    {

        double? f = await this._repoLayer.countFurnitureFootageAsync(HouseID);

        double? h = await this._repoLayer.HouseFootageAsync(HouseID);


        // Set a condition to make sure that the house furniture limit is not exceeded.

        if (h / 2 < f)
        {
            return null;

        }
        else
        {

            Furniture furniture1 = await this._repoLayer.insertFurnitureAsync(furniture, ImagebyteF);

            return furniture1;

        }


        
    }



    //Count the number of times a furniture type occurs
    public async Task<int?> countFurnitureTypeAsync(string Furniture_name_type)
    {
        int? fc = await this._repoLayer.countFurnitureTypeAsync(Furniture_name_type);

        return fc;

    }

    //Gets the total count of the square footage for the furniture in a specific house
    public async Task<double?> countFurnitureFootageAsync(int? HouseID)
    {
        double ff = await this._repoLayer.countFurnitureFootageAsync(HouseID);

        return ff;

    }

    //Gets the square footage for a specific home
    public async Task<double?> HouseFootageAsync(int? HouseID)
    {
        double hf = await this._repoLayer.HouseFootageAsync(HouseID);

        return hf;

    }


    //Get furnitures using a specified house as the input parameter
    public async Task<List<FurnitureDto?>> getAllFurnitureByHouseAsync(string House_name_type)
    {
        List<FurnitureDto?> FunitureList = await this._repoLayer.getAllFurnitureByHouseAsync(House_name_type);

        return FunitureList;

    }


    //Get home by home ID
    public async Task<HouseDto?> getHouseByIdAsync(int ID)
    {
        HouseDto? p = await this._repoLayer.getHouseByIdAsync(ID);
  
        return p;

    }



    //Get all homes

    public async Task<List<HouseDto?>> getAllHomesAsync()
    {

        List<HouseDto?> HouseList = await this._repoLayer.getAllHomesAsync();

       
        return HouseList;

    }



}
