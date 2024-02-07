using MetaITAPI.Dtos;
using MetaITAPI.Utils.Responses;

namespace MetaITAPI.Interfaces;

public interface IBookService
{
    Task<ServiceResponse> GetAll();
    Task<ServiceResponse> GetById(int bookId);
    Task<ServiceResponse> Add(BookPostDto book);
    Task<ServiceResponse> Update(int bookId, BookPatchDto book);
    Task<ServiceResponse> Delete(int bookId);
}
