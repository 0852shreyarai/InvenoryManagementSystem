using System;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Data
{
    public class InventoryReq
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required(ErrorMessage = "Please provide the Item name.")]
        public string Item { get; set; }
        public int Quantity { get; set; }
        public string RequestedBy { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime RequestedAt { get; set; } = DateTime.Now;
    }
}


