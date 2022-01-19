using System.Collections.Generic;

namespace Cinerva.Services.Common.Users.Dto
{
    public interface IUserService
    {
        public List<UserDto> GetAdmins();
    }
}
