using Application.Views;
using Domain.IRepositories;
using MediatR;
using Microsoft.AspNetCore.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Category.Command
{
    public record CreateCategoryCommand(string Name) : IRequest<RequestResult<bool>>;

    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand,RequestResult<bool>>
    {
        private readonly IMediator _mediator;
        private readonly IGeneralRepository<Domain.Models.Category> _generalRepository;
        public CreateCategoryHandler(IMediator mediator,IGeneralRepository<Domain.Models.Category> generalRepository) 
        {
            _mediator = mediator;
            _generalRepository = generalRepository;
        }

        public async Task<RequestResult<bool>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var existingCategory =await  _generalRepository.AnyAsync(c => c.Name == request.Name && !c.IsDeleted, cancellationToken);

            if(existingCategory == true)
            {
                return RequestResult<bool>.Failure(ErrorCode.Exist,$"Category with name '{request.Name}' already exists.");
            }

            var category = new Domain.Models.Category
            {
                Name = request.Name,
            };

            await _generalRepository.AddAsync(category);
            await _generalRepository.SaveChangesAsync(cancellationToken);

            return RequestResult<bool>.Success(true, $"Category '{request.Name}' created successfully.");
        }
    }
    
}
