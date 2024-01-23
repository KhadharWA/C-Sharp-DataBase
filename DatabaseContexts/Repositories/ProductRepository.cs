




using Shared.Contexts;
using Shared.Entities;
using Shared.Interfaces;

namespace Shared.Repositories;

public class ProductRepository(DataContext context, IErrorLogger logger) : BaseRepository<ProductEntity, DataContext>(context, logger), IProductRepository
{
    private readonly DataContext _context = context;
    private readonly IErrorLogger _logger = logger;
}
