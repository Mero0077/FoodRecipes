using Application.CQRS.Category.Query;
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
    public record DeleteCategoryCommand(Guid id) : IRequest<RequestResult<bool>>;

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

            var existingCategory = await _mediator.Send(new GetCategoryByIdQuery(request.id));

            if (existingCategory==null) return RequestResult<bool>.Failure(ErrorCode.NotFound, "Category not found.");

            await _generalRepository.DeleteAsync(request.id);
            await _generalRepository.SaveChangesAsync(cancellationToken);

            return RequestResult<bool>.Success(true,"Deleted Succeffully.");

        }
    }

}
