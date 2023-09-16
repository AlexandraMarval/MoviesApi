using System.ComponentModel.DataAnnotations;

namespace MoviesApi.DTOs
{
    public class MovieCreacionDTO
    {    
        public string Name { get; set; }
        public string Descriptión { get; set; }
        public DateTime? ReleaseYear { get; set; }
        public int CategoryId { get; set; }
    }
}
