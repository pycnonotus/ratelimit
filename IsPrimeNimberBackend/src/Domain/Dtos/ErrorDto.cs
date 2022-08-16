namespace Domain.Dtos;

public class ErrorDto : IApiResponse
{
    public string ErrorMessage { get; set; }
    public bool HasError => true;
}
