using BookstoreAPI.DTOs;
using BookstoreAPI.Pagination;
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

    public async Task<PagedResult<Models.Product>> GetProducts(int page, int itemsPerPage, string search)
    {
        return await _productRepository.GetProducts(page, itemsPerPage, search);
    }

    public async Task<Models.Product> GetProduct(int productId)
    {
        return await _productRepository.GetProduct(productId);
    }

    public async Task<Models.Product> DeleteProduct(int productId)
    {
        return await _productRepository.DeleteProduct(productId);
    }

    public async Task<PagedResult<Models.Product>> GetVendorProducts(int vendorId, int page, int itemsPerPage, string search)
    {
        return await _productRepository.GetVendorProducts(vendorId, page, itemsPerPage, search);
    }
}