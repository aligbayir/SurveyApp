namespace SurveyApp.Models.Entities
{
    public class Answer
    {
        public Guid AnswerId { get; set; }
        public string? AnswerName { get; set; }
       
        
        public Guid? UserId { get; set; }
        public User? user { get; set; }

    }
}
