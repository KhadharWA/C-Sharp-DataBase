using Shared.Entities;
using Shared.Interfaces;
using Shared.Models;
using Shared.Utils;
using System.Diagnostics;



namespace Shared.Services;

public class ProductService(IProductRepository productRepository, IProductPriceRepository productPriceRepository, IProductImageRepository productImageRepository, CategoryService categoryService, ManufacturesService manufacturesService, IErrorLogger errorLogger, ImageService imageService, CurrencyService currencyService)
{
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IProductPriceRepository _productPriceRepository = productPriceRepository;
    private readonly IProductImageRepository _productImageRepository = productImageRepository;
    private readonly CategoryService _categoryService = categoryService;
    private readonly ManufacturesService _manufacturesService = manufacturesService;
    private readonly ImageService _imageService = imageService;
    private readonly CurrencyService _currencyService = currencyService;
    private readonly IErrorLogger _errorLogger = errorLogger;


    public bool RegisterProduct(Product product)
    {
        var categoryEntity = _categoryService.GetorCreateCategory(product.Category);
        var manufacturerEntity = _manufacturesService.GetOrCreateManufacturer(product.Manufacturer);
        
        


        var productEntity = new ProductEntity
        {
            ArticleNumber = product.ArticleNumber,
            Title = product.Title,
            ManufacturerId = manufacturerEntity.Id,
            CategoryId = categoryEntity.Id,
            Ingress = product.Ingress ?? string.Empty,
            Description = product.Description ?? string.Empty,
            Specification = product.Specification ?? string.Empty,
        };

        try
        {
            _productRepository.Create(productEntity);
            return true;
        }
        catch (Exception ex)
        {
            _errorLogger.Log(ex.Message, "ProductService.RegisterProduct()", LogTypes.Error);
            return false;
        }
    }

    public ProductCurrencyEntity AddImageToProduct(string articleNumber, string imageUrl)
    {
        return _imageService.GetOrCreateProductImage(articleNumber, imageUrl);
    }

    public ProductPriceEntity AddProductPrice(string articleNumber, string code, string currencyName, decimal price, decimal? discountPrice = null)
    {
        return _currencyService.GetOrCreateProductCurrency(articleNumber, code, currencyName, price, discountPrice);
    }

}
