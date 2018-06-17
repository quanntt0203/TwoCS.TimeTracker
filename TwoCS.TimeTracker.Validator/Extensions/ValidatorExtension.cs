namespace TwoCS.TimeTracker.DtoValidation.Extensions
{
    using System;
    using FluentValidation.AspNetCore;
    using Microsoft.Extensions.DependencyInjection;

    public static class ValidatorExtension
    {

        public static IMvcBuilder AddDtoValidation(this IMvcBuilder builder, Type startupType)
        {
            builder.AddFluentValidation(fvc =>
                fvc.RegisterValidatorsFromAssemblyContaining(startupType));


            return builder;
        }
    }
}
