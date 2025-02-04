using System;
using System.Collections.Generic;

class Program
{
 
    class Item
    {
        public int ItemId { get; set; }
        public int CurrentStock { get; set; }
        public int ForecastedDemand { get; set; }
        public int ReorderCostPerUnit { get; set; }
        public int BatchSize { get; set; }
    }

    static List<(int itemId, int unitsToOrder)> ReorderInventory(List<Item> items)
    {
        List<(int, int)> reorderPlan = new List<(int, int)>();

        foreach (var item in items)
        {
            if (item.CurrentStock < item.ForecastedDemand) 
            {
                int deficit = item.ForecastedDemand - item.CurrentStock;
                int batchesNeeded = (deficit / item.BatchSize) + (deficit % item.BatchSize > 0 ? 1 : 0);
                int unitsToOrder = batchesNeeded * item.BatchSize;

                reorderPlan.Add((item.ItemId, unitsToOrder));
            }
        }

        return reorderPlan;
    }

    static void Main()
    {
        List<Item> inventoryData = new List<Item>
        {
            new Item { ItemId = 101, CurrentStock = 50, ForecastedDemand = 120, ReorderCostPerUnit = 5, BatchSize = 20 },
            new Item { ItemId = 102, CurrentStock = 200, ForecastedDemand = 150, ReorderCostPerUnit = 3, BatchSize = 30 },
            new Item { ItemId = 103, CurrentStock = 10, ForecastedDemand = 80, ReorderCostPerUnit = 2, BatchSize = 15 }
        };

        List<(int, int)> result = ReorderInventory(inventoryData);

        Console.WriteLine("Reorder Plan:");
        foreach (var order in result)
        {
            Console.WriteLine($"Item ID: {order.Item1}, Units to Order: {order.Item2}");
        }
    }
}
