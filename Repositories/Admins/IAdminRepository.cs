using BookstoreAPI.DTOs;

namespace BookstoreAPI.Repositories.Admins;

public interface IAdminRepository
{
    Task<Models.Admin> AdminSignIn(SigninDto signinDto);

}