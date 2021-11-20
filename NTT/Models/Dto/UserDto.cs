using NTT.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTT.API.Models.Dto
{
    public class UserDto
    {

        public int Id { get; set; }
        public string Name { get; set; }
        
        public UserDto FromDomain(User user)
        {
            return new UserDto
            {

                Id = user.Id,
                Name = user.Name
            };

        }
        public User ToDomain()
        {
            return new User
            {

                Id = Id,
                Name = Name
            };

        }

    }

  
}

