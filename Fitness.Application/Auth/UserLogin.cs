using FluentValidation;

namespace Fitness.Application.Auth
{
    public record UserLoginCommand(string Email, string Password) : IRequest<UserResponse>
    {
        public class Validator : AbstractValidator<UserLoginCommand>
        {
            public Validator()
            {
                RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required").WithMessage("Invalid email format.");
                RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");
            }
        }

    }


    public class UserLoginHandler() : IRequestHandler<UserLoginCommand, UserResponse>
    {
        public async Task<UserResponse> Handle(UserLoginCommand request, CancellationToken ct)
        {
            return await Task.FromResult(new UserResponse { Token = "Login WORKING" });
        }
    }
}
