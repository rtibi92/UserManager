using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserManager.BusinessLogic.Constants;
using UserManager.BusinessLogic.DTO;
using UserManager.BusinessLogic.Services.Implementation;
using UserManager.DataAccess.Model;
using UserManager.Models;

namespace UserManager.Controllers
{
    
    public class AccountController : Controller
    {
         private SignInManager<ApplicationUser> _singInManager;
          private UserManager<ApplicationUser> _userManager;
        private readonly AccountService _accountService;

        public AccountController(SignInManager<ApplicationUser> singInManager,
            UserManager<ApplicationUser> userManager,
            AccountService accountService
            )
        {
            _accountService = accountService;
            _singInManager = singInManager;
           _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = "/")
        {
           return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm]LoginModel model, string returnUrl = "/")
        {
            var result = await _singInManager.PasswordSignInAsync(model.UserName, model.Password, true, true);

            if (result.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                ModelState.AddModelError("", "Rossz felhasználónév vagy jelszó!");
            }

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _singInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize(Roles = Roles.ADMIN)]
        public IActionResult AddUser()
        {   
            return View();
        }

        [HttpPost]
        [Authorize(Roles = Roles.ADMIN)]
        public async Task<IActionResult> AddUser([FromForm] UserDto model)
        {
            if (ModelState.IsValid) {
                var result = await _accountService.CreateUser(model);

                if (result != null)
                {
                    ViewData["result"] = "User sikeresen létrehozva!";
                    return LocalRedirect("/");
                }
                else
                {
                    ModelState.AddModelError("", "Már létezik user ezzel a névvel vagy e-mail cmmel!");
                }
            }
            
            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> List()
        {
            var result = await _accountService.GetAllUsers();
            UserListDto list = new UserListDto();
            list.Users = result.ToList();
            return View(list);
        }

        [HttpGet]
        [Route("Account/Edit/{userName}")]
        [Authorize(Roles = Roles.ADMINANDMANAGER)]
        public async Task<IActionResult> Edit(string userName)
        {
            var result = await _accountService.GetUserByUserName(userName);
            UserEditDto dto = new UserEditDto();
            dto.User = result;

            return View(dto);
        }

        [HttpPost]
        [Authorize(Roles = Roles.ADMINANDMANAGER)]
        public async Task<IActionResult> Edit(UserEditDto account)
        {
            await _accountService.UpdateUser(account.User);

            ViewData["Message"] = "Sikeres mentés!";

            return View();
        }

        [HttpPost]
        [Authorize(Roles = Roles.ADMIN)]
        public async Task<IActionResult> Delete(UserEditDto account)
        {
            await _accountService.DeleteUser(account.User.UserName);

            return RedirectToAction("List");
        }
    }
}
