using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Entity.DTOs
{
    public class UserDTO
    {
        [Required]
        public String Username { get; set; }
        [Required]
        public String Token { get; set; }
    }
}
