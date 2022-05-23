using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notes.Domain.Entity;
using Notes.Persistence;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Notes.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        string name = "Сашка";

        [HttpGet()]
        public JsonResult Get()
        {            
            using (NotesContext db = new())
            {
                Note user1 = new Note
                {
                    Guid = Guid.NewGuid(),
                    NoteId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Title = "Даб даб даб"
                };

                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                db.Add(user1);
                db.SaveChanges();
                return new JsonResult( user1);
            }
        }
    }
}


