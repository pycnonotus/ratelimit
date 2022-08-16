using System;

namespace FakeCompany.RateLimit.Factories;

public interface IErrorMessageFactory
{
    public object CrateError(DateTime blockedUntil);
}
