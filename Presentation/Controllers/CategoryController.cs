using Application.CQRS.Category.Command;
using Application.CQRS.Category.Query;
using Application.DTOs.Category;
using Application.Views;
using AutoMapper.Features;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Filters;
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

        [HttpGet("GetAllCategories")]
        [Authorize]
        [TypeFilter<CustomAuthorizeFilter>(Arguments = new object[] { FeatureCode.GetAllCategories })]


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
        [HttpPost("CreateCategory")]
        [Authorize]
        [TypeFilter<CustomAuthorizeFilter>(Arguments = new object[] { FeatureCode.CreateCategory })]

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

        [HttpPatch("UpdateCategory/{id}")]
        [Authorize]
        [TypeFilter<CustomAuthorizeFilter>(Arguments = new object[] { FeatureCode.UpdateCategory })]

        public async Task<EndpointResponse<bool>> UpdateCategory([FromRoute] Guid id,[FromBody] UpdateCategoryVM command)
        {
            if (!ModelState.IsValid)
            {
                return EndpointResponse<bool>.Failure(ErrorCode.DataNotValide, "Invalid model state.");
            }
            var request = await _mediator.Send(new UpdateCategoryCommand(id, command.Name));
            if (request.IsSuccess == true)
            {
                return EndpointResponse<bool>.Success(request.Data, request.Message);
            }
            else
            {
                return EndpointResponse<bool>.Failure(request.ErrorCode, request.Message);
            }
        }
        [HttpDelete("DeleteCategory/{Id}")]
        [Authorize]
        [TypeFilter<CustomAuthorizeFilter>(Arguments = new object[] { FeatureCode.DeleteCategory })]

        public async Task<EndpointResponse<bool>> DeleteCategory([FromRoute] DeleteCategoryVM deleteCategoryVM)
        {
            if (!ModelState.IsValid)
            {
                return EndpointResponse<bool>.Failure(ErrorCode.DataNotValide, "Invalid model state.");
            }
            var request = await _mediator.Send(new DeleteCategoryCommand(deleteCategoryVM.Id));
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
