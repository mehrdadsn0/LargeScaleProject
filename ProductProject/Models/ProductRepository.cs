namespace ProductProject.Models;

public class ProductRepository
{
    private readonly ProductsContext _context;

    public ProductRepository(ProductsContext context)
    {
        _context = context;
    }

    public (bool, Product) Add(Product product)
    {
        try
        {
            _context.Add(product);
            _context.SaveChanges();
            return (true, product);
        }
        catch
        {
            return (false, product);
        }
    }

    public (bool, Product?) Edit(Product product)
    {
        Product? existingProduct = _context.Products.Find(product.Id);
        if (existingProduct == null)
        {
            return (false, existingProduct);
        }
        if (existingProduct.Title != product.Title)
        {
            existingProduct.Title = product.Title;
        }
        if (existingProduct.Price != product.Price)
        {
            existingProduct.Price = product.Price;
        }
        _context.SaveChanges();
        return (true, existingProduct);
    }

    public Product? GetById(int id)
    {
        return _context.Products.Find(id);
    }

    public IEnumerable<Product> GetAll()
    {
        return _context.Products;
    }
}