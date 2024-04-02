using System.ComponentModel.DataAnnotations;

namespace Razor_UTC.Models
{
    public class UserInformation
    {
        [Required]
        [Display(Name = "Username")]
        public int Id { get; set; } = 0;

        [Required]
        [Display(Name = "First Name")]
        public string FName { get; set; } = default!;

        [Required]
        [Display(Name = "Last Name")]
        public string LName { get; set; } = default!;

        [Required]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; } = default!;

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = default!;

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; } = default!;

        [Required]
        [Display(Name = "Mobile Number")]
        public long PhoneNumber { get; set; } = default!;
    }
}
