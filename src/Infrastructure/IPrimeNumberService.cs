using Domain.Dtos;

namespace Infrastructure;

public interface IPrimeNumberService
{
    IsPrimeDto IsPrime(int number);
}
