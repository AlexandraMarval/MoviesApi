using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApi.DTOs;
using MoviesApi.Entidades;

namespace MoviesApi.Service
{
    public class RentalMovieService : IRentalMovieService
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly ILogger<RentalMovieService> logger;

        public RentalMovieService(ApplicationDbContext context, IMapper mapper, ILogger<RentalMovieService> logger)
        {
            this.context = context;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<MovieDTO> RentalMovie(RentalCreacionDTO rentalCreacionDTO)
        {
            var movie = await context.Movies.FindAsync(rentalCreacionDTO.MovieId);

            if (movie == null)
            {
                throw new Exception("Pelicula no encontrada");
            }
            var rental = new Rental()
            {
                MovieId = rentalCreacionDTO.MovieId
            };

            context.Add(rental);
            await context.SaveChangesAsync();
            
            var movieDTO = mapper.Map<MovieDTO>(rental);

            return movieDTO;

        }

    }
}
