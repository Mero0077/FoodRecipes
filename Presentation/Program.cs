using Application.CQRS.Account.Commands;
using Application.CQRS.Account.Queries;
using Application.CQRS.Account.Shared;
using Application.CQRS.WishList.Queries;
using Application.DTOs.User;
using Domain.IRepositories;
using Hangfire;
using Infrastructure.AppDbContext;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Presentation.Controllers;
using Presentation.Middlewares;
using Presentation.Shared;
using Presentation.ViewModels.RecipeWishList;
using Presentation.ViewModels.Roles;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
                    .LogTo(log => Debug.WriteLine(log), LogLevel.Information)
                    .EnableSensitiveDataLogging(true)
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                    .UseLazyLoadingProxies());

            // Add services to the container.
            builder.Services.AddControllers();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddHangfire(config =>
            {
                config.UseSqlServerStorage(connectionString);
            });

            builder.Services.AddHangfireServer();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
   


    
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IEmailSender, EmailSender>();

            builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("Jwt"));
            var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JWTSettings>();
            var key = Encoding.ASCII.GetBytes(jwtSettings.SecretKey);

            builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                };
            });
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            builder.Services.AddScoped<GlobalExceptionHandlerMiddleware>();
            builder.Services.AddScoped(typeof(IGeneralRepository<>), typeof(GeneralRepository<>));
            builder.Services.AddScoped<UserCredentialsChecker>();

            builder.Services.AddAutoMapper(
                typeof(UserProfile).Assembly,
                typeof(RoleProfile).Assembly,
                typeof(RecipeWisListProfile).Assembly,
                typeof(WishListProfile).Assembly);

           
            builder.Services.AddMediatR(cfg =>cfg.RegisterServicesFromAssembly(typeof(IsWishListExistsQueryHandler).Assembly));


            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
            app.UseAuthorization();
            app.UseHangfireDashboard();
            app.MapControllers();

            app.Run();
        }
    }
}
