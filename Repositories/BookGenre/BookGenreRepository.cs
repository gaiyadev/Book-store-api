using System.Net;
using BookstoreAPI.CustomExceptions.Exceptions;
using BookstoreAPI.Data;
using BookstoreAPI.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BookstoreAPI.Repositories.BookGenre;

public class BookGenreRepository : IBookGenreRepository
{
    
    private readonly ApplicationDbContext _context;
    private readonly ILogger<BookGenreRepository> _logger;

    public BookGenreRepository(ApplicationDbContext context, ILogger<BookGenreRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Models.BookGenre> CreateGenres( CreateGenreDto createGenreDto)
    {
        try
        {
            var bookGenre = new Models.BookGenre()
            {
                Name = createGenreDto.Name
            };
            await _context.AddAsync(bookGenre);
            await _context.SaveChangesAsync();
            return bookGenre;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw new InternalServerException(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<List<Models.BookGenre>> GetGenres()
    {
        try
        {
          return await _context.BookGenres
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw new InternalServerException(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Models.BookGenre> GetGenre(int genreId)
    {
        var findGenre = await _context.BookGenres.FindAsync(genreId);

        if (findGenre == null)
        {
            throw new NotFoundException($"Genre with {genreId} not found", HttpStatusCode.NotFound);
        }

        return findGenre;
    }


    public Task<Models.BookGenre> DeleteGenres(int id)
    {
        throw new NotImplementedException();
    }
}