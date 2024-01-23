
using Microsoft.EntityFrameworkCore;
using Shared.Contexts;
using Shared.Entities;
using Shared.Interfaces;


namespace Shared.Repositories;

public class ProductPriceRepository(DataContext context, IErrorLogger logger) : BaseRepository<ProductPriceEntity, DataContext>(context, logger), IProductPriceRepository
{
    private readonly DataContext _context = context;
    private readonly IErrorLogger _logger = logger;


    
}
