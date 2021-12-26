using System;

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
        // TODO password hash!
        public string Password { get; set; }
    }
}
