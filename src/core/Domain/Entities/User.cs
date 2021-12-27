using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    /// <summary>
    /// The User object
    /// </summary>
    public class User
    {
        public Guid Id { get; set; }
        /// <summary>
        /// Username
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// User password
        /// </summary> 
        public string Password { get; set; }


        public List<MovieVote> MovieVotes { get; set; }
            = new List<MovieVote>();
    }
}
