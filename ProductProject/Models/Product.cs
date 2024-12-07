namespace ProductProject.Models;
using Microsoft.EntityFrameworkCore;

[Index(nameof(Title), IsUnique = true)]
public class Product
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public decimal Price { get; set; }

    public Product()
    {

    }
    public Product(string title, decimal price)
    {
        Title = title;
        Price = price;
    }
}