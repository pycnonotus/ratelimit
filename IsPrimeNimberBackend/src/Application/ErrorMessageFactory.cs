using Application.Extensions;
using Domain.Dtos;
using FakeCompany.RateLimit.Factories;

namespace Application;

public class ErrorMessageFactory : IErrorMessageFactory
{

    public object CrateError(DateTime blockedUntil)
    {
        var remainingTime =  blockedUntil - DateTime.UtcNow ;
        return new ErrorDto()
        {
            ErrorMessage =
                $"You have exceeded your call limits. Remaining time until restriction removed is {remainingTime}"
        };

    }
}
