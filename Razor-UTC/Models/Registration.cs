using System.ComponentModel.DataAnnotations;

namespace Razor_UTC.Models
{
    public class Registration
    {
        [Required]
        public string Username { get; set; } = default!;

        [Required]
        public string Password { get; set; } = default!;

        [Required]
        public string ConfirmPassword { get; set; } = default!;
    }
}
