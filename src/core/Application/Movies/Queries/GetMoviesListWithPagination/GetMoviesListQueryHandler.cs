using Application.Interfaces;
using Application.Models;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Movies.Queries.GetMoviesListWithPagination
{
    public class GetMoviesListQueryHandler : IRequestHandler<GetMoviesListQuery, PaginatedItemsViewModel<MovieListItemDto>>
    {
        private readonly IProjectContext _projectContext;
        private readonly IMapper _mapper;

        public GetMoviesListQueryHandler(IProjectContext projectContext
            , IMapper mapper)
        {
            _projectContext = projectContext;
            _mapper = mapper;
        }

        public async Task<PaginatedItemsViewModel<MovieListItemDto>> Handle(GetMoviesListQuery request, CancellationToken cancellationToken)
        {
            var totalItems = await _projectContext.Movies
                .LongCountAsync();

            if (totalItems <= 0)
                return new PaginatedItemsViewModel<MovieListItemDto>(request.PageIndex, request.PageSize, totalItems, null);

            var movies = await _projectContext.Movies
                .OrderBy(m => m.Id)
                .Skip(request.PageSize * request.PageIndex)
                .Take(request.PageSize)
                .ToListAsync();

            var movieListItems = _mapper.Map<List<MovieListItemDto>>(movies);

            return new PaginatedItemsViewModel<MovieListItemDto>(request.PageIndex, request.PageSize, totalItems, movieListItems);
        }
    }
}
