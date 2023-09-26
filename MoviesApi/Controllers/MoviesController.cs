using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApi.DTOs;
using MoviesApi.Entidades;
using MoviesApi.Service;

namespace MoviesApi.Controllers
{
    [ApiController]
    [Route("api/controllerMovies")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public MoviesController(ApplicationDbContext context, IMapper mapper) 
        {
            this.context = context;
            this.mapper = mapper;
        }
     
        [HttpGet(Name = "obtenerPeliculas")]
        public async Task<ActionResult<List<MovieDTO>>> GetMovieList()
        {
            var movies = await context.Movies.ToListAsync();
            return mapper.Map<List<MovieDTO>>(movies);
        }

        [HttpPost(Name = "crearPelicula")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsEmpleado")]
        public async Task<ActionResult> CreateMovie(MovieCreacionDTO movieCreacionDTO)
        {
            var existePeliculaConElMismoNombre = await context.Movies.AnyAsync(movie => movie.Name == movieCreacionDTO.Name);

            if(existePeliculaConElMismoNombre) 
            {
                return BadRequest($"Ya existe una pelicula con ese mismo nombre{movieCreacionDTO.Name}");
            }
            var movie = mapper.Map<Movie>(movieCreacionDTO);

            context.Add(movie);
            await context.SaveChangesAsync();

            var movieDTO = mapper.Map<MovieDTO>(movie);

            return Ok(movieDTO);
        }

        [HttpPut("{id:int}", Name = "ActualizarMovie")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsEmpleado")]
        public async Task<ActionResult> UpdateMovie(MovieCreacionDTO movieCreacionDTO, int id)
        {
            var existetheMovie = await context.Movies.AnyAsync(movie => movie.Id ==id);

            if(!existetheMovie) 
            {
                return NotFound("No se puede actualizar");
            }
            var movie = mapper.Map<Movie>(movieCreacionDTO);
            movie.Id = id;

            context.Update(movie);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsEmpleado")]
        public async Task<ActionResult> DeleteMovie(int id)
        {
            var deleteMovie = await context.Movies.AnyAsync(movie => movie.Id == id);
            if (!deleteMovie)
            {
                return NotFound("No se puede eleminar la movie");
            }
            context.Remove(new Movie() { Id = id});
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
