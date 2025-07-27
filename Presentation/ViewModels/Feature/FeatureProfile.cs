using Application.DTOs.Features;
using AutoMapper;
using Domain.Models;

namespace Presentation.ViewModels.Feature
{
    public class FeatureProfile : Profile
    {
        public FeatureProfile()
        {
            CreateMap<Domain.Models.Feature, GetAllFeaturesDTO>();
        }
    }
}
