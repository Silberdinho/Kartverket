namespace Kartverket.Models.DomainModels
{
    /// <summary>
    /// Represents the submission status of a reported map error
    /// </summary>
    public class SubmitStatus
    {
        public int Id { get; set; }

        public string? Status { get; set; }
    }
}