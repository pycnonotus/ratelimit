namespace Infrastructure;

public interface IPrimeNumberCalculator
{
    // public bool IsPrime<T>(T number) where T : IComparable<T> ;
     bool IsPrime(int number);
}