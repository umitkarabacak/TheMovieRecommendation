using System.Collections.Generic;

namespace Domain.Entities
{
    /// <summary>
    /// The movie object
    /// </summary>
    public class Movie
    {
        /// <summary>
        /// Movie unique number
        /// </summary>
        public int Id { get; set; }

        public string Title { get; set; }

        public List<MovieGenre> MovieGenres { get; set; }
            = new List<MovieGenre>();

        public List<MovieVote> MovieVotes { get; set; }
            = new List<MovieVote>();
    }
}
