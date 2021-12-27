using FluentValidation;

namespace Application.Users.Commands.Register
{
    public class UserRegisterCommandValidator : AbstractValidator<UserRegisterCommand>
    {
        public static readonly int USERNAME_MIN_LENGTH = 2;
        public static readonly int USERNAME_MAX_LENGTH = 100;

        public static readonly int PASSWORD_MIN_LENGTH = 4;
        public static readonly int PASSWORD_MAX_LENGTH = 10;

        public UserRegisterCommandValidator()
        {
            RuleFor(u => u.Username)
                .NotEmpty()
                .WithMessage("Username is required field")
                .MinimumLength(USERNAME_MIN_LENGTH)
                .WithMessage($"Username is minimum length is {USERNAME_MIN_LENGTH}")
                .MaximumLength(USERNAME_MAX_LENGTH)
                .WithMessage($"Username is maximum length is {USERNAME_MAX_LENGTH}");

            RuleFor(u => u.Password)
                .NotEmpty()
                .WithMessage("Username is required field")
                .MinimumLength(PASSWORD_MIN_LENGTH)
                .WithMessage($"Username is minimum length is {PASSWORD_MIN_LENGTH}")
                .MaximumLength(PASSWORD_MAX_LENGTH)
                .WithMessage($"Username is maximum length is {PASSWORD_MAX_LENGTH}");
        }
    }
}
