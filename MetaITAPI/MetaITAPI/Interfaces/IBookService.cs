using MetaITAPI.Common.Responses;
using MetaITAPI.Dtos;

namespace MetaITAPI.Interfaces;

public interface IBookService
{
    Task<ServiceResponse> GetAll();
    Task<ServiceResponse> Get(long id);
    Task<ServiceResponse> Add(BookPostDto book);
    Task<ServiceResponse> Update(long id, BookPutDto book);
    Task<ServiceResponse> Delete(long id);
}
