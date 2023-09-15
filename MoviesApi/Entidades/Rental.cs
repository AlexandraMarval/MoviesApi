namespace MoviesApi.Entidades
{
    public class Rental
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public DateTime RentaDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int RentalRates { get; set; }

    }
}
