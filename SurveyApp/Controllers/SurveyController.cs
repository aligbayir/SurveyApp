using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurveyApp.Data;
using SurveyApp.Models.Entities;

namespace SurveyApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class SurveyController : ControllerBase
    {
        private readonly SurveyDbContext _surveyDbContext;

        public SurveyController(SurveyDbContext surveyDbContext)
        {
            this._surveyDbContext = surveyDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSurveys()
        {
            //Get all notes from database
            return Ok(await _surveyDbContext.Surveys.ToListAsync());
        }
   

        [HttpGet]
        [Route("{id:Guid}")]
        [ActionName("GetSurveyById")]
        public async Task<IActionResult> GetSurveyById([FromRoute] Guid id)
        {
            //Get all notes from database
            //await notesDbContext.Notes.FirstOrDefaultAsync(x => x.Id == id);
            var survey = await _surveyDbContext.Surveys.FindAsync(id);
            if (survey == null)
            {
                return NotFound();
            }
            return Ok(survey);
        }

        [HttpPost]
        public async Task<IActionResult> AddSurvey(Survey survey)
        {
            survey.SurveyId = Guid.NewGuid();
            await _surveyDbContext.Surveys.AddAsync(survey);
            await _surveyDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSurveyById), new { id = survey.SurveyId }, survey);
        }
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateNote([FromRoute] Guid id, [FromBody] Survey updatedSurvey)
        {
            var existingSurvey = await _surveyDbContext.Surveys.FindAsync(id);
            if (existingSurvey == null)
            {
                return NotFound();
            }
            existingSurvey.SurveyDescription = updatedSurvey.SurveyDescription;

            await _surveyDbContext.SaveChangesAsync();

            return Ok(existingSurvey);
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteSurvey([FromRoute] Guid id)
        {
            var existingSurvey = await _surveyDbContext.Surveys.FindAsync(id);

            if (existingSurvey == null)
            {
                return NotFound();
            }
            _surveyDbContext.Surveys.Remove(existingSurvey);
            await _surveyDbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
