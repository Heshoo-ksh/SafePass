using System.ComponentModel.DataAnnotations;

namespace SafePass.Data
{
     public class CreditCard
     {
          [Key]
          public Guid Id { get; set; }

          [Required(ErrorMessage = "Name is required and cannot be empty.")]
          public string? Name { get; set; }

          [Required(ErrorMessage = "Card number is required and cannot be empty.")]
          public string? Number { get; set; }

          [Required(ErrorMessage = "CVV is required and cannot be empty.")]
          public string? CVV { get; set; }

          [Required(ErrorMessage = "ZipCode is required and cannot be empty.")]
          public string? ZipCode { get; set; }
     }
}
