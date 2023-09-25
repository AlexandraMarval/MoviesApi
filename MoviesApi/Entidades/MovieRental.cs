namespace MoviesApi.Entidades
{
    public class MovieRental
    {
        public int MovieId { get; set; }
        public int RentalId { get; set; }
        public virtual Movie Movie { get; set; }
        public virtual Rental Rental { get; set;}

    }
}
