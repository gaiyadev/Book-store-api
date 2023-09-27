using BookstoreAPI.DTOs;
using BookstoreAPI.Repositories.Product;

namespace BookstoreAPI.Services.Product;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    
    public async Task<Models.Product> CreateProduct(CreateProductDto createProductDto, int userId)
    {
        return await _productRepository.CreateProduct(createProductDto, userId);
    }

    public async Task<List<Models.Product>> GetProducts()
    {
        return await _productRepository.GetProducts();
    }

    public async Task<Models.Product> GetProduct(int productId)
    {
        return await _productRepository.GetProduct(productId);
    }

    public async Task<Models.Product> DeleteProduct(int productId)
    {
        return await _productRepository.DeleteProduct(productId);
    }
}