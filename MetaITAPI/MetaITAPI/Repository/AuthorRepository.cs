using MetaITAPI.Data;
using MetaITAPI.Entities;
using MetaITAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MetaITAPI.Repository;

public class AuthorRepository : IAuthorRepository
{
    private readonly AppDbContext _dataContext;

    public AuthorRepository(AppDbContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<List<Author>> GetAll()
    {
        return await _dataContext.Authors.AsNoTracking().Include(a => a.Books).ToListAsync();
    }
}
