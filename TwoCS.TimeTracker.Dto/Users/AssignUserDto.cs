namespace TwoCS.TimeTracker.Dto
{
    using FluentValidation.Attributes;
    using TwoCS.TimeTracker.Dto.Validators;

    [Validator(typeof(AssignUserDtoValidator<AssignUserDto>))]
    public class AssignUserDto : DtoBase, IDto
    {
        public string Manager { get; set; }

        public string Member { get; set; }
    }

    [Validator(typeof(AssignProjectUserDtoValidator<AssignProjectUserDto>))]
    public class AssignProjectUserDto : DtoBase, IDto
    {
        public string Project { get; set; }

        public string Member { get; set; }
    }

    [Validator(typeof(SignInManagerDtoValidator<SignInManagerDto>))]
    public class SignInManagerDto : DtoBase, IDto
    {
        public string Manager { get; set; }
    }
}
