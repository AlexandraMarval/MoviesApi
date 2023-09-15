namespace MoviesApi.Entidades
{
    public class Movies
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Descripción { get; set;}
        public int AñoDeLanzamiento { get; set; }
        public int IdCategoria { get; set; }
    }
}
