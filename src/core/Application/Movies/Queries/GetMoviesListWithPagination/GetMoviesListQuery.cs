using Application.Models;
using MediatR;

namespace Application.Movies.Queries.GetMoviesListWithPagination
{
    public class GetMoviesListQuery : IRequest<PaginatedItemsViewModel<MovieListItemDto>>
    {
        public int PageIndex { get; init; }

        public int PageSize { get; init; }

        public GetMoviesListQuery(int pageSize, int pageIndex)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
    }
}
