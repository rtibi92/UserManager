using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManager.BusinessLogic.DTO;
using UserManager.DataAccess.Model;

namespace UserManager.BusinessLogic.Mapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserDto>().ReverseMap();

           CreateMap<IEnumerable<UserDto>, UserListDto>()
                .ForMember(dest => dest.Users, o => o.MapFrom(src => src));

            CreateMap<IEnumerable<UserDto>, UserEditDto>()
                .ForMember(dest => dest.User, o => o.MapFrom(src => src));
        }
    }
}
