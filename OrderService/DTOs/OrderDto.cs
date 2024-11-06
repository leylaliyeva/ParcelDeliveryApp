using OrderService.Enums;

namespace OrderService.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Destination { get; set; }
        public string PickupLocation { get; set; }
        public double Weight { get; set; }
        public int? CourierId { get; set; }
    }
}
