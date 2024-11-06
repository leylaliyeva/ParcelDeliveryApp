// Controllers/TrackingController.cs
using Microsoft.AspNetCore.Mvc;
using TrackingService.Data;
using TrackingService.Models;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace TrackingService.Controllers
{
    [ApiController]
    [Route("api/tracking")]
    public class TrackingController : ControllerBase
    {
        private readonly TrackingDbContext _context;
        private readonly HttpClient _httpClient;

        public TrackingController(TrackingDbContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet("{orderId}")]
        public async Task<ActionResult<TrackingInfo>> GetTrackingInfo(int orderId)
        {
            var trackingInfo = await _context.TrackingInfos.FirstOrDefaultAsync(t => t.OrderId == orderId);
            if (trackingInfo == null)
                return NotFound();
            return Ok(trackingInfo);
        }

        [HttpPut("{orderId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateTrackingInfo(int orderId, [FromBody] TrackingInfo trackingInfo)
        {
            var existingTrackingInfo = await _context.TrackingInfos.FindAsync(orderId);
            if (existingTrackingInfo == null)
                return NotFound();

            existingTrackingInfo.CurrentLatitude = trackingInfo.CurrentLatitude;
            existingTrackingInfo.CurrentLongitude = trackingInfo.CurrentLongitude;
            existingTrackingInfo.LastUpdated = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("order/{orderId}/status")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<string>> GetOrderStatus(int orderId)
        {
            var response = await _httpClient.GetAsync($"/api/orders/{orderId}");
            if (response.IsSuccessStatusCode)
            {
                var orderStatus = await response.Content.ReadAsStringAsync();
                return Ok(orderStatus);
            }
            return StatusCode((int)response.StatusCode);
        }

        [HttpGet("couriers")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<Courier>>> GeCouriers(int orderId)
        {
            var response = await _context.Couriers.ToListAsync<Courier>();
            return Ok(response);
        }
    }
}
