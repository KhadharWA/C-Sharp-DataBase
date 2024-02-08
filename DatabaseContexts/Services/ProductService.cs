using Shared.DTOs;
using Shared.Entities;
using Shared.Interfaces;
using Shared.Models;
using Shared.Repositories;
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
        return _currencyService.GetOrCreateProductCurrency(articleNumber, currencyName, code,  price, discountPrice);
    }



    public List<ProductDetailsDTO> GetAllProductDetails()
    {
    var products = _productRepository.ReadAll();
    var productDetailsList = new List<ProductDetailsDTO>();

    foreach (var product in products)
    {
        var productDetails = GetProductDetails(product.ArticleNumber);
        if (productDetails != null)
        {
            productDetailsList.Add(productDetails);
        }
    }

        return productDetailsList;
    }

    public ProductDetailsDTO GetProductDetails(string articleNumber)
    {
        
        var productEntity = _productRepository.GetOne(x => x.ArticleNumber == articleNumber);
        if (productEntity == null)
        {
            return null!;
        }

        
        var manufacturer = _manufacturesService.GetManufacturerById(productEntity.ManufacturerId);
        var category = _categoryService.GetCategoryById(productEntity.CategoryId);

        
        var currency = _currencyService.GetProductPriceByArticleNumber(productEntity.ArticleNumber);
        var imageUrls = _imageService.GetImageUrlByArticleNumber(productEntity.ArticleNumber);

        
        var productDetailsDTO = new ProductDetailsDTO
        {
            ArticleNumber = productEntity.ArticleNumber,
            Title = productEntity.Title,
            ManufacturerName = manufacturer.ManufacturerName,  
            CategoryName = category.CategoryName,  
            Ingress = productEntity.Ingress,
            Description = productEntity.Description,
            Specification = productEntity.Specification,
            PriceDetails = currency?.Price ?? default(decimal),  
            ImageUrl = imageUrls.ImageUrl  
        };

        return productDetailsDTO;
    }


    public bool UpdateProduct(string articleNumber, ProductEntity updatedProductEntity)
    {
        try
        {

            var existingProduct = _productRepository.GetOne(product => product.ArticleNumber == articleNumber);
            if (existingProduct == null)
            {
                _errorLogger.Log($"Product with article number {articleNumber} not found.", "ProductService.RegisterProduct()", LogTypes.Error);
                return false;
            }

            existingProduct.Title = updatedProductEntity.Title;
            existingProduct.ManufacturerId = updatedProductEntity.ManufacturerId; 
            existingProduct.CategoryId = updatedProductEntity.CategoryId; 
            existingProduct.Ingress = updatedProductEntity.Ingress;
            existingProduct.Description = updatedProductEntity.Description;
            existingProduct.Specification = updatedProductEntity.Specification;




            _productRepository.Update(product => product.ArticleNumber == articleNumber, existingProduct);
            return true;
        }
        catch (Exception ex)
        {
            _errorLogger.Log(ex.Message, "ProductService.UpdateProduct()", LogTypes.Error);
            return false;
        }
    }

    public bool DeleteProduct(string articleNumber)
    {
        try
        {
            
            var prices = _productPriceRepository.ReadAll().Where(p => p.ArticleNumber == articleNumber).ToList();
            foreach (var price in prices)
            {
                _productPriceRepository.Delete(p => p.ArticleNumber == articleNumber && p.Price == price.Price);
            }

            
            var images = _productImageRepository.ReadAll().Where(img => img.ArticleNumber == articleNumber).ToList();
            foreach (var image in images)
            {
                _productImageRepository.Delete(img => img.ArticleNumber == articleNumber && img.ImageId == image.ImageId);
            }

            
            var result = _productRepository.Delete(product => product.ArticleNumber == articleNumber);

            return result;
        }
        catch (Exception ex)
        {
            _errorLogger.Log(ex.Message, "ProductService.DeleteProduct()", LogTypes.Error);
            return false;
        }
    }
}
