namespace Domain.Entities
{
    public class MovieGenre
    {
        public long MovieId { get; set; }

        public int GenreId { get; set; }

        public Movie Movie { get; set; }
        public Genre Genre { get; set; }
    }
}
