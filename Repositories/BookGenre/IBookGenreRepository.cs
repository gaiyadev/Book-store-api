using BookstoreAPI.DTOs;

namespace BookstoreAPI.Repositories.BookGenre;

public interface IBookGenreRepository
{
    Task<Models.BookGenre> CreateGenres(CreateGenreDto createGenreDto);

    Task<List<Models.BookGenre>> GetGenres();
    
    Task<Models.BookGenre> GetGenre(int id);
    
    Task<Models.BookGenre> DeleteGenres(int id);

}