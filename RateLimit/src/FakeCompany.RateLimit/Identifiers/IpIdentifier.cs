using System;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Microsoft.AspNetCore.Http;

namespace FakeCompany.RateLimit.Identifiers;

internal class IpIdentifier : IIdentifier 
{
    public string Identify(HttpContext context)
    {
        return context.Connection!.RemoteIpAddress!.ToString();
    }
}
