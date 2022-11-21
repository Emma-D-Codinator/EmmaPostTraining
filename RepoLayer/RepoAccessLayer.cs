using Models;
using System.Data;
using System.Data.SQLite;
using System.Text;

namespace RepoLayer
{

    public class RepoAccessLayer
    {
        
        //Create home inventory
        public async Task<House> insertHouseAsync(House home, byte[]? Imagebyte)
        {
            SQLiteConnection conn = new SQLiteConnection("Data Source=C:/Users/chuks/OneDrive/Desktop/Revature Stuff/FurnitureMover/FurnitureMover.db;");
            using (SQLiteCommand command = new SQLiteCommand($"INSERT INTO House (id, house_type, address, h_footage, image, house_cost)  VALUES (@id, @house_type, @address, @h_footage, @image, @house_cost)", conn))
            {

                command.Parameters.Add("@id", DbType.Int32).Value = home.ID;
                command.Parameters.Add("@house_type", DbType.String).Value = home.House_name_type;
                command.Parameters.Add("@address", DbType.String).Value = home.Address;
                command.Parameters.Add("@h_footage", DbType.Double).Value = home.Footage;
                command.Parameters.Add("@image", DbType.Binary, 20).Value = Imagebyte;
                command.Parameters.Add("@house_cost", DbType.Double).Value = home.HouseCost;

                conn.Open();
                int ret = await command.ExecuteNonQueryAsync();

                conn.Close();
                return home;
            }
        }//EoM


