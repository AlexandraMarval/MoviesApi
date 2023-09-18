using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApi.DTOs;
using MoviesApi.Entidades;
using MoviesApi.Service;

namespace MoviesApi.Controllers
{
    [ApiController]
    [Route("api/rental")]
    public class RentalsController : ControllerBase
    {     
        private readonly IRentalMovieService rentalMovieService;

        public RentalsController(IRentalMovieService rentalMovieService)
        {          
          
            this.rentalMovieService = rentalMovieService;
        } 

        [HttpPost]
        public async Task<ActionResult<MovieDTO>> Post(RentalCreacionDTO rentalCreacionDTO)
        {
           var rental = await rentalMovieService.RentalMovie(rentalCreacionDTO);

            if (rental == null)
            {
                return NotFound("No se encontro la pelicula");
            }

           return Ok(rental);
        }
    }
}
