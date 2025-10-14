using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? ProfileImageUrl { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? LastUpdate { get; set; }
        public UserType Role { get; set; }

        public static UserDTO Create(User user)
        {
            var dto = new UserDTO();

            dto.Id = user.Id;
            dto.FirstName = user.FirstName;
            dto.LastName = user.LastName;
            dto.Email = user.Email;
            dto.ProfileImageUrl = user.ProfileImageUrl;
            dto.CreationDate = user.CreationDate;
            dto.LastUpdate = user.LastUpdate;
            dto.Role = user.Role;

            return dto;
        }

        public static List<UserDTO> CreateList(List<User> userList)
        {
            var dtoList = new List<UserDTO>();

            foreach (var user in userList)
            {
                dtoList.Add(Create(user));
            }

            return dtoList;
        }
    }
}
