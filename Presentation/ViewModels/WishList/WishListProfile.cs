using AutoMapper;

namespace Presentation.ViewModels.WishList
{
    public class WishListProfile : Profile
    {
        public WishListProfile()
        {
            CreateMap<Domain.Models.WishList, AddWishListVM>();
        }
    }
}
