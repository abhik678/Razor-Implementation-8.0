using System.ComponentModel.DataAnnotations;

namespace Razor_UTC.Models
{
    public class Credentials
    {
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
