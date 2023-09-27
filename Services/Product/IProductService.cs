using BookstoreAPI.DTOs;

namespace BookstoreAPI.Services.Product;

public interface IProductService
{
    Task<Models.Product> CreateProduct( CreateProductDto createProductDto, int userId);
    
    Task<List<Models.Product>> GetProducts();
    
    Task<Models.Product> GetProduct(int productId);
    
    Task<Models.Product> DeleteProduct(int productId);




}