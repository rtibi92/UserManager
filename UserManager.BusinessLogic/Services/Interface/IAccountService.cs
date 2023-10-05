using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UserManager.BusinessLogic.DTO;

namespace UserManager.BusinessLogic.Services.Interface
{
    public interface IAccountService
    {
        Task<UserDto> CreateUser(UserDto  user);

        Task<HttpStatusCode> DeleteUser(string userName);

        Task<UserDto> UpdateUser(UserDto dto);

        Task<IEnumerable<UserDto>> GetAllUsers();
    }
}
