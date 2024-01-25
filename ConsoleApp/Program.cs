using ConsoleApp.Service;
using Shared.Contexts;
using Shared.Interfaces;
using Shared.Repositories;
using Shared.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared.Utils;


var app = Host.CreateDefaultBuilder().ConfigureServices(services =>
{


    services.AddSingleton<IErrorLogger>(new ErrorLogger(@"c:\Utb-Projects\DataBase\log.txt"));

    services.AddScoped<ICurrencyRepository, CurrencyRepository>();
    services.AddScoped<ICategoryRepository, CategoryRepository>();
    services.AddScoped<IImageRepository, ImageRepository>();
    services.AddScoped<IProductImageRepository, ProductImageRepository>();
    services.AddScoped<IProductPriceRepository, ProductPriceRepository>();
    services.AddScoped<IProductRepository, ProductRepository>();
    services.AddScoped<IManufacturesRepository, ManufacturesRepository>();

    services.AddScoped<CurrencyService>();
    services.AddScoped<ImageService>();
    services.AddScoped<ManufacturesService>();
    services.AddScoped<ProductService>();
    services.AddScoped<CategoryService>();

    services.AddDbContext<DataContext>(x => x.UseSqlServer(@"Data Source=LocalHost;Initial Catalog=Product;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"));

     services.AddSingleton<MenuService>();


}).Build();

app.Start();

var menuService = app.Services.GetRequiredService<MenuService>();
string articleNumber = menuService.CreateProductMenu();

if (!string.IsNullOrEmpty(articleNumber))
{
    menuService.AddImageToProductMenu(articleNumber); 
    menuService.AddProductPriceMenu(articleNumber); 
} 