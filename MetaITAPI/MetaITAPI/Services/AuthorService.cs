using AutoMapper;
using MetaITAPI.Common.Responses;
using MetaITAPI.Dtos;
using MetaITAPI.Interfaces;

namespace MetaITAPI.Services;

public class AuthorService : IAuthorService
{
    private readonly IMapper _mapper;
    private readonly IAuthorRepository _authorRepository;

    public AuthorService(
        IMapper mapper,
        IAuthorRepository authorRepository)
    {
        _mapper = mapper;
        _authorRepository = authorRepository;
    }

    public async Task<ServiceResponse> GetAll()
    {
        var authors = await _authorRepository.GetAll();
        var result = _mapper.Map<List<AuthorGetDto>>(authors);
        return new ServiceResponse(StatusCodes.Status200OK, result);
    }
}
