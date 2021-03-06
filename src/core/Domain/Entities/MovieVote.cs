using Domain.Common;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class MovieVote : AuditableEntity
    {
        public Guid MovieId { get; set; }

        public Guid UserId { get; set; }

        public float Vote { get; set; }

        public string VoteNote { get; set; }


        public Movie Movie { get; set; }
        public User User { get; set; }
    }
}
