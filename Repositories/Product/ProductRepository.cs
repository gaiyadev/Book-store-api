using System.Net;
using BookstoreAPI.CustomExceptions.Exceptions;
using BookstoreAPI.Data;
using BookstoreAPI.DTOs;
using BookstoreAPI.Pagination;
using BookstoreAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace BookstoreAPI.Repositories.Product;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ProductRepository> _logger;
    private readonly SlugGenerator _slugGenerator;
    private readonly Awss3Service _awss3Service;

    public ProductRepository(ApplicationDbContext context,
        ILogger<ProductRepository> logger, SlugGenerator slugGenerator, Awss3Service awss3Service)
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
    
    public async Task<PagedResult<Models.Product>> GetProducts(int page, int itemsPerPage, string search)
    {
        try
        {
            // Base query without search term
            var query = _context.Products
                .Include(u => u.BookGenre)
                .Include(vendor => vendor.User)
                .ThenInclude(role => role.Role)
                .OrderByDescending(product => product.Id);

            // Apply search filter if a search term is provided
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = (IOrderedQueryable<Models.Product>)query.Where(product => product.Title.Contains(search));
            }

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);

            var products = await query
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToListAsync();

            // Create the pagination metadata
            var meta = new PaginationMetaData
            {
                TotalItems = totalItems,
                ItemCount = products.Count,
                ItemsPerPage = itemsPerPage,
                TotalPages = totalPages,
                CurrentPage = page
            };
            var paginationLinks = new PaginationLinks("http://localhost:5178/api/", page, totalPages, itemsPerPage);

            // Create the paged result
            return new PagedResult<Models.Product>
            {
                Data = products,
                Meta = meta,
                Links = paginationLinks
            };
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

    public async Task<PagedResult<Models.Product>> GetVendorProducts(int vendorId, int page, int itemsPerPage, string search)
    {
        try
        {
            // Base query without search term
            var query = _context.Products
                .Where( vendor => vendor.UserId == vendorId)
                .Include(u => u.BookGenre)
                .Include(vendor => vendor.User)
                .ThenInclude(role => role.Role)
                .OrderByDescending(product => product.Id);

            // Apply search filter if a search term is provided
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = (IOrderedQueryable<Models.Product>)query.Where(product => product.Title.Contains(search));
            }

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);

            var products = await query
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToListAsync();

            // Create the pagination metadata
            var meta = new PaginationMetaData
            {
                TotalItems = totalItems,
                ItemCount = products.Count,
                ItemsPerPage = itemsPerPage,
                TotalPages = totalPages,
                CurrentPage = page
            };
            var paginationLinks = new PaginationLinks("http://localhost:5178/api/", page, totalPages, itemsPerPage);

            // Create the paged result
            return new PagedResult<Models.Product>
            {
                Data = products,
                Meta = meta,
                Links = paginationLinks
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw new InternalServerException(ex.Message, HttpStatusCode.InternalServerError);
        }
    }
}