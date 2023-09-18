namespace MoviesApi.Entidades
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Descriptión { get; set;}
        public DateTime? ReleaseYear { get; set; }
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }
        public virtual List<Rental> Rental { get; set; }
    }
}
