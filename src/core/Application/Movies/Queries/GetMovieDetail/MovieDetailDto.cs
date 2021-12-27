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

        public string Genres { get; set; }

        public List<MovieVoteDetailDto> Votes { get; set; }
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
                .ForMember(m => m.Votes, opts =>
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
