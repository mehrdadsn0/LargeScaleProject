using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using ProductProject.Models;
using Xunit;

namespace ProductProject.Tests;

public class ProductsTests
{
    [Fact]
    public void Get_Products()
    {
        // Arrange
        // var context = new Mock
        var options = new DbContextOptionsBuilder<ProductsContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase") // Name the in-memory database
            .Options;
        var context = new ProductsContext(options);
        var mockSet = new Mock<DbSet<Product>>();
        var products = new List<Product>
        {
            new Product { Id = 1, Title = "moz", Price = 5 },
            new Product { Id = 2, Title = "Alma", Price = 2 }
        };
        
        context.Products.AddRange(products);
        context.SaveChanges();

        var repository = new ProductRepository(context);

        // Act
        var result = repository.GetAll();

        // Assert
        Assert.Equal(2, result.Count());
        Assert.Contains(result, u => u.Title == "Alma");
    }
}