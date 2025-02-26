using System.ComponentModel.DataAnnotations;

namespace Kartverket.Models.ViewModels
{
    /// <summary>
    /// Represents the login viewmodel containing user credentials
    /// </summary>
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
