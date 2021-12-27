using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;

namespace Application.Movies.Commands.CreateMovieVote
{
    public class CreateMovieVoteCommand : IRequest<Guid>, IMapFrom<MovieVote>
    {
        public Guid MovieId { get; set; }
        public float Vote { get; set; }
        public string VoteNote { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<MovieVote, CreateMovieVoteCommand>()
                .ReverseMap();
        }
    }
}
