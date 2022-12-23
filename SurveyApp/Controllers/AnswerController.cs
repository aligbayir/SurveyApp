using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurveyApp.Data;
using SurveyApp.Models.Entities;

namespace SurveyApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class AnswerController : ControllerBase
    {
        private readonly SurveyDbContext _surveyDbContext;

        public AnswerController(SurveyDbContext surveyDbContext)
        {
            this._surveyDbContext = surveyDbContext;
        }


        [HttpPost]
        public async Task<IActionResult> AddAnswer(Answer answer)
        {
            answer.AnswerId = Guid.NewGuid();
            await _surveyDbContext.Answers.AddAsync(answer);
            await _surveyDbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
