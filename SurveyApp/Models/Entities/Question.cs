namespace SurveyApp.Models.Entities
{
    public class Question
    {
        public Guid QuestionId { get; set; }
        public string? QuestionName { get; set; }
        public string? AnswerType { get; set; }

        public ICollection<Answer>? answers { get; set; }

        public Guid? ChoiceId { get; set; }
        public ICollection<Choice>? choices { get; set; }

        public Guid? SurveyId { get; set; }
        public Survey? Survey { get; set; }
    }
}
