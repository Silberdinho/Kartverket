namespace Kartverket.Models.ViewModels
{
    /// <summary>
    /// Represents the error view model containing error details
    /// </summary>
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}