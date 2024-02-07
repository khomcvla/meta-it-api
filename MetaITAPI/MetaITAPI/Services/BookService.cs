using AutoMapper;
using MetaITAPI.Dtos;
using MetaITAPI.Entities;
using MetaITAPI.Interfaces;
using MetaITAPI.Utils.Constants;
using MetaITAPI.Utils.Exceptions;
using MetaITAPI.Utils.Responses;

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
            throw new NotExistException(StatusMessages.BookNotExist);

        var result = _mapper.Map<BookGetDto>(book);
        return new ServiceResponse(StatusCodes.Status200OK, result);
    }

    public async Task<ServiceResponse> Add(BookPostDto bookDto)
    {
        if (!await _bookRepository.IsAuthorIdExist(bookDto.AuthorId))
            throw new NotExistException(StatusMessages.AuthorNotExist);

        if (await _bookRepository.IsBookExist(bookDto.AuthorId, bookDto.Title))
            throw new AlreadyExistException();

        var book = _mapper.Map<Book>(bookDto);
        var result = await _bookRepository.Add(book);
        if (result == false) throw new Exception();

        return new ServiceResponse(StatusCodes.Status201Created, book);
    }

    public async Task<ServiceResponse> Update(int bookId, BookPatchDto bookDto)
    {
        var book = await _bookRepository.GetById(bookId);
        if (book is null)
            throw new NotExistException(StatusMessages.BookNotExist);

        _mapper.Map(bookDto, book);

        var result = await _bookRepository.Update(book);
        if (result == false) throw new Exception();

        return new ServiceResponse(StatusCodes.Status200OK, book);
    }

    public async Task<ServiceResponse> DeleteById(int bookId)
    {
        if (!await _bookRepository.IsBookIdExist(bookId))
            throw new NotExistException(StatusMessages.BookNotExist);

        var book = await _bookRepository.GetById(bookId);
        var result = await _bookRepository.Delete(book!);
        if (result == false) throw new Exception();

        return new ServiceResponse(StatusCodes.Status200OK, StatusMessages.StatusSuccess);
    }
}
