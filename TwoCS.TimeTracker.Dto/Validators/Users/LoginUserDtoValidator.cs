namespace TwoCS.TimeTracker.Dto.Validators
{
    using FluentValidation;
    using TwoCS.TimeTracker.Dto;

    public class LoginUserDtoValidator<T> : DtoBaseValidator<T> 
        where T : LoginUserDto
    {
        public LoginUserDtoValidator()
        {
            RuleFor(x => x.Email)
                 .NotEmpty()
                 .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6)
                .MaximumLength(30);
        }
    }
}
