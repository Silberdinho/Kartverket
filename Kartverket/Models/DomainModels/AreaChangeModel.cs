using System.ComponentModel.DataAnnotations;

namespace Kartverket.Models.DomainModels
{
    /// <summary>
    /// Represents the model for tracking area change requests
    /// </summary>
    public class AreaChangeModel
    {

        public int Id { get; set; }
        public string? UserName { get; set; }

        public DateTime? Date { get; set; } = DateTime.Now;
        public string? Email { get; set; }

        public string? CaseWorker { get; set; }
        public string? AreaJson { get; set; }
        public string? Description { get; set; }
        public string? Kommunenavn { get; set; }
        public string? Fylkenavn { get; set; }

        public SubmitStatus? SubmitStatusModel { get; set; }

        public int StatusId { get; set; }
        public string? StatusDescription { get; set; }

    }
}