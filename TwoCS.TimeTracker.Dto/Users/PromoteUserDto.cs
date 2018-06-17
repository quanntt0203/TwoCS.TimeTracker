namespace TwoCS.TimeTracker.Dto
{
    using FluentValidation.Attributes;
    using TwoCS.TimeTracker.Dto.Validators;

    [Validator(typeof(PromoteUserDtoValidator<PromoteUserDto>))]
    public class PromoteUserDto : DtoBase, IDto
    {
        public string UserName { get; set; }

        public string ConfirmMessage { get; set; }
    }
}
