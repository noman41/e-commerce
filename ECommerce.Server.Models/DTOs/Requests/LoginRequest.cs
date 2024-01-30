using System.ComponentModel.DataAnnotations;

namespace ECommerce.Server.Models.DTOs.Requests
{
    public class LoginRequest
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
