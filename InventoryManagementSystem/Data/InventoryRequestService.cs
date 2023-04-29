using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace InventoryManagementSystem.Data
{
    public class InventoryRequestService
    {

        private static void SaveAll(Guid userId, List<InventoryReq> inventories)
        {
            string appDataDirectoryPath = Utils.GetAppDirectoryPath();
            string inventoriesReqFilePath = Utils.GetInventoriesReqFilePath();

            if (!Directory.Exists(appDataDirectoryPath))
            {
                Directory.CreateDirectory(appDataDirectoryPath);
            }

            var json = JsonSerializer.Serialize(inventories);
            File.WriteAllText(inventoriesReqFilePath, json);
        }

       

        public static List<InventoryReq> ReadData()
        {
            string inventoriesFilePath = Utils.GetInventoriesReqFilePath();

            if (!File.Exists(inventoriesFilePath))
            {
                return new List<InventoryReq>();
            }
            var json = File.ReadAllText(inventoriesFilePath);
            if (json == "")
            {
                return new List<InventoryReq>();
            }
            return JsonSerializer.Deserialize<List<InventoryReq>>(json);
        }


  



        public static List<InventoryReq> GetAll()
        {
            string inventoriesFilePath = Utils.GetInventoriesReqFilePath();
            if (!File.Exists(inventoriesFilePath))
            {
                return new List<InventoryReq>();
            }

            var json = File.ReadAllText(inventoriesFilePath);

            return JsonSerializer.Deserialize<List<InventoryReq>>(json);
        }

        public static List<InventoryReq> Create(Guid Id, string item, int quantity, string requestedBy, string approvedBy, DateTime requestedAt)
        {


            List<InventoryReq> inventories = GetAll();
            inventories.Add(new InventoryReq
            {
                Item = item,
                Quantity = quantity,
                RequestedBy = requestedBy,
                ApprovedBy = approvedBy,
                RequestedAt = requestedAt


            });
            SaveAll(Id, inventories);
            return inventories;
        }

        public static List<InventoryReq> Delete(Guid Id, Guid id)
        {
            List<InventoryReq> inventories = GetAll();
            InventoryReq inventory = inventories.FirstOrDefault(x => x.Id == id);

            if (inventory == null)
            {
                throw new Exception("Inventory not found.");
            }

            inventories.Remove(inventory);
            SaveAll(Id, inventories);
            return inventories;
        }

     

        public static List<InventoryReq> Update(Guid Id, Guid id, string item, int quantity, string requestedBy, string approvedBy, DateTime requestedAt)
        {
            List<InventoryReq> inventories = GetAll();
            InventoryReq inventoryToUpdate = inventories.FirstOrDefault(x => x.Id == id);

            if (inventoryToUpdate == null)
            {
                throw new Exception("Inventory not found.");
            }

            inventoryToUpdate.Item = item;
            inventoryToUpdate.Quantity = quantity;
            inventoryToUpdate.RequestedBy = requestedBy;
            inventoryToUpdate.ApprovedBy = approvedBy;
            inventoryToUpdate.RequestedAt = requestedAt;
            SaveAll(Id, inventories);
            return inventories;
        }
    }
}




