using AutoMapper;
using Presentation.ViewModels.WishList;

namespace Presentation.Controllers
{
    public class WishListProfile:Profile
    {
        public WishListProfile()
        {
            CreateMap<Domain.Models.WishList,AddWishListVM>();
        }
    }
}
