using Application.Views;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Category.Command
{
    public record UpdateCategoryCommand(string Id, string Name) : IRequest<RequestResult<bool>>;
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, RequestResult<bool>>
    {
        private readonly IMediator _mediator;
        private readonly Domain.IRepositories.IGeneralRepository<Domain.Models.Category> _generalRepository;
        public UpdateCategoryHandler(IMediator mediator, Domain.IRepositories.IGeneralRepository<Domain.Models.Category> generalRepository)
        {
            _mediator = mediator;
            _generalRepository = generalRepository;
        }
        public async Task<RequestResult<bool>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            // Check if category already exists
            var id = Guid.Parse(request.Id);
            var existingCategory = await _generalRepository.GetOneByIdAsync(id);
            if (existingCategory == null)
            {
                return RequestResult<bool>.Failure(ErrorCode.NotFound, "Category not found");
            }
            // Update the category
            existingCategory.Name = request.Name;
            // Save changes to the database
            await _generalRepository.UpdateIncludeAsync(existingCategory,nameof(existingCategory.Name));
            await _generalRepository.SaveChangesAsync(cancellationToken);
            return RequestResult<bool>.Success(true, "Category updated successfully");
        }
    }   
}
