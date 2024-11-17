using Microsoft.AspNetCore.Mvc;
using ProductProject.Dtos;
using ProductProject.Models;

namespace ProductProject.Controllers;

[ApiController]
[Route("product")]
public class ProductController : ControllerBase
{
    private readonly ProductRepository _productRepository;
    public ProductController(ProductRepository productRepository = null)
    {
        _productRepository = productRepository;
    }

    [HttpGet(":id")]
    public ActionResult<Product> GetProduct(int id)
    {
        Product? product = _productRepository.GetById(id);
        if (product == null)
        {
            return NotFound();
        }
        else
        {
            return Ok(product);
        }
    }
    [HttpGet("search")]
    public IEnumerable<Product> SearchProduct(string query)
    {
        return _productRepository.Search(query);
    }

    [HttpGet]
    public IEnumerable<Product> GetProducts()
    {
        return _productRepository.GetAll();
    }

    [HttpPost]
    public ActionResult<Product> AddProduct(AddProductDto input)
    {
        Product newProduct = new(input.Title, input.Price);
        var (res, product) = _productRepository.Add(newProduct);
        if (!res)
        {
            return BadRequest(product);
        }
        else
        {
            return Ok(product);
        }
    }

    [HttpPut]
    public ActionResult<Product> EditProduct(Product input)
    {
        var (res, product) = _productRepository.Edit(input);
        if(!res)
        {
            return BadRequest(product);
        }
        else{
            return Ok(product);
        }
    }


}
