using Application.CQRS.Category.Command;
using Application.CQRS.Category.Query;
using Application.DTOs.Category;
using Application.Views;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.ViewModels;
using Presentation.ViewModels.Category;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Example endpoint for getting all categories
        [HttpGet("GetAllCategories")]
        public async Task<EndpointResponse<List<CategoryDTO>>> Index()
        {
            var request = await _mediator.Send(new GetAllCategoriesQuery());

            if (request.IsSuccess == true)
            {
                return EndpointResponse<List<CategoryDTO>>.Success(request.Data, request.Message);
            }
            else
            {
                return EndpointResponse<List<CategoryDTO>>.Failure(request.ErrorCode, request.Message);
            }
        }
        // Example endpoint for creating a new category
        [HttpPost("CreateCategory")]
        public async Task<EndpointResponse<bool>> CreateCategory([FromBody] CreateCategoryVM command)
        {
            if (!ModelState.IsValid)
            {
                return EndpointResponse<bool>.Failure(ErrorCode.DataNotValide, "Invalid model state.");
            }
            var request = await _mediator.Send(new CreateCategoryCommand(command.Name));
            if (request.IsSuccess == true)
            {
                return EndpointResponse<bool>.Success(request.Data, request.Message);
            }
            else
            {
                return EndpointResponse<bool>.Failure(request.ErrorCode, request.Message);
            }
        }

        [HttpPost("UpdateCategory")]
        public async Task<EndpointResponse<bool>> UpdateCategory([FromBody] UpdateCategoryVM command)
        {
            if (!ModelState.IsValid)
            {
                return EndpointResponse<bool>.Failure(ErrorCode.DataNotValide, "Invalid model state.");
            }
            var request = await _mediator.Send(new UpdateCategoryCommand(command.Id, command.Name));
            if (request.IsSuccess == true)
            {
                return EndpointResponse<bool>.Success(request.Data, request.Message);
            }
            else
            {
                return EndpointResponse<bool>.Failure(request.ErrorCode, request.Message);
            }
        }
        [HttpPost("DeleteCategory")]
        public async Task<EndpointResponse<bool>> DeleteCategory([FromBody] DeleteCategoryVM command)
        {
            if (!ModelState.IsValid)
            {
                return EndpointResponse<bool>.Failure(ErrorCode.DataNotValide, "Invalid model state.");
            }
            var request = await _mediator.Send(new DeleteCategoryCommand(command.Id));
            if (request.IsSuccess == true)
            {
                return EndpointResponse<bool>.Success(request.Data, request.Message);
            }
            else
            {
                return EndpointResponse<bool>.Failure(request.ErrorCode, request.Message);
            }
        }

    }
}
