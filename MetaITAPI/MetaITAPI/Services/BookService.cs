using AutoMapper;
using MetaITAPI.Common.Responses;
using MetaITAPI.Dtos;
using MetaITAPI.Interfaces;

namespace MetaITAPI.Services;

public class BookService : IBookService
{
    public readonly IMapper _mapper;
    private readonly IBookRepository _bookRepository;

    public BookService(
        IMapper mapper,
        IBookRepository bookRepository)
    {
        _mapper = mapper;
        _bookRepository = bookRepository;
    }

    public Task<ServiceResponse> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse> Get(long id)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse> Add(BookPostDto book)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse> Update(long id, BookPutDto book)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse> Delete(long id)
    {
        throw new NotImplementedException();
    }
}
