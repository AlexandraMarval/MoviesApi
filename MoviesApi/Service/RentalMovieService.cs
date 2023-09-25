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

        public RentalMovieService(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;           
        }

        public async Task<MovieDTO> RentalMovie(RentalCreacionDTO rentalCreacionDTO)
        {
            var movie = await context.Movies.FindAsync(rentalCreacionDTO.MovieId);

            if (movie != null)
            {            
                if (movie.Available == true)
                {
                    movie.Available = false;
                    var rental = new Rental()
                    {
                        MovieId = rentalCreacionDTO.MovieId,
                        RentaDate = DateTime.Now,
                    };
                    context.Add(rental);
                }
                else
                {
                    throw new InvalidOperationException("La pelicula no se encuentra disponible");
                }           
            }            
            var movieDTO = mapper.Map<MovieDTO>(movie); 
            await context.SaveChangesAsync();
            return movieDTO;
        }

        public async Task<MovieDTO> ReturnDate(RentalCreacionDTO rentalCreacionDTO)
        {
            var movie = await context.Movies.FindAsync(rentalCreacionDTO.MovieId);

            if (rentalCreacionDTO != null)
            {
                movie.Available = true;
                var rental = new Rental()
                {
                    MovieId = rentalCreacionDTO.MovieId,                    
                    ReturnDate = DateTime.Now
                };
                context.Add(rental);
            } 
            var movieDTO = mapper.Map<MovieDTO>(movie);

            await context.SaveChangesAsync();
            return movieDTO;
        }
    }
}