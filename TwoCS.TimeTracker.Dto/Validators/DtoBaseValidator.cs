namespace TwoCS.TimeTracker.Dto.Validators
{
    using FluentValidation;
    using TwoCS.TimeTracker.Dto;

    public class DtoBaseValidator<T> : AbstractValidator<T> where T : IDto
    {
        public DtoBaseValidator()
        {
            RuleFor(x => x.IsValid)
                .Equals(true);
        }
    }
}
