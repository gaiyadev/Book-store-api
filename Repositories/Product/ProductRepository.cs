using System.Net;
using BookstoreAPI.CustomExceptions.Exceptions;
using BookstoreAPI.Data;
using BookstoreAPI.DTOs;
using BookstoreAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace BookstoreAPI.Repositories.Product;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ProductRepository> _logger;
    private readonly SlugGenerator _slugGenerator;
    private readonly AWSS3Service _awss3Service;

    public ProductRepository(ApplicationDbContext context,
        ILogger<ProductRepository> logger, SlugGenerator slugGenerator, AWSS3Service awss3Service)
    {
        _context = context;
        _logger = logger;
        _slugGenerator = slugGenerator;
        _awss3Service = awss3Service;
    }    
    public async Task<Models.Product> CreateProduct(CreateProductDto createProductDto, int userId)
    {
        if (!createProductDto.File.ContentType.Equals("image/jpeg") &&
            !createProductDto.File.ContentType.Equals("image/png"))
        {
            throw new BadRequestException("Only JPEG and PNG images are allowed.", HttpStatusCode.BadRequest);
        }
        
        if (createProductDto.File.Length > 1024 * 1024)
        {
            throw new BadRequestException("The file is too large", HttpStatusCode.BadRequest);
        }

        try
        {
            var openReadStream = createProductDto.File.OpenReadStream();
            var uploadUrl = await _awss3Service.UploadFileAsync(openReadStream,"products",createProductDto.File.FileName);
            string slugUrl =  _slugGenerator.GenerateSlug(createProductDto.Title);
            
            var product = new Models.Product()
            {
                Title = createProductDto.Title,
                Author = createProductDto.Author,
                Price = createProductDto.Price,
                UserId = userId,
                BookGenreId = createProductDto.GenreId,
                ProductSlug = slugUrl,
                ProductUrl = uploadUrl
            };
            await _context.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw new InternalServerException(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<List<Models.Product>> GetProducts()
    {
        try
        {
            return await _context.Products
                .OrderByDescending(product => product.Id)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw new InternalServerException(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Models.Product> GetProduct(int productId)
    {
        var findProduct = await _context.Products.FindAsync(productId);
        
        if (findProduct == null)
        {
            throw new NotFoundException($"Genre with {productId} not found", HttpStatusCode.NotFound);
        }
        return findProduct;
    }

    public async Task<Models.Product> DeleteProduct(int productId)
    {
        var findProduct = await GetProduct(productId);
        if (findProduct == null)
        {
            throw new NotFoundException($"Genre with {productId} not found", HttpStatusCode.NotFound);
        }
        _context.Products.Remove(findProduct);
        await _context.SaveChangesAsync();
        return findProduct;
    }
}