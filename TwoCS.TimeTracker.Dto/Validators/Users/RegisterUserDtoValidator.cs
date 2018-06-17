namespace TwoCS.TimeTracker.Dto.Validators
{
    using FluentValidation;
    using TwoCS.TimeTracker.Dto;

    public class RegisterUserDtoValidator : LoginUserDtoValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator()
            : base()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(30);
        }
    }
}
