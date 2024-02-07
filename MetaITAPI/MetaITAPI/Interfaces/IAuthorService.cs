using MetaITAPI.Common.Responses;

namespace MetaITAPI.Interfaces;

public interface IAuthorService
{
    Task<ServiceResponse> GetAll();
}
