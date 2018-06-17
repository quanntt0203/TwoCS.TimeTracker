namespace TwoCS.TimeTracker.Dto
{
    using FluentValidation.Attributes;
    using TwoCS.TimeTracker.Dto.Validators;

    [Validator(typeof(RegisterUserDtoValidator))]
    public class RegisterUserDto : LoginUserDto
    {
        public string UserName { get; set; }

        public string Role { get; set; } = "User";
    }
}
