

using Shared.Entities;

using Shared.Interfaces;
using Shared.Contexts;

namespace Shared.Repositories;

public class ManufacturesRepository(DataContext context, IErrorLogger logger) : BaseRepository<ManufacturesEntity, DataContext>(context, logger), IManufacturesRepository
{
    private readonly DataContext _context = context;
    private readonly IErrorLogger _logger = logger;
}
