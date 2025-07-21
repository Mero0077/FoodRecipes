using Application.Enums.ErrorCodes;
using Application.Exceptions;
using Domain.IRepositories;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.WishList.Queries
{

    public record IsWishListExistsQuery() : IRequest<Guid?>;
    public class IsWishListExistsQueryHandler : IRequestHandler<IsWishListExistsQuery, Guid?>
    {
        private readonly IGeneralRepository<Domain.Models.WishList> _wishListRepo;
        
        private readonly IMediator _mediator;

        public IsWishListExistsQueryHandler(IGeneralRepository<Domain.Models.WishList> wishListRepo, IMediator mediator)
        {
            _wishListRepo = wishListRepo;
            _mediator = mediator;
        }

        public async Task<Guid?> Handle(IsWishListExistsQuery request, CancellationToken cancellationToken)
        {
            var userId = await _mediator.Send(new IsUserExistsQuery());
            var wishlist = await _wishListRepo.Get(e => e.UserId == userId).FirstOrDefaultAsync();
            return wishlist?.Id;
        }
    }
}
