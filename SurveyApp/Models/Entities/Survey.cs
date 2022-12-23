namespace SurveyApp.Models.Entities
{
    public class Survey
    {
        public Guid SurveyId { get; set; }
        public string? SurveyDescription { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public ICollection<Question>? questions { get; set; }
    }
}