        //Check for existing house before inserting
        public async Task<bool> CheckExisitngHomeAsync(House home)
        {
            SQLiteConnection conn = new SQLiteConnection("Data Source=C:/Users/chuks/OneDrive/Desktop/Revature Stuff/FurnitureMover/FurnitureMover.db;");
            using (SQLiteCommand command = new SQLiteCommand($"SELECT * FROM House WHERE id = @id", conn))
            {
                command.Parameters.AddWithValue("@id", home.ID);
                conn.Open();
                SQLiteDataReader? ret = (SQLiteDataReader?)await command.ExecuteReaderAsync();

                if (ret.Read())
                {

                    HouseDto h = new HouseDto();

                    h.ID = ret.GetInt32(0);
                    h.House_name_type = ret.GetString(1);
                    h.Address = ret.GetString(2);
                    h.Footage = ret.GetDouble(3);

                    byte[] Imagebyte2 = (byte[])ret["image"]; //== image is the picture column
                   
                    h.HouseImage = Imagebyte2;

                    h.HouseCost = ret.GetDouble(4); 

                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }

        }//EoM


        //Create furniture inventory
        public async Task<Furniture> insertFurnitureAsync(Furniture furniture, byte[]? ImagebyteF)
        {
            SQLiteConnection conn = new SQLiteConnection("Data Source=C:/Users/chuks/OneDrive/Desktop/Revature Stuff/FurnitureMover/FurnitureMover.db;");
            using (SQLiteCommand command = new SQLiteCommand($"INSERT INTO Furniture (furniture_id, type, cost, image, footage, house_id)  VALUES (@furniture_id, @type, @cost, @image, @footage, @house_id)", conn))
            {

                command.Parameters.Add("@furniture_id", DbType.Int32).Value = furniture.FurnitureID;
                command.Parameters.Add("@type", DbType.String).Value = furniture.Furniture_name_type;
                command.Parameters.Add("@cost", DbType.Double).Value = furniture.FurniturePrice;
                command.Parameters.Add("@image", DbType.Binary, 20).Value = ImagebyteF;
                command.Parameters.Add("@footage", DbType.Double).Value = furniture.FurnitureFootage;
                command.Parameters.Add("@house_id", DbType.Int32).Value = furniture.HouseID;

                conn.Open();
                int ret = await command.ExecuteNonQueryAsync();

                conn.Close();
                return furniture;
            }
        }//EoM

        
        //Count the number of times a furniture type occurs
        public async Task<int> countFurnitureTypeAsync(string Furniture_name_type)
        {
            SQLiteConnection conn = new SQLiteConnection("Data Source=C:/Users/chuks/OneDrive/Desktop/Revature Stuff/FurnitureMover/FurnitureMover.db;");
              
            
            using (SQLiteCommand command = new SQLiteCommand($"SELECT COUNT(type) From Furniture WHERE type = @type", conn))
            {
                
                command.Parameters.AddWithValue("@type", Furniture_name_type);
                conn.Open();
               
                int count = Convert.ToInt32(await command.ExecuteScalarAsync());

                conn.Close ();
                return count;
            }

            
        }//EoM



        //Gets the total count of the square footage for the furniture in a specific house
        public async Task<double> countFurnitureFootageAsync(int? HouseID)
        {
            SQLiteConnection conn = new SQLiteConnection("Data Source=C:/Users/chuks/OneDrive/Desktop/Revature Stuff/FurnitureMover/FurnitureMover.db;");


            using (SQLiteCommand command = new SQLiteCommand($"SELECT SUM(footage) From Furniture WHERE house_id = @house_id", conn))
            {

                command.Parameters.AddWithValue("@house_id", HouseID);
                conn.Open();

                double sum = Convert.ToDouble(await command.ExecuteScalarAsync());

                conn.Close();
                return sum;
            }


        }//EoM



        //Gets the square footage for a specific home
        public async Task<double> HouseFootageAsync(int? HouseID)
        {
            SQLiteConnection conn = new SQLiteConnection("Data Source=C:/Users/chuks/OneDrive/Desktop/Revature Stuff/FurnitureMover/FurnitureMover.db;");


            using (SQLiteCommand command = new SQLiteCommand($"SELECT h_footage FROM House LEFT JOIN Furniture ON house_id = id WHERE house_id = @house_id;", conn))
            {

                command.Parameters.AddWithValue("@house_id", HouseID);
                conn.Open();

                double houseFootage = Convert.ToDouble(await command.ExecuteScalarAsync());

                conn.Close();
                return houseFootage;
            }


        }//EoM



        //Get furnitures using a specified house as the input parameter

        public async Task<List<FurnitureDto?>> getAllFurnitureByHouseAsync(string House_name_type)
        {
            SQLiteConnection conn = new SQLiteConnection("Data Source=C:/Users/chuks/OneDrive/Desktop/Revature Stuff/FurnitureMover/FurnitureMover.db;");
            using (SQLiteCommand command = new SQLiteCommand($"SELECT * FROM House LEFT JOIN Furniture ON house_id = id WHERE house_type = @house_type;", conn))
            {
                command.Parameters.Add("@house_type", DbType.String).Value = House_name_type;
                conn.Open();
                SQLiteDataReader? ret = (SQLiteDataReader?)await command.ExecuteReaderAsync();

                List<FurnitureDto?> FurnitureList = new List<FurnitureDto?>();

                while (ret.Read())
                {

                    FurnitureDto f = new FurnitureDto();

                    f.FurnitureID = ret.GetInt32(6);
                    f.Furniture_name_type = ret.GetString(7);
                    f.FurniturePrice = ret.GetDouble(8);
                   
                    byte[] Imagebyte2= (byte[])ret.GetValue(9);
                    //byte[] Imagebyte2 = (byte[])ret["image"];

                    f.FurnitureImage = Imagebyte2;

                    f.FurnitureFootage = ret.GetDouble(10);

                    f.HouseID = ret.GetInt32(11);

                    FurnitureList.Add(f);


                }
                conn.Close();
                return FurnitureList;
            }

        }//EoM





        //Get all Homes

        public async Task<List<HouseDto?>> getAllHomesAsync()
        {
            SQLiteConnection conn = new SQLiteConnection("Data Source=C:/Users/chuks/OneDrive/Desktop/Revature Stuff/FurnitureMover/FurnitureMover.db;");
            using (SQLiteCommand command = new SQLiteCommand($"SELECT * FROM House", conn))
            {
                conn.Open();
                SQLiteDataReader? ret = (SQLiteDataReader?)await command.ExecuteReaderAsync();

                List<HouseDto?> HouseList = new List<HouseDto?>();

                while (ret.Read())
                {

                    HouseDto h = new HouseDto();

                    h.ID = ret.GetInt32(0);
                    h.House_name_type = ret.GetString(1);
                    h.Address = ret.GetString(2);
                    h.Footage = ret.GetDouble(3);

                    byte[] Imagebyte2 = (byte[])ret["image"];
                    
                     // byte[] Image2= (byte[])ret.GetValue(4);
                    
                    h.HouseImage = Imagebyte2;

                    h.HouseCost = ret.GetDouble(5);

                    HouseList.Add(h);


                }
                conn.Close();
                return HouseList;
            }

        }//EoM



        //Get House by ID

        public async Task<HouseDto?> getHouseByIdAsync(int ID)
        {
            
            SQLiteConnection conn = new SQLiteConnection("Data Source=C:/Users/chuks/OneDrive/Desktop/Revature Stuff/FurnitureMover/FurnitureMover.db;");
            using (SQLiteCommand command = new SQLiteCommand($"SELECT * FROM House WHERE id = @id", conn))
            {
                command.Parameters.AddWithValue("@id", ID);           
                conn.Open();
                SQLiteDataReader? ret = (SQLiteDataReader?)await command.ExecuteReaderAsync();

                if (ret.Read())
                {

                    HouseDto h = new HouseDto();

                    h.ID = ret.GetInt32(0);
                    h.House_name_type = ret.GetString(1);
                    h.Address = ret.GetString(2);
                    h.Footage = ret.GetDouble(3);

                    byte[] Imagebyte2 = (byte[])ret["image"];
                    
                    h.HouseImage = Imagebyte2;

                    h.HouseCost = ret.GetDouble(5);

                    conn.Close();
                    return h;
                }
                else
                {
                    conn.Close();
                    return null;
                }
            }

        }//EoM




        //update house image
        public async Task<bool> updateHouseImage(byte[]? Imagebyte, int ID)
        {
            SQLiteConnection conn = new SQLiteConnection("Data Source=C:/Users/chuks/OneDrive/Desktop/Revature Stuff/FurnitureMover/FurnitureMover.db;");
            using SQLiteCommand command = new SQLiteCommand("UPDATE House SET image = @image WHERE id = @id", conn);
            command.Parameters.AddWithValue("@image", Imagebyte);
            command.Parameters.AddWithValue("@id", ID);

            conn.Open();
            bool ret = (await command.ExecuteNonQueryAsync()) > 0;
            conn.Close();

            return ret;
        }

}
}





