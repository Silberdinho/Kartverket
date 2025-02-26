using System.ComponentModel.DataAnnotations;

namespace Kartverket.Models.ViewModels
{
    /// <summary>
    /// Represents the reset password view model containing user credentials and reset details
    /// </summary>
    public class ResetPasswordViewModel
    {

        [Required]
        public string Email { get; set; }


        /// <summary>
        /// gets or sets the token for password reset validation.
        /// This field is required to verify the reset request
        /// </summary>
        [Required]
        public string Token { get; set; }

        [Required, DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 6)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
    }
}