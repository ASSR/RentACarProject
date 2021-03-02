﻿namespace Core.Entities.Concrete
{
    public class UserOperationClaim : IEntity
    {
        public int UserOperationClaimId { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int OperationClaimId { get; set; }
        public OperationClaim OperationClaim { get; set; }
    }
}