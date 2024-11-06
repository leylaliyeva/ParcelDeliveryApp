namespace TrackingService.Models
{
    public class Courier
    {
            public int CourierId { get; set; } // Linked to the User
            public CourierStatus Status { get; set; } // Enum: Available, Busy, etc.
    }
}
