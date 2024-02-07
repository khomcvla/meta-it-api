using System.Reflection;
using MetaITAPI.Dtos;
using MetaITAPI.Interfaces;
using MetaITAPI.Utils.Constants;
using MetaITAPI.Utils.Responses;
using Microsoft.AspNetCore.Mvc;

namespace MetaITAPI.Controllers;

[ApiController]
public class AuthorController : ControllerBase
{
    private readonly IAuthorService _authorService;
    private readonly ILogger<AuthorController> _logger;

    public AuthorController(
        IAuthorService authorService,
        ILogger<AuthorController> logger)
    {
        _authorService = authorService;
        _logger = logger;
    }

    //GET
    [HttpGet("api/authors")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<AuthorGetDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> GetAllAuthors()
    {
        _logger.LogInformation("Getting all authors.");

        var response = await _authorService.GetAll();
        return StatusCode(response.StatusCode, response.Value);
    }
}
