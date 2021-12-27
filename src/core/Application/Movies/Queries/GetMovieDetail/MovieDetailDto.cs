using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Application.Movies.Queries.GetMovieDetail
{
    public class MovieDetailDto : IMapFrom<Movie>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public string Original_language { get; set; }
        public string Original_title { get; set; }
        public string Release_date { get; set; }
        public float Vote_average { get; set; }
        public int Vote_count { get; set; }
        public float Popularity { get; set; }

        public string Genres { get; set; }
        public List<MovieVoteDetailDto> LocalVotes { get; set; }
            = new List<MovieVoteDetailDto>();

        public void Mapping(Profile profile)
        {
            profile
                .CreateMap<Movie, MovieDetailDto>()
                .ForMember(m => m.Genres, opts =>
                {
                    opts.MapFrom(src => src.MovieGenres.Any()
                        ? string.Join(", ", src.MovieGenres.Select(mg => mg.Genre.Name).OrderBy(gn => gn).ToList())
                        : null);
                })
                .ForMember(m => m.LocalVotes, opts =>
                {
                    opts.MapFrom(src => src.MovieVotes.Any()
                        ? src.MovieVotes.Select(mv => new MovieVoteDetailDto
                        {
                            Username = mv.User.Username ?? string.Empty,
                            Vote = mv.Vote,
                            VoteNote = mv.VoteNote ?? default,
                        })
                        : null);
                });
            ;
        }
    }
}
