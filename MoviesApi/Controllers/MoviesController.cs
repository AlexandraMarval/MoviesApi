using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApi.Entidades;

namespace MoviesApi.Controllers
{
    [ApiController]
    [Route("api/controller")]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public MoviesController(ApplicationDbContext context) 
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var movies = await context.Movies.ToListAsync();
            return Ok(movies);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Movies movies)
        {
            context.Add(movies);
            await context.SaveChangesAsync();
            return Ok(movies);
        }
    }
}
