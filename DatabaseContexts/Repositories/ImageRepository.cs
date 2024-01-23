

using Shared.Entities;

using Shared.Interfaces;
using Shared.Contexts;

namespace Shared.Repositories;

public class ImageRepository(DataContext context, IErrorLogger logger) : BaseRepository<ImageEntity, DataContext>(context, logger), IImageRepository
{
    private readonly DataContext _context = context;
    private readonly IErrorLogger _logger = logger;
}
