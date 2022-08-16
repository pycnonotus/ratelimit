using System.Linq;
using FakeCompany.RateLimit.Attributes;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace FakeCompany.RateLimit.Extensions;

internal static class ControllerActionDescriptorExtensions
{
    internal static RateLimitedAttribute? GetRateLimitedAttribute(this ControllerActionDescriptor controllerActionDescriptor)
    {
        return (RateLimitedAttribute)
            controllerActionDescriptor
            .MethodInfo
            .GetCustomAttributes(true)
            .SingleOrDefault(w => w.GetType() == typeof(RateLimitedAttribute));
    }
}
