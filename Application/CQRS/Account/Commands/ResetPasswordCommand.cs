using Application.CQRS.Account.Queries;
using Application.CQRS.Account.Shared;
using Application.DTOs.User;
using AutoMapper;
using Domain.IRepositories;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Account.Commands
{
    public record ResetPasswordCommand(ResetPasswordDTO resetPasswordDTO): IRequest<bool>;

    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, bool>
    {
        private IGeneralRepository<User> _generalRepository;
        private readonly IMediator _mediator;
        private IMapper _mapper;
        private readonly UserCredentialsChecker _userCredentialsChecker;

        public ResetPasswordCommandHandler(IGeneralRepository<User> generalRepository, IMediator mediator, IMapper mapper,UserCredentialsChecker userCredentialsChecker)
        {
            _generalRepository = generalRepository;
            _mediator = mediator;
            _mapper = mapper;
            _userCredentialsChecker = userCredentialsChecker;
        }
        public async Task<bool> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userCredentialsChecker.GetUserIfCredentialsMatch(request.resetPasswordDTO.UserName, request.resetPasswordDTO.OldPassword);
            user.Password = BCrypt.Net.BCrypt.HashPassword(request.resetPasswordDTO.NewPassword);
            await _generalRepository.UpdateAsync(user);
           await _generalRepository.SaveChangesAsync();
            return true;
        }
    }


}
