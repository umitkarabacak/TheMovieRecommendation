using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using System.Linq;

namespace Application.Movies.ViewModels
{
    public class MovieDto : IMapFrom<Movie>
    {
        public int[] genre_ids { get; set; }
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
        public string first_air_date { get; set; }
        public string name { get; set; }
        public string original_name { get; set; }
        public string[] origin_country { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<MovieDto, Movie>()
                .ForMember(src => src.MovieGenres, opts =>
                {
                    opts.MapFrom(srcx => srcx.genre_ids.Length > 0
                        ? srcx.genre_ids.ToList().Select(id => new MovieGenre { GenreId = id })
                        : null);
                });
        }
    }
}
