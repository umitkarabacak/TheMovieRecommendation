using System.Collections.Generic;

namespace Domain.Entities
{
    /// <summary>
    /// The Movie Genre object
    /// </summary>
    public class Genre
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<MovieGenre> MovieGenres { get; set; }
           = new List<MovieGenre>();
    }
}
