using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Entity
{
    public class AppUser
    {
        public int ID { get; set; }

        [Required]
        public required string UserName { get; set; }

        [Required]
        [Phone]
        public required string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }
    }
}
