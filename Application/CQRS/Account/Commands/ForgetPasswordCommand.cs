using Domain.IRepositories;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Account.Commands
{
    public record ForgetPasswordCommand(string Email) : IRequest<bool>;

    public class ForgetPasswordCommandHandler : IRequestHandler<ForgetPasswordCommand, bool>
    {
        private readonly IGeneralRepository<User> _generalRepository;
        private readonly IGeneralRepository<PasswordReset> _passwordResetRepository;
        private readonly IEmailSender _emailSender;

        public ForgetPasswordCommandHandler(
            IGeneralRepository<User> generalRepository,
            IEmailSender emailSender,
            IGeneralRepository<PasswordReset> passwordResetRepository)
        {
            _generalRepository = generalRepository;
            _emailSender = emailSender;
            _passwordResetRepository = passwordResetRepository;
        }

        public async Task<bool> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _generalRepository.GetOneWithTrackingAsync(u => u.Email == request.Email);
            if (user == null) return false;

            var otp = new Random().Next(100000, 999999).ToString();

            var existing = await _passwordResetRepository.GetOneWithTrackingAsync(r => r.UserId == user.Id);
            if (existing != null)
            {
                existing.OtpCode = otp;
                existing.Expiration = DateTime.UtcNow.AddMinutes(10);
            }
            else
            {
                var reset = new PasswordReset
                {
                    UserId = user.Id,
                    OtpCode = otp,
                    Expiration = DateTime.UtcNow.AddMinutes(10)
                };
                await _passwordResetRepository.AddAsync(reset);
            }

            await _passwordResetRepository.SaveChangesAsync(cancellationToken);

            await _emailSender.SendEmailAsync(
                user.Email,
                "Your Password Reset OTP",
                $"Your OTP code is: {otp}"
            );

            return true;
        }
    }




}
