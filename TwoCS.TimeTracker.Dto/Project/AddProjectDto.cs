namespace TwoCS.TimeTracker.Dto
{
    using FluentValidation.Attributes;
    using Validators;

    [Validator(typeof(AddProjectDtoValidator<AddProjectDto>))]
    public class AddProjectDto : DtoBase, IDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

    }
}
