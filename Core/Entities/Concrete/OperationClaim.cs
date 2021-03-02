using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities.Concrete
{
    public class OperationClaim : IEntity
    {
        public int OperationClaimId { get; set; }

        [Required]
        [StringLength(120, MinimumLength = 2)]
        public string Name { get; set; }

        public List<UserOperationClaim> UserOperationClaims { get; set; }
    }
}