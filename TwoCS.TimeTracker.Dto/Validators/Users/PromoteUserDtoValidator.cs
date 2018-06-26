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

    public class AssignUserDtoValidator<T> : DtoBaseValidator<T>
        where T : AssignUserDto
    {
        public AssignUserDtoValidator()
        {
            RuleFor(x => x.Manager)
                 .NotEmpty();

            RuleFor(x => x.Member)
                .NotEmpty();
        }
    }


    public class AssignProjectUserDtoValidator<T> : DtoBaseValidator<T>
        where T : AssignProjectUserDto
    {
        public AssignProjectUserDtoValidator()
        {
            RuleFor(x => x.Project)
                 .NotEmpty();

            RuleFor(x => x.Member)
                .NotEmpty();
        }
    }


    public class SignInManagerDtoValidator<T> : DtoBaseValidator<T>
        where T : SignInManagerDto
    {
        public SignInManagerDtoValidator()
        {
            RuleFor(x => x.Manager)
                 .NotEmpty();
        }
    }
}
