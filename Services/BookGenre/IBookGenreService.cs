using BookstoreAPI.DTOs;

namespace BookstoreAPI.Services.BookGenre;

public interface IBookGenreService
{
    Task<Models.BookGenre> CreateGenres( CreateGenreDto createGenreDto);

    Task<List<Models.BookGenre>> GetGenres();
    
    Task<Models.BookGenre> GetGenre(int genreId);
    
    Task<Models.BookGenre> UpdateGenres( CreateGenreDto createGenreDto, int genreId);
    
    Task<Models.BookGenre> DeleteGenre(int genreId);
    
}