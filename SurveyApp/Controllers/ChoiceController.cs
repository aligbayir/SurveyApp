using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurveyApp.Data;
using SurveyApp.Models.Entities;

namespace SurveyApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class ChoiceController : ControllerBase
    {
        private readonly SurveyDbContext _surveyDbContext;

        public ChoiceController(SurveyDbContext surveyDbContext)
        {
            this._surveyDbContext = surveyDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllChoices()
        {
            //Get all notes from database
            return Ok(await _surveyDbContext.Choices.ToListAsync());
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [ActionName("GetChoiceById")]
        public async Task<IActionResult> GetChoiceById([FromRoute] Guid id)
        {
            //Get all notes from database
            //await notesDbContext.Notes.FirstOrDefaultAsync(x => x.Id == id);
            var choice = await _surveyDbContext.Choices.FindAsync(id);
            if (choice == null)
            {
                return NotFound();
            }
            return Ok(choice);
        }
        [HttpGet]
        [Route("/GetChoiceBySurveyId/{id:Guid}")]
        public async Task<IActionResult> GetChoiceBySurveyId([FromRoute] Guid id)
        {
            var choice = _surveyDbContext.Choices.Where(x => x.SurveyId == id).ToList();
            if (choice == null)
            {
                return NotFound();
            }
            return Ok(choice);
        }
        [HttpGet]
        [Route("/GetChoiceByQuestionId/{id:Guid}")]
        public async Task<IActionResult> GetChoiceByQuestionId([FromRoute] Guid id)
        {
            var choice = _surveyDbContext.Choices.Where(x => x.QuestionId == id).ToList();
            if (choice == null)
            {
                return NotFound();
            }
            return Ok(choice);
        }
        [HttpPost]
        public async Task<IActionResult> AddChoice(Choice choice)
        {
            choice.ChoiceId = Guid.NewGuid();
            choice.SurveyId = _surveyDbContext.Questions.Where(x => x.QuestionId == choice.QuestionId).Select(x => x.SurveyId).FirstOrDefault();
            await _surveyDbContext.Choices.AddAsync(choice);
            await _surveyDbContext.SaveChangesAsync();

            return Ok(choice);
        }
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateChoice([FromRoute] Guid id, [FromBody] Choice updatedChoice)
        {
            var existingChoice = await _surveyDbContext.Choices.FindAsync(id);
            if (existingChoice == null)
            {
                return NotFound();
            }
            existingChoice.ChoiceName = updatedChoice.ChoiceName;

            await _surveyDbContext.SaveChangesAsync();

            return Ok(existingChoice);
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteChoice([FromRoute] Guid id)
        {
            var existingChoice = await _surveyDbContext.Choices.FindAsync(id);

            if (existingChoice == null)
            {
                return NotFound();
            }
            _surveyDbContext.Choices.Remove(existingChoice);
            await _surveyDbContext.SaveChangesAsync();

            return Ok();
        }

    }
}
