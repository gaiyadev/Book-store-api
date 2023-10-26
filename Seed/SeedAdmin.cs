using BookstoreAPI.Models;

namespace BookstoreAPI.Seed;

public class SeedAdmin
{
    public static List<Admin> Admin { get; } = new()
    {
        new Admin() { Id = 1, Username = "admin", Email = "store.admin@bookstore.com", Password = "password", RoleId = 1  },
        new Admin()  { Id = 2, Username = "admin2", Email = "store.admin2@bookstore.com", Password = "password", RoleId = 1  },
        new Admin()  { Id = 3, Username = "admin3", Email = "store.admin3@bookstore.com" , Password = "password", RoleId = 1 },
    };
}