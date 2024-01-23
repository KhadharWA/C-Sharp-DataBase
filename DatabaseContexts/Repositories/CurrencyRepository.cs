



using Shared.Interfaces;
using Shared.Entities;
using Shared.Contexts;

namespace Shared.Repositories;

public class CurrencyRepository(DataContext context, IErrorLogger logger) : BaseRepository<CurrencyEntity, DataContext>(context, logger), ICurrencyRepository
{
    private readonly DataContext _context = context;
    private readonly IErrorLogger _logger = logger;
}
