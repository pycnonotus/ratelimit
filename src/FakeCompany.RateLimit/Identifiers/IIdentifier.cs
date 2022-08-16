using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Microsoft.AspNetCore.Http;

namespace FakeCompany.RateLimit.Identifiers;

public interface IIdentifier
{
    public string Identify(HttpContext actionContext);
}
