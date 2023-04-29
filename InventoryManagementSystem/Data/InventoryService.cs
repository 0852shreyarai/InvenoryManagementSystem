using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace InventoryManagementSystem.Data
{
    public static class InventoryService
    {

        private static void SaveAll(Guid userID, List<Inventory> inventories)
        {
            string appDataDirectoryPath = Utils.GetAppDirectoryPath();
            string inventoriesFilePath = Utils.GetInventoriesFilePath();

            if (!Directory.Exists(appDataDirectoryPath))
            {
                Directory.CreateDirectory(appDataDirectoryPath);
            }

            var json = JsonSerializer.Serialize(inventories);
            File.WriteAllText(inventoriesFilePath, json);

        }
        
  
      
      
        

        public static List<Inventory> GetAll()
        {
            string inventoriesFilePath = Utils.GetInventoriesFilePath();
            if (!File.Exists(inventoriesFilePath))
            {
                return new List<Inventory>();
            }

            var json = File.ReadAllText(inventoriesFilePath);

            return JsonSerializer.Deserialize<List<Inventory>>(json);
        }

        public static List<Inventory> Create(Guid Id, string item, int price, int quantity)
        {


            List<Inventory> inventories = GetAll();
            inventories.Add(new Inventory
            {
                Item = item,
                Quantity = quantity,
                Price = price
            });
            SaveAll(Id, inventories);
            return inventories;
        }

        public static List<Inventory> Delete(Guid Id, Guid id)
        {
            List<Inventory> inventories = GetAll();
            Inventory inventory = inventories.FirstOrDefault(x => x.Id == id);

            if (inventory == null)
            {
                throw new Exception("Inventory not found.");
            }

            inventories.Remove(inventory);
            SaveAll(Id, inventories);
            return inventories;
        }


     



        public static List<Inventory> Update(Guid Id, Guid id, string item, int price, int quantity)
        {
            List<Inventory> inventories = GetAll();
            Inventory inventoryToUpdate = inventories.FirstOrDefault(x => x.Id == id);

            if (inventoryToUpdate == null)
            {
                throw new Exception("Inventory not found.");
            }

            inventoryToUpdate.Item = item;
            inventoryToUpdate.Quantity = quantity;
            inventoryToUpdate.Price = price;
            SaveAll(Id, inventories);
            return inventories;
        }


    }
}




