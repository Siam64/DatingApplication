using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Entity.DTOs
{
    public class LoginDto
    {
            [Required]
            public string? Username { get; set; }

            [Required]
            public string? Password { get; set; }
        
    }
}
