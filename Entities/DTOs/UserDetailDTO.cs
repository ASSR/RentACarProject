using Core.Entities;

namespace Entities.DTOs
{
    public class UserDetailDTO : IDTO
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}