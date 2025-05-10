using Patikadev_RestfulApi.Domain;


namespace Patikadev_RestfulApi.Interfaces;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<Product?> GetProductByIdAsync(Guid id);
    Task<Product> CreateProductAsync(Product product);
    Task<Product?> UpdateProductAsync(Guid id, Product updatedProduct);
    Task<bool> DeleteProductAsync(Guid id);
}
