using ShopHub.Models.Dtos;
using ShopHub.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopHub.Services.Interface
{
    public interface IUserService
    {
        public Task<User> RegisterUser(UserAuthDto user);
        public Task<UserAuthDto> AuthUser(UserAuthDto user);
    }
}
