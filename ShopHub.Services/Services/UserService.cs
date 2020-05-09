using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopHub.Models.Context;
using ShopHub.Models.Dtos;
using ShopHub.Models.Models;
using ShopHub.Services.Interface;
using ShopHub.Services.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopHub.Services.Services
{
    public class UserService : IUserService
    {
        private ShopHubContext _context;
        private IMapper _mapper;
        public UserService(ShopHubContext shopHubContext, IMapper mapper)
        {
            _context = shopHubContext;
            _mapper = mapper;
        }

        public async Task<User> RegisterUser(UserAuthDto user)
        {
            user.UserTypeId = Convert.ToInt32(UserTypeNames.Customer);
            var mappedData = _mapper.Map<User>(user);
            _context.Users.Add(mappedData);
            await _context.SaveChangesAsync();

            return mappedData;
        }

        public async Task<UserAuthDto> AuthUser(UserAuthDto user)
        {
            var record = await _context.Users.FirstOrDefaultAsync(x => (x.FirstName.ToLower().Equals(user.FirstName)) 
                           && x.LastName.ToLower().Equals(user.LastName) && x.Password.ToLower().Equals(user.Password));

            if (!(record is null))
            {
                var mappedData = _mapper.Map<UserAuthDto>(record);
                mappedData.IsSuccessFullLogin = true;
                return mappedData;
            }
            else
            {
                var mappedData = _mapper.Map<UserAuthDto>(user);
                mappedData.IsSuccessFullLogin = false;
                return mappedData;
            }
        }

    }
}
