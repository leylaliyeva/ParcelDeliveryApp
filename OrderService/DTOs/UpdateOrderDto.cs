using OrderService.Enums;
using System.ComponentModel.DataAnnotations;

namespace OrderService.DTOs
{
    public class UpdateOrderDto
    {
        [Required]
        public int Id { get; set; }

        public string Destination { get; set; }

        public string PickupLocation { get; set; }

        public double? Weight { get; set; }

        public OrderStatus? Status { get; set; }
    }
}
