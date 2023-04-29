using System;
namespace InventoryManagementSystem.Data
{
    public class ChartService
    {

        public static Dictionary<string, int> GetChartData()
        {
            List<InventoryReq> acquiredItems = InventoryRequestService.ReadData();

            var itmDictionary = new Dictionary<string, int>();

            foreach (var item in acquiredItems)
            {
                if (itmDictionary.ContainsKey(item.Item))
                {
                    itmDictionary[item.Item] = itmDictionary[item.Item] + item.Quantity;
                }
                else
                {
                    itmDictionary[item.Item] = item.Quantity;
                }
            }
            return itmDictionary;
        }
    }
}

