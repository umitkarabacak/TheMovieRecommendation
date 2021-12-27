using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Movies.Commands.CreateMovieVote
{
    public class CreateMovieVoteCommandHandler : IRequestHandler<CreateMovieVoteCommand, int>
    {
        private readonly IProjectContext _projectContext;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public CreateMovieVoteCommandHandler(IProjectContext projectContext
            , IMapper mapper
            , ICurrentUserService currentUserService)
        {
            _projectContext = projectContext;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<int> Handle(CreateMovieVoteCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = Guid.Parse(_currentUserService.UserId);

            var movieVote = await _projectContext.MovieVotes
                .FirstOrDefaultAsync(mv => mv.MovieId.Equals(request.MovieId)
                                        && mv.UserId.Equals(currentUserId)
                , cancellationToken);

            if (movieVote is not null)
            {
                movieVote = _mapper.Map(request, movieVote);
                _projectContext.MovieVotes.Update(movieVote);
                await _projectContext.SaveChangesAsync(cancellationToken);

                return movieVote.MovieId;
            }

            movieVote = _mapper.Map(request, movieVote);
            movieVote.UserId = currentUserId;

            await _projectContext.MovieVotes.AddAsync(movieVote, cancellationToken);
            await _projectContext.SaveChangesAsync(cancellationToken);

            return movieVote.MovieId;
        }
    }
}
