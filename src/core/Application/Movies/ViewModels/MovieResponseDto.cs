using System.Collections.Generic;

namespace Application.Movies.ViewModels
{
    public class MovieResponseDto
    {
        public int page { get; set; }
        public List<MovieDto> results { get; set; }
            = new List<MovieDto>();
        public int total_pages { get; set; }
        public int total_results { get; set; }
    }
}
