using Microsoft.AspNetCore.Mvc;

namespace ProductProject.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{

    public ProductController()
    {
    }

    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {

    }
}
