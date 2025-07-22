using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Category;
using Domain.IRepositories;
using Domain.Models;
using Application.Views;

namespace Application.CQRS.Category.Query
{
    public record GetAllCategoriesQuery():IRequest<RequestResult<List<CategoryDTO>>>;

    public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, RequestResult<List<CategoryDTO>>>
    {
        private readonly IGeneralRepository<Domain.Models.Category> _generalRepository;
        public GetAllCategoriesHandler(IGeneralRepository<Domain.Models.Category> generalRepository)
        {
            _generalRepository = generalRepository;
        }
        public async Task<RequestResult<List<CategoryDTO>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories =  _generalRepository.GetAll().Select(
                
                    c => new CategoryDTO
                    {
                        Id = c.Id.ToString(),
                        Name = c.Name,
                    }
            ).ToList();

            if (categories.Any())
            {
               return RequestResult<List<CategoryDTO>>.Success(categories, "Categories retrieved successfully.");
            }
            else
            {
                return RequestResult<List<CategoryDTO>>.Failure(ErrorCode.NotFound, "No categories found.");
            }

        }
    }
}
