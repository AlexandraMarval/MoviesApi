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

        [HttpGet("int:id")]
        public async Task<ActionResult<List<Movie>>> Get(int id)
        {
            var movie = await context.Movies.FirstOrDefaultAsync(movie => movie.Id == id);
            return Ok(movie);
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


        [HttpPut("int:id")]
        public async Task<ActionResult> Put(int id)
        {
            var existeMovie = context.Movies.FirstOrDefault(movie => movie.Id == id);

            if (existeMovie == null)
            {
                return BadRequest();
            }
            context.Update(existeMovie);
            await context.SaveChangesAsync();
            return Ok(existeMovie);
        }
    }
}
