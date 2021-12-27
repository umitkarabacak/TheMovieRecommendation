using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using System;

namespace Application.Movies.Queries.GetMoviesListWithPagination
{
    public class MovieListItemDto : IMapFrom<Movie>
    {
        public Guid MovieId { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public string release_date { get; set; }
        public float vote_average { get; set; }
        public int vote_count { get; set; }
        public float popularity { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Movie, MovieListItemDto>();
        }
    }
}
