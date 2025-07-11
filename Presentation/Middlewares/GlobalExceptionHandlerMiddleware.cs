
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.Hosting;
using Presentation.Enums.ErrorsCode;
using Presentation.ErrorDTOS;
using Presentation.Exceptions;
using System;
using System.Text.Json;

namespace Presentation.Middlewares
{
    public class GlobalExceptionHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public GlobalExceptionHandlerMiddleware(ILogger<GlobalExceptionHandlerMiddleware> logger,IWebHostEnvironment webHostEnvironment) 
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._webHostEnvironment = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {

            var traceId = context.TraceIdentifier;

            try
            {
                _logger.LogInformation($"Request Started. TraceId:{traceId} , Method:{context.Request.Method} ,Path:{context.Request.Path} ");

                await next(context);

                _logger.LogInformation($"Request Successfully Completed. TraceId:{traceId} , Method:{context.Request.Method} ,Path:{context.Request.Path} ");
            }
            catch (UnAuthorizedException ex)
            {
                await HandleUnAuthorizedException(context, ex, traceId);
            }
            catch (NotFoundException ex)
            {
                await HandleNotFoundException(context, ex, traceId);
            }
            catch (ValidationException ex)
            {
                await HandleValidationException(context, ex, traceId);
            }
            catch (BaseApplicationException ex)
            {
                await HandleBaseApplicationException(context, ex, traceId);
            }
            catch (Exception ex) 
            {
                await HandleUnExpectedException(context,ex, traceId);
            }

        }

        private async Task HandleUnExpectedException(HttpContext context, Exception exception, string traceId)
        {
            _logger.LogError(
               exception,
               "Unexpected error occurred. TraceId: {TraceId}, RequestPath: {RequestPath}",
               traceId,
               context.Request.Path);

            var response = new ErrorFailResponse<object>(exception.Message, _webHostEnvironment.IsDevelopment() ? traceId : null,ErrorCode.InternalErrorServer );

            await WriteErrorResponseAsync<object>(context, response, traceId, StatusCodes.Status500InternalServerError);
        }

        private async Task HandleValidationException(HttpContext context, BaseApplicationException exception, string traceId)
        {
            var logLevel = exception is BusinessLogicException ? LogLevel.Warning : LogLevel.Error;

            _logger.Log(
                logLevel,
                exception,
               $"Application Exception. TraceId: {traceId}, Message: {exception.Message}, ErrorCode: {exception.ErrorCode}, RequestPath: {context.Request.Path}"
                );
            var response = new ErrorFailResponse<object>(exception.Message,_webHostEnvironment.IsDevelopment() ? traceId : null,exception.ErrorCode);
            await WriteErrorResponseAsync(context,response, traceId,StatusCodes.Status400BadRequest);
        }

        private async Task HandleNotFoundException(HttpContext context, BaseApplicationException execption, string traceId)
        {
            var logLevel = execption is BusinessLogicException ? LogLevel.Warning : LogLevel.Error;
            _logger.Log(
            logLevel,
            execption,
            $"Application Exception. TraceId: {traceId}, Message: {execption.Message}, ErrorCode: {execption.ErrorCode}, RequestPath: {context.Request.Path}"
                );
            var response = new ErrorFailResponse<object>(execption.Message,_webHostEnvironment.IsDevelopment()?traceId:null,execption.ErrorCode);
            await WriteErrorResponseAsync<object>(context, response,traceId,StatusCodes.Status400BadRequest);
        }

        private async Task HandleUnAuthorizedException(HttpContext context,UnAuthorizedException exception,string traceId)
        {
            _logger.LogWarning(
               exception,
               "Unauthorized Access Exception. TraceId: {TraceId}, Message: {Message}, RequestPath: {RequestPath}",
               traceId,
               exception.Message,
               context.Request.Path);

            var response = new ErrorFailResponse<object>("Access Denied", _webHostEnvironment.IsDevelopment() ? traceId : null ,ErrorCode.UnAuthorized);


            await WriteErrorResponseAsync<object>(context, response,traceId,StatusCodes.Status403Forbidden);

        }

        private async Task HandleBaseApplicationException(HttpContext context, BaseApplicationException exception, string traceId)
        { 
            var logLevel = exception is BusinessLogicException ? LogLevel.Warning : LogLevel.Error;

            _logger.Log(
                logLevel,
                exception,
               $"Application Exception. TraceId: {traceId}, Message: {exception.Message}, ErrorCode: {exception.ErrorCode}, RequestPath: {context.Request.Path}"
                );
            var response = new ErrorFailResponse<object>(exception.Message, _webHostEnvironment.IsDevelopment() ? traceId : null, exception.ErrorCode);

            await WriteErrorResponseAsync<object>(context, response,traceId,StatusCodes.Status403Forbidden);
        }

        private async Task WriteErrorResponseAsync<T>(HttpContext context,ResponseDTO<T> response,string traceId,int statusCode)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
            context.Response.Headers.Append("X-Trace-Id", traceId);

            var jsonOptions = new JsonSerializerOptions() { PropertyNamingPolicy=JsonNamingPolicy.CamelCase};
            
            await context.Response.WriteAsJsonAsync(response, jsonOptions);

        }

    }
}
