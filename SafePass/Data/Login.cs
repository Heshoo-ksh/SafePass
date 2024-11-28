using System.ComponentModel.DataAnnotations;

namespace SafePass.Data
{
     public class Login
     {
          [Key]
          public Guid Id { get; set; }

          [Required(ErrorMessage = "Username is required and cannot be empty.")]
          public string? UserName { get; set; }

          [Required(ErrorMessage = "Password is required and cannot be empty.")]
          public string? Password { get; set; }

          [Required(ErrorMessage = "URL is required and cannot be empty.")]
          public string? URL { get; set; }
     }
}
