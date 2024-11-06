using System.ComponentModel.DataAnnotations;

namespace OrderService.DTOs
{
    public class CreateOrderDto
    {
        [Required]
        public required string Destination { get; set; }

        [Required]
        public required string PickupLocation { get; set; }

        [Required]
        public double Weight { get; set; }
    }
}
