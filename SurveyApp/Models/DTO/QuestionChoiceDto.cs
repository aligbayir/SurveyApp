using SurveyApp.Models.Entities;

namespace SurveyApp.Models.DTO
{
    public class QuestionChoiceDto
    {
        public string? QuestionName { get; set; }
        public string? AnswerType { get; set; }

        public List<string>? choices { get; set; }
    }
}
