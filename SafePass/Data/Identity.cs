using System;
using System.ComponentModel.DataAnnotations;

namespace SafePass.Data
{
     public class Identity
     {
          [Key]
          public Guid Id { get; set; }

          [Required(ErrorMessage = "Holder Name is required and cannot be empty.")]
          public string HolderName { get; set; }

          [Required(ErrorMessage = "ID Number is required and cannot be empty.")]
          public string IDNumber { get; set; }

          [Required(ErrorMessage = "ID Type is required and cannot be empty.")]
          public string IDType { get; set; } // e.g., "Driver's License," "Passport"

          [Required(ErrorMessage = "Expiration Date is required and cannot be empty.")]
          public DateTime? ExpirationDate { get; set; }

          [Required(ErrorMessage = "Issue Date is required and cannot be empty.")]
          public DateTime? IssueDate { get; set; }

          [Required(ErrorMessage = "Date of Birth is required and cannot be empty.")]
          public DateTime? DateOfBirth { get; set; }

          [Required(ErrorMessage = "Gender is required and cannot be empty.")]
          public string Gender { get; set; } // Could also use an enum

          [Required(ErrorMessage = "Address is required and cannot be empty.")]
          public string Address { get; set; }
     }
}
