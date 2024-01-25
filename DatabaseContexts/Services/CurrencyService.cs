

using Shared.Entities;
using Shared.Interfaces;
using Shared.Repositories;
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
}
