using BookstoreAPI.Models;

namespace BookstoreAPI.Seed;

public static class SeedUser
{
    public static List<User> Genres { get; } = new()
    {
        new User() { Id = 1, Username = "gaiya", Email = "gaiyaobed@gmail.com", Password = "Pass123" },
        new User() { Id = 1, Username = "gaiyadev", Email = "gaiya.obed16@gmail.com" },
        new User() { Id = 1, Username = "admin", Email = "admin@gmail.com" },

    };
}