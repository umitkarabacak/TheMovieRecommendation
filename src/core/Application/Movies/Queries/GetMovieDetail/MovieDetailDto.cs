using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using System.Linq;

namespace Application.Movies.Queries.GetMovieDetail
{
    public class MovieDetailDto : IMapFrom<Movie>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Genres { get; set; }

        public void Mapping(Profile profile)
        {
            profile
                .CreateMap<Movie, MovieDetailDto>()
                .ForMember(m => m.Genres, opts =>
                {
                    opts.MapFrom(src => src.MovieGenres.Any()
                        ? string.Join(", ", src.MovieGenres.Select(mg => mg.Genre.Name).OrderBy(gn => gn).ToList())
                        : null);
                });
            ;
        }
    }
}
