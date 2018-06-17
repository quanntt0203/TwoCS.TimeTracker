namespace TwoCS.TimeTracker.DtoValidation.Users
{
    using FluentValidation;
    using TwoCS.TimeTracker.Dto.User;

    public class UserLoginDtoValidator : DtoBaseValidator<UserLoginDto>
    {
        public UserLoginDtoValidator()
        {
            RuleFor(x => x.UserName)
                 .NotEmpty()
                 .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6);
        }
    }
}
