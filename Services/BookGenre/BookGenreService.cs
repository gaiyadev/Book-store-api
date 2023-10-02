using BookstoreAPI.DTOs;
using BookstoreAPI.Repositories.BookGenre;

namespace BookstoreAPI.Services.BookGenre;

public class BookGenreService : IBookGenreService
{
    private readonly IBookGenreRepository _bookGenreRepository;

    public BookGenreService(IBookGenreRepository bookGenreRepository)
    {
        _bookGenreRepository = bookGenreRepository;
    }

    public async Task<Models.BookGenre> CreateGenres( CreateGenreDto createGenreDto)
    {
        return await _bookGenreRepository.CreateGenres(createGenreDto);
    }

    public async Task<List<Models.BookGenre>> GetGenres()
    {
        return await _bookGenreRepository.GetGenres();
    }

    public async Task<Models.BookGenre> GetGenre(int genreId)
    {
        return await _bookGenreRepository.GetGenre(genreId);
    }

    public async Task<Models.BookGenre> UpdateGenres(CreateGenreDto createGenreDto, int genreId)
    {
        return await _bookGenreRepository.UpdateGenres(createGenreDto, genreId);
    }

    public async Task<Models.BookGenre> DeleteGenre(int genreId)
    {
        return await _bookGenreRepository.DeleteGenre(genreId);
    }
    
}