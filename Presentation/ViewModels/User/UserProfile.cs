using AutoMapper;
using Domain.Models;
using Presentation.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.User
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<UserRegisterVM, UserRegisterDTO>().ReverseMap();
            CreateMap<UserRegisterDTO, Domain.Models.User>();

            CreateMap<UserLoginVM, LoginDTO>();
            
        }
    }
}
