using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SafePass.Data
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        // Username/Email
        [Required(ErrorMessage = "The username field is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string? UserName { get; set; }

        // Password
        [Required(ErrorMessage = "The password field is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        public string? Password { get; set; }

        // Security Question 1
        [Required(ErrorMessage = "Security Question 1 is required.")]
        public string? SecurityQuestion1 { get; set; }

        [Required(ErrorMessage = "Answer to Security Question 1 is required.")]
        public string? SecurityAnswer1 { get; set; }

        // Security Question 2
        [Required(ErrorMessage = "Security Question 2 is required.")]
        public string? SecurityQuestion2 { get; set; }

        [Required(ErrorMessage = "Answer to Security Question 2 is required.")]
        public string? SecurityAnswer2 { get; set; }

        // Security Question 3
        [Required(ErrorMessage = "Security Question 3 is required.")]
        public string? SecurityQuestion3 { get; set; }

        [Required(ErrorMessage = "Answer to Security Question 3 is required.")]
        public string? SecurityAnswer3 { get; set; }

        /// <summary>
        /// Evaluates the strength of the password and returns its health status.
        /// </summary>
        public string GetPasswordHealth()
        {
            if (string.IsNullOrWhiteSpace(Password))
                return "Weak";

            int score = 0;

            // Check length
            if (Password.Length >= 12) score++;
            if (Password.Length >= 16) score++;

            // Check for uppercase
            if (Regex.IsMatch(Password, @"[A-Z]")) score++;

            // Check for lowercase
            if (Regex.IsMatch(Password, @"[a-z]")) score++;

            // Check for numbers
            if (Regex.IsMatch(Password, @"[0-9]")) score++;

            // Check for special characters
            if (Regex.IsMatch(Password, @"[\W_]")) score++;

            return score switch
            {
                >= 5 => "Strong",
                >= 3 => "Medium",
                _ => "Weak"
            };
        }


    }

    public class RegisterRequest
    {
        [Required(ErrorMessage = "The username field is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "The password field is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "The confirm password field is required.")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Security Question 1 is required.")]
        public string SecurityQuestion1 { get; set; } = string.Empty;

        [Required(ErrorMessage = "Answer to Security Question 1 is required.")]
        public string SecurityAnswer1 { get; set; } = string.Empty;

        [Required(ErrorMessage = "Security Question 2 is required.")]
        public string SecurityQuestion2 { get; set; } = string.Empty;

        [Required(ErrorMessage = "Answer to Security Question 2 is required.")]
        public string SecurityAnswer2 { get; set; } = string.Empty;

        [Required(ErrorMessage = "Security Question 3 is required.")]
        public string SecurityQuestion3 { get; set; } = string.Empty;

        [Required(ErrorMessage = "Answer to Security Question 3 is required.")]
        public string SecurityAnswer3 { get; set; } = string.Empty;
    }
}