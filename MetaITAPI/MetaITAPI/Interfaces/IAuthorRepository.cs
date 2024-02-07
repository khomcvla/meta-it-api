using MetaITAPI.Entities;

namespace MetaITAPI.Interfaces;

public interface IAuthorRepository
{
    Task<List<Author>> GetAll();
}
