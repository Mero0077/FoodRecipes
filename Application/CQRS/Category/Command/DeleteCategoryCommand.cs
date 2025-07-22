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
    public record DeleteCategoryCommand(string id) : IRequest<RequestResult<bool>>;

    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, RequestResult<bool>>
    {
        private readonly IMediator _mediator;
        private readonly IGeneralRepository<Domain.Models.Category> _generalRepository;
        public DeleteCategoryHandler(IMediator mediator, IGeneralRepository<Domain.Models.Category> generalRepository)
        {
            _mediator = mediator;
            _generalRepository = generalRepository;
        }

        public async Task<RequestResult<bool>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            // Check if category already exists
            var id = Guid.TryParse(request.id, out var categoryId) ? categoryId : Guid.Empty;
            var existingCategory = await _generalRepository.AnyAsync(c => c.Id == id);

            if (existingCategory == false)
            {
               return RequestResult<bool>.Failure(ErrorCode.NotFound, "Category not found.");
            }

            // Add the new category
            await _generalRepository.DeleteAsync(id);
            // Save changes to the database
            await _generalRepository.SaveChangesAsync(cancellationToken);

            return RequestResult<bool>.Success(true,"Deleted Succeffully.");

        }
    }

}
