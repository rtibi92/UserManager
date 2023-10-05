using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UserManager.BusinessLogic.Constants;
using UserManager.BusinessLogic.DTO;
using UserManager.BusinessLogic.Services.Interface;
using UserManager.DataAccess.Model;

namespace UserManager.BusinessLogic.Services.Implementation
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public AccountService(
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<UserDto> CreateUser(UserDto user)
        {
            if (!await IsUserExists(user))
            {
                var newUser = new ApplicationUser
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };

                //Random jelszót kéne generálni
                await _userManager.CreateAsync(newUser, "Password123!");

                if (newUser.LastName.Contains(Roles.ADMIN))
                {
                    await _userManager.AddToRoleAsync(newUser, Roles.ADMIN);
                }
                else if (newUser.LastName.Contains(Roles.MANAGER))
                {
                    await _userManager.AddToRoleAsync(newUser, Roles.MANAGER);
                }
                else
                {
                    await _userManager.AddToRoleAsync(newUser, Roles.EMPLOYEE);
                }
            }

            return user;
        }

        private async Task<bool> IsUserExists(UserDto user)
        {
            var existingUserByEmail = await _userManager.FindByEmailAsync(user.Email);
            var existingUserByUserName = await _userManager.FindByNameAsync(user.UserName);

            if (existingUserByEmail != null || existingUserByUserName != null)
            {
                return true;
            }
            return false;
        }

        public async Task<HttpStatusCode> DeleteUser(string userName)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(userName);

            if (user != null)
            {
               await _userManager.DeleteAsync(user);
            }

            return HttpStatusCode.OK;
        }

        public async Task<UserDto> UpdateUser(UserDto dto)
        {
            ApplicationUser existing = await _userManager.FindByNameAsync(dto.UserName);
            if (existing != null)
            {

                _mapper.Map(dto, existing);
                await _userManager.UpdateAsync(existing);
            }
            else 
            {
                return null;
            }

            return dto;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            var list = _userManager.Users.AsEnumerable();

            List<UserDto> users = _mapper.Map<List<UserDto>>(list);

            return users;
        }

        public async Task<UserDto> GetUserByUserName(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            UserDto dto = _mapper.Map<UserDto>(user);

           /* UserDto dto = new UserDto()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName
            };*/

            return dto;
        }
    }
}
