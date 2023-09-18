using Microsoft.AspNetCore.Mvc;
using MoviesApi.DTOs;

namespace MoviesApi.Service
{
    public interface IRentalMovieService
    {
        public Task<MovieDTO> RentalMovie(RentalCreacionDTO rentalCreacionDTO);
      
    }
}