using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Patikadev_RestfulApi.Context;
using Patikadev_RestfulApi.Models;

namespace Patikadev_RestfulApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly AppDbContext _context;
    public ProductController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProduct()
    {
        var products = await _context.Products.ToListAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(int id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
        if (product is null)
            return NotFound(new { message = "Product not found" });

        return Ok(product);
    }

    [HttpGet("list")]
    public IActionResult List([FromQuery] string? name)
    {
        var products = _context.Products.AsQueryable();

        if (!string.IsNullOrEmpty(name))
            products = products.Where(p => p.Name.Contains(name));

        return Ok(products.OrderBy(p => p.Name).ToList());
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Product product)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return StatusCode(201, product);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Product updatedProduct)
    {
        var product = await _context.Products.FindAsync(id);
        if (product is null)
            return NotFound();

        product.Name = updatedProduct.Name;
        product.Description = updatedProduct.Description;
        product.Price = updatedProduct.Price;
        product.Stock = updatedProduct.Stock;
        product.IsAvailable = updatedProduct.IsAvailable;
     
        await _context.SaveChangesAsync();
        return Ok(product);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product is null)
            return NotFound();

        product.IsAvailable = false;
        await _context.SaveChangesAsync();
        return Ok(new { message = "Deleted successfully" });
    }
}
