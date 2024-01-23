


using Shared.Contexts;
using Shared.Entities;
using Shared.Interfaces;

namespace Shared.Repositories;

public class ProductImageRepository(DataContext context, IErrorLogger logger) : BaseRepository<ProductCurrencyEntity, DataContext>(context, logger), IProductImageRepository
{
    private readonly DataContext _context = context;
    private readonly IErrorLogger _logger = logger;
}
