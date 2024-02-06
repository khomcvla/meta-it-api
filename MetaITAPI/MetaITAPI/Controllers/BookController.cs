using MetaITAPI.Dtos;
using MetaITAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

    //--------------------------- CREATE ---------------------------
    [HttpPost("api/books")]
    public async Task<IActionResult> CreateBook([FromBody] BookPostDto book)
    {
        var response = await _bookService.Add(book);
        return StatusCode(response.StatusCode, response.Value);
    }

    //---------------------------  READ  ---------------------------
    // ROUTE VERSION
    [HttpGet("api/books/{id:long}")]
    public async Task<IActionResult> GetBook([FromRoute] long id)
    {
        var response = await _bookService.Get(id);
        return StatusCode(response.StatusCode, response.Value);
    }

    // QUERY VERSION
    [HttpGet("api/books")]
    public async Task<IActionResult> GetBooks([FromQuery] long? id)
    {
        var response = id is null
            ? await _bookService.GetAll()
            : await _bookService.Get(id.Value);
        return StatusCode(response.StatusCode, response.Value);
    }

    //--------------------------- UPDATE ---------------------------
    [HttpPut("api/books/{id:long}")]
    public async Task<IActionResult> UpdateBook(
        [FromRoute] long id,
        [FromBody] BookPutDto book)
    {
        var response = await _bookService.Update(id, book);
        return StatusCode(response.StatusCode, response.Value);
    }

    //--------------------------- DELETE ---------------------------
    [HttpDelete("api/books/{id:long}")]
    public async Task<IActionResult> UpdateBook([FromRoute] long id)
    {
        var response = await _bookService.Delete(id);
        return StatusCode(response.StatusCode, response.Value);
    }
}
