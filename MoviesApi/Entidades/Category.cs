namespace MoviesApi.Entidades
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Movie> Movie { get; set; }
    }
}
