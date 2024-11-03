namespace ProductProject.Models;
using Microsoft.EntityFrameworkCore;

[Index(nameof(Title), IsUnique = true)]
public class Product
{
    public int Id { get; set;}
    public string Title { get; set;}
    public decimal Price { get; set; }
}