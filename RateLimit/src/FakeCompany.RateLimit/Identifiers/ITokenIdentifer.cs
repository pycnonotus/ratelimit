using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace FakeCompany.RateLimit.Identifiers;

public class ITokenIdentifer : IIdentifier
{

    public string Identify(HttpContext actionContext)
    {
       return actionContext.Request.Headers[HeaderNames.Authorization];
    }
}
