using System;
using BLL.Entities.Identity;

namespace BLL.Interface
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
