using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using UserManager.DataAccess.Model;
using UserManager.BusinessLogic.Services.Implementation;
using UserManager.BusinessLogic.DTO;
using System.Net;

namespace UserManager.API.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly AccountService _accountService;

        public UserController(
            AccountService accountService,
            ILogger<UserController> logger)
        {
            _accountService = accountService;
            _logger = logger;
        }



        [Microsoft.AspNetCore.Mvc.HttpGet(Name = "GetAll")]
        public async Task<IEnumerable<UserDto>> GetAll()
        {
            return await _accountService.GetAllUsers();
        }

        [Microsoft.AspNetCore.Mvc.HttpPost(Name = "CreateUser")]
        public async Task<IActionResult> CreateUser(UserDto dto)
        {
            await _accountService.CreateUser(dto);

            return Ok();
        }

        [Microsoft.AspNetCore.Mvc.HttpDelete(Name = "DeleteUser")]
        public async Task<IActionResult> Delete(string userName)
        {
            await _accountService.DeleteUser(userName); 

            return Ok();
        }

        [Microsoft.AspNetCore.Mvc.HttpPost(Name = "EditUser")]
        public async Task<IActionResult> Edit(UserDto user)
        {
            UserDto dto = await _accountService.UpdateUser(user);

            if (dto == null)
            {
                NotFound();
            }

            return Ok();
        }
    }
}