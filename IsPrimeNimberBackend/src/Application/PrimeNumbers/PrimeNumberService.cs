using Domain.Dtos;
using Infrastructure;

namespace Application.PrimeNumbers;

public class PrimeNumberService : IPrimeNumberService
{
    private readonly IPrimeNumberCalculator _primeNumberCalculator;

    public PrimeNumberService(IPrimeNumberCalculator primeNumberCalculator)
    {
        _primeNumberCalculator = primeNumberCalculator;
    }

    public IsPrimeDto IsPrime(int number)
    {
        return new IsPrimeDto()
        {
            IsPrime = _primeNumberCalculator.IsPrime(number),
            Number = number
        };
    }
}
