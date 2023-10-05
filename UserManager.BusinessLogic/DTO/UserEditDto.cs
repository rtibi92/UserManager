using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManager.BusinessLogic.DTO
{
    public class UserEditDto
    {
        [BindProperty]
        public UserDto User { get; set; }
        //public string UserName { get; set; }
        //public string Email { get; set; }

        //public string FirstName { get; set; }

        //public string LastName { get; set; }
    }
}
