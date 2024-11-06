using System.ComponentModel.DataAnnotations;

namespace TrackingService.Models
{
    public class TrackingInfo
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public double CurrentLatitude { get; set; }
        public double CurrentLongitude { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
