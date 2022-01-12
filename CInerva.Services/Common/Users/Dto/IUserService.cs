using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinerva.Services.Common.Users.Dto
{
    public interface IUserService
    {
        public List<UserDto> GetAdmins();
    }
}
