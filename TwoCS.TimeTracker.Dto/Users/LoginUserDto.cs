
namespace TwoCS.TimeTracker.Dto
{
    using System.ComponentModel.DataAnnotations;
    using FluentValidation.Attributes;
    using TwoCS.TimeTracker.Dto.Validators;

    [Validator(typeof(LoginUserDtoValidator<LoginUserDto>))]
    public class LoginUserDto : DtoBase, IDto
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
