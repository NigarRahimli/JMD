using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace JMD.DTOs
{
    public class RegisterDTO
    {
        [MaxLength(15)]
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordRepeat { get; set; }
    }
}
