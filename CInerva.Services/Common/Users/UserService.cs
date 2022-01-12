using Cinerva.Data;
using Cinerva.Services.Common.Users.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinerva.Services.Common.Users
{
    public class UserService : IUserService
    {
        public readonly CinervaDbContext dbContext;
        public UserService(CinervaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public UserDto GetUser(int id)
        {
            if (id < 1) throw new ArgumentException(nameof(id));

            var userEntity = dbContext.Users.Find(id);
            if (userEntity == null) return null;

            return new UserDto
            {
                Id = userEntity.Id,
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                RoleId = userEntity.RoleId,
                Email = userEntity.Email,
                Password = userEntity.Password,
                IsBanned = userEntity.IsBanned,
                IsDeleted = userEntity.IsDeleted
            };

        }

        public List<UserDto> GetAdmins()
        {
            return dbContext.Users
                .Where(u => u.Role.Name == "Admin")
                .OrderBy(u => u.FirstName)
                .ThenBy(u => u.LastName)
                .Select(
                x => new UserDto
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    RoleId = x.RoleId,
                    Email = x.Email,
                    Password = x.Password,
                    IsBanned = x.IsBanned,
                    IsDeleted = x.IsDeleted,
                    FullName = String.Join(" ", x.FirstName, x.LastName)
                }
            )
            .ToList();
        }
    }
}
