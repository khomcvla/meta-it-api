using AutoMapper;
using MetaITAPI.Common.Responses;
using MetaITAPI.Dtos;
using MetaITAPI.Entities;
using MetaITAPI.Interfaces;
using MetaITAPI.Utils.Constants;

namespace MetaITAPI.Services;

public class BookService : IBookService
{
    private readonly IMapper _mapper;
    private readonly IBookRepository _bookRepository;

    public BookService(
        IMapper mapper,
        IBookRepository bookRepository)
    {
        _mapper = mapper;
        _bookRepository = bookRepository;
    }

    public async Task<ServiceResponse> GetAll()
    {
        var books = await _bookRepository.GetAll();
        var result = _mapper.Map<List<BookGetDto>>(books);
        return new ServiceResponse(StatusCodes.Status200OK, result);
    }

    public async Task<ServiceResponse> GetById(int bookId)
    {
        var book = await _bookRepository.GetById(bookId);
        if (book is null)
            return new ServiceResponse(StatusCodes.Status404NotFound, StatusMessages.NotExist);

        var result = _mapper.Map<BookGetDto>(book);
        return new ServiceResponse(StatusCodes.Status200OK, result);
    }

    public async Task<ServiceResponse> Add(BookPostDto bookDto)
    {
        if (await _bookRepository.IsBookExist(bookDto.AuthorId, bookDto.Title))
            return new ServiceResponse(StatusCodes.Status409Conflict, StatusMessages.AlreadyExist);

        var book = _mapper.Map<Book>(bookDto);
        var result = await _bookRepository.Add(book);
        if (result == false) throw new Exception();

        return new ServiceResponse(StatusCodes.Status201Created, book);
    }

    public async Task<ServiceResponse> Update(int bookId, BookPatchDto bookDto)
    {
        var book = await _bookRepository.GetById(bookId);
        if (book is null)
            return new ServiceResponse(StatusCodes.Status404NotFound, StatusMessages.NotExist);

        _mapper.Map(bookDto, book);

        var result = await _bookRepository.Update(book);
        if (result == false) throw new Exception();

        return new ServiceResponse(StatusCodes.Status200OK, book);
    }

    public async Task<ServiceResponse> Delete(int bookId)
    {
        if (!await _bookRepository.IsBookIdExist(bookId))
            return new ServiceResponse(StatusCodes.Status404NotFound, StatusMessages.NotExist);

        var book = await _bookRepository.GetById(bookId);
        var result = await _bookRepository.Delete(book!);
        if (result == false) throw new Exception();

        return new ServiceResponse(StatusCodes.Status200OK, StatusMessages.StatusSuccess);
    }
}
