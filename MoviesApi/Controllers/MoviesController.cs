using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApi.Entidades;

namespace MoviesApi.Controllers
{
    [ApiController]
    [Route("api/controllerMovies")]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public MoviesController(ApplicationDbContext context) 
        {
            this.context = context;
        }

        [HttpGet(Name = "obtenerPeliculas")]
        public async Task<ActionResult<List<Movie>>> Get()
        {
            return await context.Movies.ToListAsync();   
        }

        [HttpPost(Name = "crearPelicula")]
        public async Task<ActionResult> Post(Movie movies)
        {
            context.Add(movies);
            await context.SaveChangesAsync();
            return Ok(movies);
        }      
    }
}
