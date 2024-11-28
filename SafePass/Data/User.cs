using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace SafePass.Data
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        // username/email
        [Required(ErrorMessage = "email field cannot be empty.")]
        public string? UserName { get; set; }


        // password
        [Required(ErrorMessage = "password field cannot be empty.")]
        public string? Password { get; set; }


    }
}
