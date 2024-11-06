using OrderService.Enums;
using System.ComponentModel.DataAnnotations;

namespace OrderService.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public required string CreatedBy { get; set; }  

        [EnumDataType(typeof(OrderStatus))]
        public OrderStatus Status { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public required string Destination { get; set; }

        public required string PickupLocation { get; set; }

        public double Weight { get; set; }

        public int? CourierId { get; set; } 
    }
}
