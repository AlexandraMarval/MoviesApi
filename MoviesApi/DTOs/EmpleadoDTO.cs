using System.ComponentModel.DataAnnotations;

namespace MoviesApi.DTOs
{
    public class EmpleadoDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
