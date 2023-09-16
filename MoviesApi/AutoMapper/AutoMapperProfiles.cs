using AutoMapper;
using MoviesApi.DTOs;
using MoviesApi.Entidades;

namespace MoviesApi.AutoMapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<MovieCreacionDTO, Movie>();
            CreateMap<CategoryCreacionDTO, Category>();
            CreateMap<Movie, MovieDTO>();
        }
    }
}
