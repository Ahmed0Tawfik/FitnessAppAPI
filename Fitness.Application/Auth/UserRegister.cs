using FluentValidation;

namespace Fitness.Application.Auth
{
   
    public record UserRegisterCommand(string Email, string Password) : IRequest<UserResponse>
    {
        public class Validator : AbstractValidator<UserRegisterCommand>
        {
            public Validator()
            {
                RuleFor(x => x.Email)
                    .NotEmpty().WithMessage("Email is required.")
                    .EmailAddress().WithMessage("Invalid email format.");

                RuleFor(x => x.Password)
                    .NotEmpty().WithMessage("Password is required.")
                    .MinimumLength(8).WithMessage("Password must be at least 8 characters long.");
            }
        }


    }

    public class UserRegisterHandler() : IRequestHandler<UserRegisterCommand,UserResponse>
    {
        
        public async Task<UserResponse> Handle(UserRegisterCommand request, CancellationToken ct)
        {
            return await Task.FromResult(new UserResponse { Token = "Register WORKING" });
        }
    }
}
