using Application.CQRS.WishList.Queries;
using Application.DTOs.Recipes;
using Domain.IRepositories;
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
    public record EnsureWishListExistsCommand() : IRequest<Guid>;

    public class EnsureWishListExistsCommandHandler : IRequestHandler<EnsureWishListExistsCommand, Guid>
    {
        private readonly IGeneralRepository<Domain.Models.WishList> wishListRepo;
        private IMediator mediator;
        public EnsureWishListExistsCommandHandler(IGeneralRepository<Domain.Models.WishList> wishListRepo,IMediator mediator)
        {
            this.wishListRepo = wishListRepo;
            this.mediator = mediator;
        }

        public async Task<Guid> Handle(EnsureWishListExistsCommand request, CancellationToken cancellationToken)
        {
            var existingWishListId = await mediator.Send(new IsWishListExistsQuery());

            if (existingWishListId.HasValue)
                return existingWishListId.Value;

            var userId = mediator.Send(new IsUserExistsQuery());

            var newWishList = new Domain.Models.WishList
            {
                UserId = userId.Result
            };

            await wishListRepo.AddAsync(newWishList);
            return newWishList.Id;
        }
    }

}
