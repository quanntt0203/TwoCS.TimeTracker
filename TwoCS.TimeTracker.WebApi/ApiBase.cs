
namespace TwoCS.TimeTracker.WebApi
{
    using System;
    using System.Net.Http;
    using FluentValidation;
    using FluentValidation.Attributes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using TwoCS.TimeTracker.Core.Factories;
    using TwoCS.TimeTracker.Core.Results;
    using TwoCS.TimeTracker.Core.Settings;
    using TwoCS.TimeTracker.Dto;

    [Authorize(AuthenticationSchemes = OAuthSetting.AUTHENTICATION_SCHEME)]
    [Route("api"), Produces("application/json")]
    public class ApiBase : Controller
    {
        protected HttpClient client;

        protected virtual ValidationDto CheckValidation<TDto>(TDto model)
        {
            if (model == null)
            {
                return new ValidationDto(false);

            }

            var attrs = typeof(TDto).GetCustomAttributes(true);

            ValidatorAttribute attr = (ValidatorAttribute)Attribute.GetCustomAttribute(typeof(TDto), typeof(ValidatorAttribute));

            if (attr != null)
            {
                IValidator validator = ResolverFactory.CreateInstance<IValidator>(attr.ValidatorType.AssemblyQualifiedName);

                return new ValidationDto(validator.Validate(model));
            }
            else
            {
                return new ValidationDto();
            }
        }

        protected OkObjectResult ResultOk()
        {
            return Ok(new ApiResultOk());
        }

        protected OkObjectResult ResultOk(object result)
        {
            return Ok(new ApiResultOk(result));
        }

        protected OkObjectResult ResultOk(string message, object result)
        {
            return Ok(new ApiResultOk(message, result));
        }
    }
}
