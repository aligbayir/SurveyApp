namespace SurveyApp.Models.Entities
{
    public class Choice
    {
        public Guid ChoiceId { get; set; }
        public string? ChoiceName { get; set; }


        public Guid? QuestionId { get; set; }
        public Question? Question { get; set; }

        public Guid? SurveyId { get; set; }
        public Survey? Survey { get; set; }
    }
}
