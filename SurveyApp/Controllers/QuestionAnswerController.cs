using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyApp.Data;
using SurveyApp.Models.DTO;
using SurveyApp.Models.Entities;

namespace SurveyApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionAnswerController : ControllerBase
    {
        private readonly SurveyDbContext _surveyDbContext;

        public QuestionAnswerController(SurveyDbContext surveyDbContext)
        {
            this._surveyDbContext = surveyDbContext;
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetQuestionAndAnswer(Guid id)
        {
            List<QuestionChoiceDto> qesto = new List<QuestionChoiceDto>();
            var question = _surveyDbContext.Questions.Where(x => x.SurveyId == id).ToList();
            foreach (var item in question)
            {
                QuestionChoiceDto qe = new QuestionChoiceDto();
                qe.QuestionName= item.QuestionName;
                qe.AnswerType= item.AnswerType;
                qe.choices = _surveyDbContext.Choices.Where(x=>x.QuestionId==item.QuestionId).Select(x=>x.ChoiceName).ToList();
                qesto.Add(qe);
            }
            if (qesto == null)
            {
                return NotFound();
            }
            return Ok(qesto);
        }
    }
}
