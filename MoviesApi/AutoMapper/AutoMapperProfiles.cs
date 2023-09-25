using AutoMapper;
using MoviesApi.DTOs;
using MoviesApi.Entidades;

namespace MoviesApi.AutoMapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<MovieCreacionDTO, Movie>()
                .AfterMap((dto, movie) =>
                {
                    movie.Available = true;
                });
            CreateMap<CategoryCreacionDTO, Category>();
            CreateMap<MovieCreacionDTO, MovieDTO>();
            CreateMap<RentalCreacionDTO, Rental>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<Movie, MovieDTO>();         
        }
    }
}
