using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurveyApp.Data;
using SurveyApp.Models.Entities;

namespace SurveyApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class QuestionController : ControllerBase
    {
        private readonly SurveyDbContext _surveyDbContext;

        public QuestionController(SurveyDbContext surveyDbContext)
        {
            this._surveyDbContext = surveyDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllQuestions()
        {
            //Get all notes from database
            return Ok(await _surveyDbContext.Questions.ToListAsync());
        }

        [HttpGet]
        [Route("/GetBySurveyId/{id:Guid}")]
        public async Task<IActionResult> GetQuestionBySurveyId([FromRoute] Guid id)
        {
            var questions = await _surveyDbContext.Questions.Where(x => x.SurveyId == id).ToListAsync();
            return Ok(questions);
        }


        [HttpGet]
        [Route("{id:Guid}")]
        [ActionName("GetQuestionById")]
        public async Task<IActionResult> GetQuestionById([FromRoute] Guid id)
        {
            //Get all notes from database
            //await _surveyDbContext.Questions.FirstOrDefaultAsync(x => x.QuestionId == id);
            var question = await _surveyDbContext.Questions.FindAsync(id);
            if (question == null)
            {
                return NotFound();
            }
            return Ok(question);
        }

        [HttpPost]
        public async Task<IActionResult> AddQuestion(Question question)
        {
            question.QuestionId = Guid.NewGuid();
            await _surveyDbContext.Questions.AddAsync(question);
            await _surveyDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetQuestionById), new { id = question.QuestionId }, question);
        }
        [HttpPut]
        [Route("/{id:Guid}")]
        public async Task<IActionResult> UpdateNote([FromRoute] Guid id, [FromBody] Question updatedQuestion)
        {
            var existingQuestion = await _surveyDbContext.Questions.FindAsync(id);
            if (existingQuestion == null)
            {
                return NotFound();
            }
            existingQuestion.QuestionName = updatedQuestion.QuestionName;
            existingQuestion.AnswerType = updatedQuestion.AnswerType;

            await _surveyDbContext.SaveChangesAsync();

            return Ok(existingQuestion);
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteSurvey([FromRoute] Guid id)
        {
            var existingQuestion = await _surveyDbContext.Questions.FindAsync(id);

            if (existingQuestion == null)
            {
                return NotFound();
            }
            _surveyDbContext.Questions.Remove(existingQuestion);
            await _surveyDbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
