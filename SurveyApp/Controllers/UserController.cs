using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurveyApp.Data;
using SurveyApp.Models.Entities;

namespace SurveyApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class UserController : ControllerBase
    {
        private readonly SurveyDbContext _surveyDbContext;

        public UserController(SurveyDbContext surveyDbContext)
        {
            this._surveyDbContext = surveyDbContext;
        }
        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            //var loginUser = _surveyDbContext.Users.Where(x => x.username == user.username && x.password == user.password).FirstOrDefault();
            //if (loginUser == null)
            //{
            //    return NotFound();
            //}
            //if (loginUser.username == "admin" && loginUser.password == "admin")
            //{
            //    return Ok("AdminGeldi");
            //}
            //return Ok(loginUser);

            user.UserId = Guid.NewGuid();
            await _surveyDbContext.Users.AddAsync(user);
            await _surveyDbContext.SaveChangesAsync();
            return Ok(user);
        }

        //[HttpPost]
        //public async Task<IActionResult> RegisterUser(User user)
        //{
        //    user.UserId = Guid.NewGuid();
        //    await _surveyDbContext.Users.AddAsync(user);
        //    await _surveyDbContext.SaveChangesAsync();
        //    return Ok();
        //}
        //[HttpGet]
        //[Route("{id:Guid}")]
        //[ActionName("GetUserById")]
        //public async Task<IActionResult> GetUserById([FromRoute] Guid id)
        //{
        //    //Get all notes from database
        //    //await notesDbContext.Notes.FirstOrDefaultAsync(x => x.Id == id);
        //    var user = _surveyDbContext.Users.Find(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(user);
        //}


    }
}
