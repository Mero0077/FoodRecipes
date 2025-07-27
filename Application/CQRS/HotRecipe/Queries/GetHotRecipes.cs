using Application.Enums.ErrorCodes;
using Application.Exceptions;
using Domain.IRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.HotRecipe.Queries
{
   public record GetHotRecipes(int limit) : IRequest<List<Domain.Models.Recipe>>;

public class GetHotRecipesHandler : IRequestHandler<GetHotRecipes, List<Domain.Models.Recipe>>
{
    private readonly IGeneralRepository<Domain.Models.HotRecipe> generalRepository;

    public GetHotRecipesHandler(IGeneralRepository<Domain.Models.HotRecipe> generalRepository)
    {
        this.generalRepository = generalRepository;
    }

    public async Task<List<Domain.Models.Recipe>> Handle(GetHotRecipes request, CancellationToken cancellationToken)
    {
        var res = await generalRepository.GetAll()
            .OrderByDescending(e => e.ViewCount * 0.2 + e.WishlistCount * 0.8)
            .Take(request.limit)
            .Select(e => new Domain.Models.Recipe
            {
                Id = e.Recipe.Id,
                Name = e.Recipe.Name,
                Description = e.Recipe.Description,
                Price = e.Recipe.Price,
                ImageUrl = e.Recipe.ImageUrl,
                CategoryId = e.Recipe.CategoryId
            })
            .ToListAsync(cancellationToken);

        if (res.Count == 0) return null;

        return res;
    }
}

}
