using BookstoreAPI.Models;

namespace BookstoreAPI.Seed;

public static class SeedBookGenre
{
    public static List<BookGenre> Genres { get; } = new List<BookGenre>
    {
        new BookGenre { Id = 1, Name = "Mystery" },
        new BookGenre { Id = 2, Name = "Thriller" },
        new BookGenre { Id = 3, Name = "Science Fiction" },
        new BookGenre { Id = 4, Name = "Fantasy" },
        new BookGenre { Id = 5, Name = "Romance" },
        new BookGenre { Id = 6, Name = "Historical Fiction" },
        new BookGenre { Id = 7, Name = "Adventure" },
        new BookGenre { Id = 8, Name = "Biography" },
        new BookGenre { Id = 9, Name = "Autobiography" },
        new BookGenre { Id = 10, Name = "Non-Fiction" },
        new BookGenre { Id = 11, Name = "Self-Help" },
        new BookGenre { Id = 12, Name = "Horror" },
        new BookGenre { Id = 13, Name = "Comedy" },
        new BookGenre { Id = 14, Name = "Drama" },
        new BookGenre { Id = 15, Name = "Travel" },
        new BookGenre { Id = 16, Name = "Children's" },
        new BookGenre { Id = 17, Name = "Cookbook" },
        new BookGenre { Id = 18, Name = "True Crime" },
        new BookGenre { Id = 19, Name = "Philosophy" },
        new BookGenre { Id = 20, Name = "Poetry" },
    };
}