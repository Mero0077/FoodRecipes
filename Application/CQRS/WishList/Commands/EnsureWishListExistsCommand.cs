using Application.CQRS.WishList.Queries;
using Application.DTOs.Recipes;
using Domain.IRepositories;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.WishList.Commands
{
    public record EnsureWishListExistsCommand() : IRequest<Domain.Models.WishList>;

    public class EnsureWishListExistsCommandHandler : IRequestHandler<EnsureWishListExistsCommand, Domain.Models.WishList>
    {
        private readonly IGeneralRepository<Domain.Models.WishList> wishListRepo;
        private IMediator mediator;
        public EnsureWishListExistsCommandHandler(IGeneralRepository<Domain.Models.WishList> wishListRepo,IMediator mediator)
        {
            this.wishListRepo = wishListRepo;
            this.mediator = mediator;
        }

        public async Task<Domain.Models.WishList> Handle(EnsureWishListExistsCommand request, CancellationToken cancellationToken)
        {
            var existingWishList = await mediator.Send(new IsWishListExistsQuery());

            if (existingWishList!=null)
                return existingWishList;

            var userId = mediator.Send(new IsUserExistsQuery());
            if(userId==null) throw new UnauthorizedAccessException("User ID not found in token");

            var newWishList = new Domain.Models.WishList
            {
                UserId = userId.Result
            };

            await wishListRepo.AddAsync(newWishList);
            await wishListRepo.SaveChangesAsync();
            return newWishList;

        }
    }

}
