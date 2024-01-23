

using Shared.Entities;
using Shared.Interfaces;
using Shared.Repositories;
using Shared.Utils;


namespace Shared.Services;

public class ImageService(IImageRepository imageRepository, IErrorLogger errorLogger,IProductImageRepository productImageRepository)
{
    private readonly IImageRepository _imageRepository = imageRepository;
    private readonly IProductImageRepository _productImageRepository = productImageRepository;
    private readonly IErrorLogger _errorLogger = errorLogger;

    public ProductCurrencyEntity GetOrCreateProductImage(string articleNumber, string imageUrl)
    {
        
        var imageEntity = _imageRepository.GetOne(i => i.ImageUrl == imageUrl);
        if (imageEntity == null)
        {
            
            imageEntity = new ImageEntity { ImageUrl = imageUrl };
            _imageRepository.Create(imageEntity);
        }

        
        var productImageEntity = _productImageRepository.GetOne(pie => pie.ArticleNumber == articleNumber && pie.ImageId == imageEntity.Id);
        if (productImageEntity == null)
        {
            
            productImageEntity = new ProductCurrencyEntity
            {
                ArticleNumber = articleNumber,
                ImageId = imageEntity.Id
            };
            _productImageRepository.Create(productImageEntity);
        }

        return productImageEntity;
    }
}
