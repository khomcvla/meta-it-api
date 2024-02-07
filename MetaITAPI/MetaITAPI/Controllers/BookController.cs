using MetaITAPI.Common.Responses;
using MetaITAPI.Dtos;
using MetaITAPI.Entities;
using MetaITAPI.Interfaces;
using MetaITAPI.Utils.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace MetaITAPI.Controllers;

[ApiController]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;
    private readonly ILogger<BookController> _logger;

    public BookController(
        IBookService bookService,
        ILogger<BookController> logger)
    {
        _bookService = bookService;
        _logger = logger;
    }

    //GET
    [HttpGet("api/books")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<BookGetDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> GetAllBooks()
    {
        var response = await _bookService.GetAll();
        return StatusCode(response.StatusCode, response.Value);
    }

    //GET
    [HttpGet("api/books/{bookId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookGetDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> GetBook([FromRoute] int bookId)
    {
        var response = await _bookService.GetById(bookId);
        return StatusCode(response.StatusCode, response.Value);
    }

    //POST
    [HttpPost("api/books")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Book))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> CreateBook([FromBody] BookPostDto bookDto)
    {
        var response = await _bookService.Add(bookDto);
        return StatusCode(response.StatusCode, response.Value);
    }

    //PATCH
    [HttpPatch("api/books/{bookId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Book))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> UpdateBook(
        [FromRoute] int bookId,
        [FromBody] BookPatchDto bookDto)
    {
        if (bookDto.AuthorId is null && bookDto.Title is null)
            throw new InvalidInputException();

        var response = await _bookService.Update(bookId, bookDto);
        return StatusCode(response.StatusCode, response.Value);
    }

    //DELETE
    [HttpDelete("api/books/{bookId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> DeleteBook([FromRoute] int bookId)
    {
        var response = await _bookService.Delete(bookId);
        return StatusCode(response.StatusCode, response.Value);
    }
}
