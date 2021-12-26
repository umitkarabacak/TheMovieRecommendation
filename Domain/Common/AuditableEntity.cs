using System;

namespace Domain.Common
{
    public class AuditableEntity
    {
        public DateTime Created { get; set; }

        public string CreatedBy { get; set; } = null;

        public DateTime? LastModified { get; set; } = null;

        public string LastModifiedBy { get; set; } = null;
    }
}
