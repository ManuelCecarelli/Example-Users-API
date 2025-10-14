using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public string? ProfileImageUrl { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public DateTime? LastUpdate { get; set; }
        public Status Status { get; set; } = Status.Active;
        public UserType Role { get; set; } = UserType.Client;
    }
}
