using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Movies.Queries.GetMoviesListWithPagination
{
    // Todo bind properties
    public class MovieListItemDto : IMapFrom<Movie>
    {
        public int Id { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Movie, MovieListItemDto>();
        }
    }
}
