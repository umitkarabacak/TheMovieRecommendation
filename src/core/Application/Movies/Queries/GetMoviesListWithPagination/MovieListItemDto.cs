using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Movies.Queries.GetMoviesListWithPagination
{
    public class MovieListItemDto : IMapFrom<Movie>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Movie, MovieListItemDto>();
        }
    }
}
