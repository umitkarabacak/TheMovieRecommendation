using System;
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
        public Guid MovieId { get; set; }
        public string title { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public string poster_path { get; set; }
        public bool video { get; set; }
        public float vote_average { get; set; }
        public string overview { get; set; }
        public string release_date { get; set; }
        public int vote_count { get; set; }
        public int id { get; set; }
        public bool adult { get; set; }
        public string backdrop_path { get; set; }
        public float popularity { get; set; }
        public string media_type { get; set; }

        public List<MovieGenre> MovieGenres { get; set; }
            = new List<MovieGenre>();

        public List<MovieVote> MovieVotes { get; set; }
            = new List<MovieVote>();
    }
}
