

using Shared.DTOs;
using Shared.Entities;
using Shared.Interfaces;
using Shared.Utils;


namespace Shared.Services;

public class CurrencyService ( ICurrencyRepository currencyRepository, IErrorLogger errorLogger, IProductPriceRepository productPriceRepository )
{
    private readonly ICurrencyRepository _currencyRepository = currencyRepository;
    private readonly IProductPriceRepository _productPriceRepository = productPriceRepository;
    private readonly IErrorLogger _errorLogger = errorLogger;


    public ProductPriceEntity GetOrCreateProductCurrency(string articleNumber, string currencyName, string code, decimal price, decimal? discountPrice)
    {


        var currencyEntity = _currencyRepository.GetOne(c => c.CurrencyName == currencyName);
        if (currencyEntity == null)
        {

            currencyEntity = new CurrencyEntity { Code = code, CurrencyName = currencyName };
            _currencyRepository.Create(currencyEntity);
        }


        try
        {
            var productPriceEntity = _productPriceRepository.GetOne(pce => pce.ArticleNumber == articleNumber && pce.CurrencyCode == currencyEntity.Code);
            if (productPriceEntity == null)
            {

                productPriceEntity = new ProductPriceEntity
                {
                    ArticleNumber = articleNumber,
                    CurrencyCode = currencyEntity.Code,
                    Price = price,
                    DiscountPrice = discountPrice
                };
                _productPriceRepository.Create(productPriceEntity);
            }
            return productPriceEntity;
        }
        catch (Exception ex)
        {
            _errorLogger.Log(ex.Message, "CurrencyService.GetOrCreateProductCurrency()", LogTypes.Error);
            return null!;
        }
    }

    public ProductPriceDTO GetProductPriceByArticleNumber(string articleNumber)
    {
        var price = _productPriceRepository.GetOne(x => x.ArticleNumber == articleNumber);
        if (price == null) return null!;

        return new ProductPriceDTO
        {
            Price = price.Price,
            DiscountPrice = price.DiscountPrice,
            CurrencyCode = price.CurrencyCode
        };
    }


    public bool UpdateProductPrice(string articleNumber, string currencyCode, decimal newPrice, decimal? newDiscountPrice)
    {
        try
        {
            var productPriceEntity = _productPriceRepository.GetOne(pce => pce.ArticleNumber == articleNumber && pce.CurrencyCode == currencyCode);
            if (productPriceEntity != null)
            {
                productPriceEntity.Price = newPrice;
                productPriceEntity.DiscountPrice = newDiscountPrice;
                _productPriceRepository.Update(p => p.ArticleNumber == articleNumber && p.CurrencyCode == currencyCode, productPriceEntity);
                return true;
            }
        }
        catch (Exception ex)
        {
            _errorLogger.Log(ex.Message, "CurrencyService.UpdateProductPrice()", LogTypes.Error);
        }
        return false;
    }

    public bool UpdateCurrency(string oldCurrencyName, string newCurrencyName, string newCode)
    {
        try
        {
            var currencyEntity = _currencyRepository.GetOne(c => c.CurrencyName == oldCurrencyName);
            if (currencyEntity != null)
            {
                currencyEntity.CurrencyName = newCurrencyName;
                currencyEntity.Code = newCode;
                _currencyRepository.Update(c => c.CurrencyName == oldCurrencyName, currencyEntity);
                return true;
            }
        }
        catch (Exception ex)
        {
            _errorLogger.Log(ex.Message, "CurrencyService.UpdateCurrency()", LogTypes.Error);
        }
        return false;
    }

    public bool DeleteProductPrice(string articleNumber, string currencyCode)
    {
        try
        {
            return _productPriceRepository.Delete(p => p.ArticleNumber == articleNumber && p.CurrencyCode == currencyCode);
        }
        catch (Exception ex)
        {
            _errorLogger.Log(ex.Message, "CurrencyService.DeleteProductPrice()", LogTypes.Error);
            return false;
        }
    }

    public bool DeleteCurrency(string currencyName)
    {
        try
        {
            return _currencyRepository.Delete(c => c.CurrencyName == currencyName);
        }
        catch (Exception ex)
        {
            _errorLogger.Log(ex.Message, "CurrencyService.DeleteCurrency()", LogTypes.Error);
            return false;
        }
    }
}
