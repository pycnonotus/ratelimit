namespace Domain.Dtos;

public class IsPrimeDto : IApiResponse
{
    public bool IsPrime { get; set; } 
    public int Number { get; set; }

    public bool HasError => false;
}
