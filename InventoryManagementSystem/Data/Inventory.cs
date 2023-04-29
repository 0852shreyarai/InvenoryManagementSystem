using System;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Data
{
    public class Inventory
    {

        public Guid Id { get; set; } = Guid.NewGuid();
        [Required(ErrorMessage = "Please provide the Item name.")]
        public string Item { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }

    }
}

