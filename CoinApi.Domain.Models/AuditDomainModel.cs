using System;

namespace CoinApi.Domain.Models
{
    public abstract class AuditDomainModel
    {
        public DateTime Created { get; set; }

        public DateTime? LastModified { get; set; }
    }
}