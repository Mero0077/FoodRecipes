using Application.DTOs.Roles;
using AutoMapper;
using Domain.Models;

namespace Presentation.ViewModels.Roles
{
    public class RoleProfile : Profile
    {
        public RoleProfile() 
        {
            CreateMap<CreateRoleVM, CreateRoleDTO>();

            CreateMap<CreateRoleDTO, Role>();

            CreateMap<Role, GetAllRolesDTO>();
            CreateMap<GetAllRolesDTO, GetAllRolesVM>();

        }
    }
}
