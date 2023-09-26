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
    [Route("api/rental")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RentalsController : ControllerBase
    {
        private readonly IRentalMovieService rentalMovieService;

        public RentalsController(IRentalMovieService rentalMovieService)
        {

            this.rentalMovieService = rentalMovieService;
        }

        [HttpPost]
        public async Task<ActionResult<MovieDTO>> RentalMovie(RentalCreacionDTO rentalCreacionDTO)
        {
            var respuestaRentals = await rentalMovieService.RentalMovie(rentalCreacionDTO);

            if (respuestaRentals == null)
            {
                return NotFound("No se encontro la pelicula");
            }
            return Ok(respuestaRentals);
        }

        [HttpPut]
        public async Task<ActionResult<MovieDTO>> Put(RentalCreacionDTO rentalCreacionDTO)
        {
            var returnDate = await rentalMovieService.ReturnDate(rentalCreacionDTO);

            if(returnDate == null)
            {
                return NotFound("No se puede actualizar la pelicula");
            }
            else
            {
                return Ok($"¡Actualizado con exito!");
            }
        }

    }
}
