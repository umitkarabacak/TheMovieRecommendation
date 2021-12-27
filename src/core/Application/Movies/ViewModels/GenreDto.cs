using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using System.Text.Json.Serialization;

namespace Application.Movies.ViewModels
{
    public class GenreDto : IMapFrom<Genre>
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GenreDto, Genre>()
                .ReverseMap();
        }
    }

}
