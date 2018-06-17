namespace TwoCS.TimeTracker.Core.Extensions
{
    using System;
    using System.Collections.Generic;

    public class BadRequestException : Exception
    {
        private const string ERROR_MESSAGE = "Something went wrong.Please try again later.";

        public IEnumerable<string> Errors { get; set; }

        public BadRequestException(string message) 
            : this(message, null)
        {

        }

        public BadRequestException(IEnumerable<string> errors)
            : this(null, errors)
        {

        }

        public BadRequestException(string message, IEnumerable<string> errors)
            : base(message ?? ERROR_MESSAGE)
        {
            Errors = errors ?? new List<string> {
                ERROR_MESSAGE
            };
        }
    }
}
