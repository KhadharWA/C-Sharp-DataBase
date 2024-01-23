



using Shared.Interfaces;
using Shared.Entities;
using Shared.Contexts;


namespace Shared.Repositories;

public class CategoryRepository(DataContext context, IErrorLogger logger) : BaseRepository<CategoryEntity, DataContext>(context, logger), ICategoryRepository
{
    private readonly DataContext _context = context;
    private readonly IErrorLogger _logger = logger;
}
