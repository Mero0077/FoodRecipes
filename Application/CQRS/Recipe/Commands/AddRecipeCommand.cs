using Application.CQRS.Recipe.Queries;
using Application.DTOs.Recipes;
using AutoMapper;
using Domain.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Recipe.Commands
{
    public record AddRecipeCommand(AddRecipeDTO AddRecipeDTO) : IRequest<Domain.Models.Recipe>;

    public class AddRecipeCommandHandler : IRequestHandler<AddRecipeCommand, Domain.Models.Recipe>
    {
        private readonly IGeneralRepository<Domain.Models.Recipe> recipeRepo;
        private readonly IMapper _mapper;

        public AddRecipeCommandHandler(IGeneralRepository<Domain.Models.Recipe> recipeRepo,IMapper mapper)
        {
            this.recipeRepo = recipeRepo;
            this._mapper = mapper;
        }

        public async Task<Domain.Models.Recipe> Handle(AddRecipeCommand request, CancellationToken cancellationToken)
        {
            var newRecipe = new Domain.Models.Recipe
            {
                Name = request.AddRecipeDTO.Name,
                Description = request.AddRecipeDTO.Description,
                Price = request.AddRecipeDTO.Price,
                CategoryId = request.AddRecipeDTO.CategoryId,
                Tag = request.AddRecipeDTO.Tag,
                ImageUrl = request.AddRecipeDTO.ImageUrl
            };

            await recipeRepo.AddAsync(newRecipe);
            await recipeRepo.SaveChangesAsync();

            return newRecipe;
        }
    }


}
