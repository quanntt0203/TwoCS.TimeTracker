namespace TwoCS.TimeTracker.Dto
{
    using System.Collections.Generic;
    using FluentValidation.Results;

    public class ValidationDto
    {
        public bool IsValid { get; private set; }

        public IList<string> Errors { get; private set; }

        public ValidationDto() : this(true)
        {
        }

        public ValidationDto(bool isValid)
        {
            IsValid = isValid;
        }

        public ValidationDto(ValidationResult validationResult)
        {
            IsValid = validationResult.IsValid;
            if (!validationResult.IsValid)
            {
                Errors = GetErrors(validationResult.Errors);
            }
        }

        private IList<string> GetErrors(IList<ValidationFailure> Errors)
        {
            IList<string> obj = new List<string>();
            foreach (ValidationFailure error in Errors)
            {
                obj.Add(error.ErrorMessage);
            }
            return obj;
        }
    }
}
