using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Application.Movies.ViewModels
{
    public class GenreResponseDto
    {
        [JsonPropertyName("genres")]
        public List<GenreDto> Genres { get; set; }
    }
}
