namespace TwoCS.TimeTracker.Dto.Validators
{
    using FluentValidation;
    using TwoCS.TimeTracker.Dto;

    public class PromoteUserDtoValidator<T> : DtoBaseValidator<T> 
        where T : PromoteUserDto
    {
        public PromoteUserDtoValidator()
        {
            RuleFor(x => x.UserName)
                 .NotEmpty();

            RuleFor(x => x.ConfirmMessage)
                .NotEmpty();
        }
    }
}
