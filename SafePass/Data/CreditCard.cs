using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace SafePass.Data
{
     public class CreditCard
     {
          [Key]
          public Guid Id { get; set; }

          [Required(ErrorMessage = "Name is required and cannot be empty.")]
          [StringLength(50, ErrorMessage = "Name must not exceed 50 characters.")]
          public string? Name { get; set; }

          [Required(ErrorMessage = "Cardholder Name is required and cannot be empty.")]
          [StringLength(50, ErrorMessage = "Cardholder Name must not exceed 50 characters.")]
          public string? CardHolderName { get; set; }

          [Required(ErrorMessage = "Card number is required and cannot be empty.")]
          [StringLength(16, MinimumLength = 13, ErrorMessage = "Card number must be between 13 and 16 digits.")]
          //[CreditCard(ErrorMessage = "Invalid card number.")]
          public string? Number { get; set; }

          [Required(ErrorMessage = "CVV is required and cannot be empty.")]
          [RegularExpression(@"^\d{3,4}$", ErrorMessage = "CVV must be 3 or 4 digits.")]
          public string? CVV { get; set; }

          [Required(ErrorMessage = "Zip Code is required and cannot be empty.")]
          [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Zip Code must be a valid 5 or 9 digit code.")]
          public string? ZipCode { get; set; }

          [Required(ErrorMessage = "Expiration Date is required and cannot be empty.")]
          [DataType(DataType.Date)]
          public DateTime? ExpirationDate { get; set; }
     }
}
