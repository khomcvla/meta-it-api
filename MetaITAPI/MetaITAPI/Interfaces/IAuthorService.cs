using MetaITAPI.Utils.Responses;

namespace MetaITAPI.Interfaces;

public interface IAuthorService
{
    Task<ServiceResponse> GetAll();
}
