using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities.Concrete
{
    public class User : IEntity
    {
        public int UserId { get; set; }

        [Required]
        [StringLength(120, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(120, MinimumLength = 2)]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public byte[] PasswordSalt { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public byte[] PasswordHash { get; set; }

        public bool Status { get; set; }

        public List<UserOperationClaim> UserOperationClaims { get; set; }
    }
}