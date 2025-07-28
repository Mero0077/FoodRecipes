using Domain.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Category.Query
{
    public record GetCategoryByIdQuery(Guid id):IRequest<Domain.Models.Category>;
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Domain.Models.Category>
    {
        private readonly IGeneralRepository<Domain.Models.Category> generalRepository;

        public GetCategoryByIdQueryHandler(IGeneralRepository<Domain.Models.Category> generalRepository)
        {
            this.generalRepository = generalRepository;
        }
        public async Task<Domain.Models.Category> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            return await generalRepository.GetOneByIdAsync( request.id);
        }
    }
}
