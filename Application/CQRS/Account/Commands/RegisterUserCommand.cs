﻿using Application.CQRS.Account.Queries;
using Application.DTOs.User;
using AutoMapper;
using Domain.IRepositories;
using Domain.Models;
using Infrastructure.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Account.Commands
{
    public record RegisterUserCommand(UserRegisterDTO UserRegisterDTO):IRequest<UserRegisterDTO>;
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, UserRegisterDTO>
    {
        private IGeneralRepository<User> _generalRepository;
        private readonly IMediator _mediator;
        private IMapper _mapper;
        public RegisterUserCommandHandler(IGeneralRepository<User> generalRepository, IMediator mediator,IMapper mapper)
        {
            _generalRepository = generalRepository;
            _mediator = mediator;
            _mapper = mapper;
        }


        public async Task<UserRegisterDTO> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            bool Userexists = await _mediator.Send(new CheckIfUserExistsQuery(request.UserRegisterDTO.UserName));
            if (Userexists)
            {
                throw new ValidationException("User already exists!");
            }

            var user = _mapper.Map<User>(request.UserRegisterDTO);
            string HashedPass = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.Password = HashedPass;
            user.RoleId = new Guid(Constants.NoneRole);
           var res= await _generalRepository.AddAsync(user);
           await _generalRepository.SaveChangesAsync();

            return request.UserRegisterDTO;
        }
    }
}
