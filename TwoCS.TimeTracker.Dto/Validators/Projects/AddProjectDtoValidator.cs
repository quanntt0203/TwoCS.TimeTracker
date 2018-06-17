namespace TwoCS.TimeTracker.Dto.Validators
{
    using FluentValidation;
    using TwoCS.TimeTracker.Dto;

    public class AddProjectDtoValidator<T> : DtoBaseValidator<T> 
        where T : AddProjectDto
    {
        public AddProjectDtoValidator()
        {
            RuleFor(x => x.Name)
                 .NotEmpty();
        }
    }
}
