using BookstoreAPI.DTOs;
using BookstoreAPI.Pagination;

namespace BookstoreAPI.Repositories.Product;

public interface IProductRepository
{
    Task<Models.Product> CreateProduct( CreateProductDto createProductDto, int userId);
    
    Task<PagedResult<Models.Product>> GetProducts(int page, int itemsPerPage, string search);
    
    Task<Models.Product> GetProduct(int productId);
    
    Task<Models.Product> DeleteProduct(int productId);
}