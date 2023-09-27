using BookstoreAPI.CustomExceptions.Exceptions;
using BookstoreAPI.CustomResponses.Responses;
using BookstoreAPI.DTOs;
using BookstoreAPI.Services.BookGenre;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreAPI.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/bookGenres")]
[ApiVersion("1.0")]
public class BookGenreController : ControllerBase
{
    private readonly IBookGenreService _bookGenreService;

    public BookGenreController(IBookGenreService bookGenreService)
    {
        _bookGenreService = bookGenreService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateGenres([FromBody] CreateGenreDto createGenreDto)
    {
        try
        {
            var genre =  await _bookGenreService.CreateGenres(createGenreDto);
            var apiResponse = new List<object>
            {
                new { id = genre.Id, name = genre.Name }
            };
            return SuccessResponse.HandleCreated("Successfully created", apiResponse);
        }
        catch (ConflictException ex)
        {
            return ApplicationExceptionResponse.HandleConflictException(ex.Message);
        }
        catch (Exception ex)
        {
            return ApplicationExceptionResponse.HandleInternalServerError(ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetGenres()
    {
        try
        {
            var genres = await _bookGenreService.GetGenres();
            return SuccessResponse.HandleOk(
                "Fetched Successfully", 
                genres, null
                );
        }
        catch (InternalServerException ex)
        {
            return ApplicationExceptionResponse.HandleInternalServerError(ex.Message);
        }
        catch (Exception ex)
        {
            return ApplicationExceptionResponse.HandleInternalServerError(ex.Message);
        }
    }
    
    
    [HttpGet("genreId:int")]
    public async Task<IActionResult> GetGenre(int genreId)
    {
        try
        {
            var genres = await _bookGenreService.GetGenre(genreId);
            return SuccessResponse.HandleOk(
                "Fetched Successfully", 
                genres, null
            );
        }
        catch (NotFoundException ex)
        {
            return ApplicationExceptionResponse.HandleNotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return ApplicationExceptionResponse.HandleInternalServerError(ex.Message);
        }
    }


    [HttpPatch("genreId:int")]
    public async Task<IActionResult> UpdateGenres([FromBody] CreateGenreDto createGenreDto, int genreId)
    {
        try
        {
            var genre = await _bookGenreService.UpdateGenres(createGenreDto, genreId);
            var apiResponse = new List<object>
            {
                new { id = genre.Id, name = genre.Name }
            };
            return SuccessResponse.HandleCreated("Successfully updated", apiResponse);
            
        } catch (NotFoundException ex)
        {
            return ApplicationExceptionResponse.HandleNotFound(ex.Message);
        }
        catch (ConflictException ex)
        {
            return ApplicationExceptionResponse.HandleConflictException(ex.Message);
        }
        catch (Exception ex)
        {
            return ApplicationExceptionResponse.HandleInternalServerError(ex.Message);
        }
    }

    [HttpDelete("genreId:int")]
    public async Task<IActionResult> DeleteGenres(int genreId)
    {
        try
        {
             await _bookGenreService.DeleteGenre(genreId);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return ApplicationExceptionResponse.HandleNotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return ApplicationExceptionResponse.HandleInternalServerError(ex.Message);
        }
    }
}