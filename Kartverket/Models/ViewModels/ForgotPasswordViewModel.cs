using System.ComponentModel.DataAnnotations;

namespace Kartverket.Models.ViewModels
{
    /// <summary>
    /// Represents a viewmodel for the forgot password functionality.
    /// </summary>
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }
    }
}