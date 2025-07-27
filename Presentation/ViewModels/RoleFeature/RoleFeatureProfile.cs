using Application.DTOs.RoleFeature;
using AutoMapper;

namespace Presentation.ViewModels.RoleFeature
{
    public class RoleFeatureProfile : Profile
    {
        public RoleFeatureProfile()
        {
            CreateMap<AssignFeatureToRoleDTO,Domain.Models.RoleFeature>()
                .ForMember(des => des.RoleID, opt => opt.MapFrom(src=>src.RoleId))
                .ForMember(des => des.FeatureID,opt=>opt.MapFrom(src => src.FeatureId))
                ;
            CreateMap<AssignFeatureToRoleVM, AssignFeatureToRoleDTO>();
        }
    }
}
