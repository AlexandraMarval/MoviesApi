using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApi.DTOs;
using MoviesApi.Entidades;

namespace MoviesApi.Controllers
{
    [ApiController]
    [Route("api/controllerMovies")]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public MoviesController(ApplicationDbContext context, IMapper mapper) 
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("int:id")]
        public async Task<ActionResult<MovieDTO>> Get(int id)
        {
            var movie = await context.Movies.FirstOrDefaultAsync(movie => movie.Id == id);

            if (movie == null)
            {
                return NotFound();
            }
            return mapper.Map<MovieDTO>(movie);
            
        }

        [HttpGet(Name = "obtenerPeliculas")]
        public async Task<ActionResult<List<MovieDTO>>> Get()
        {
            var movies = await context.Movies.ToListAsync();
            return mapper.Map<List<MovieDTO>>(movies);
        }

        [HttpPost(Name = "crearPelicula")]
        public async Task<ActionResult> Post(MovieCreacionDTO movieCreacionDTO)
        {
            var existePeliculaConElMismoNombre = await context.Movies.AnyAsync(movie => movie.Name == movieCreacionDTO.Name);

            if(existePeliculaConElMismoNombre) 
            {
                return BadRequest($"Ya existe una pelicula con ese mismo nombre{movieCreacionDTO.Name}");
            }
            var movie =  mapper.Map<Movie>(movieCreacionDTO);

            context.Add(movie);
            await context.SaveChangesAsync();
            return Ok();
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
