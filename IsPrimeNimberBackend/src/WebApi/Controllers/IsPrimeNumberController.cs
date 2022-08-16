using FakeCompany.RateLimit.Attributes;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;


[ApiController]

[Route("api/is-prime-number")]
public class IsPrimeNumberController : ControllerBase
{

    private readonly IPrimeNumberService _primeNumberService;


    public IsPrimeNumberController(IPrimeNumberService primeNumberService)
    {
        _primeNumberService = primeNumberService;
    }

    [HttpGet("{number:int}")]
    [RateLimited]
    public IActionResult IsPrime(int number)
    {
        return Ok(_primeNumberService.IsPrime(number));
    }
}
