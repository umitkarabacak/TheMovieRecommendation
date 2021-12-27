using System.Text.Json.Serialization;

namespace Application.Movies.ViewModels
{
    public class GenreDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
