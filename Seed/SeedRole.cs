using BookstoreAPI.Models;

namespace BookstoreAPI.Seed;

public static class SeedRole
{
    public static List<Role> Roles { get; } = new()
    {
        new Role() { Id = 1, Name = "Admin"},
        new Role() { Id = 2, Name = "Author" },
        new Role() { Id = 3, Name = "User" },

    };
}