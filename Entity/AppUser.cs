using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Entity
{
    public class AppUser
    {
        public int ID { get; set; }

        [Required]
        public required string UserName { get; set; }

        [Required]
        public required byte[] PasswordHash { get; set; }

        [Required]
        public required byte[] PasswordSalt { get; set; }
    }
}
