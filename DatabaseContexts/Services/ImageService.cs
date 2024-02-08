

using Shared.DTOs;
using Shared.Entities;
using Shared.Interfaces;
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


    public ProductImageDTO GetImageUrlByArticleNumber(string articleNumber)
    {
        
        var productImage = _productImageRepository.ReadAll()
            .FirstOrDefault(img => img.ArticleNumber == articleNumber); 

        
        var imageEntity = _imageRepository.GetOne(img => img.Id == productImage!.ImageId);
        if (imageEntity != null)
        {
            return new ProductImageDTO { ImageUrl = imageEntity.ImageUrl };
        }

        
        return null!;
    }

    public bool UpdateProductImage(string articleNumber, string newImageUrl)
    {
        try
        {
            
            var productImageEntity = _productImageRepository.GetOne(pie => pie.ArticleNumber == articleNumber);
            if (productImageEntity == null) return false; 

            
            var imageEntity = _imageRepository.GetOne(i => i.ImageUrl == newImageUrl);
            if (imageEntity == null)
            {
                imageEntity = new ImageEntity { ImageUrl = newImageUrl };
                _imageRepository.Create(imageEntity);
            }

            productImageEntity.ImageId = imageEntity.Id;
            _productImageRepository.Update(pie => pie.ArticleNumber == articleNumber, productImageEntity);

            return true;
        }
        catch (Exception ex)
        {
            _errorLogger.Log(ex.Message, "ImageService.UpdateProductImage()", LogTypes.Error);
            return false;
        }
    }

    public bool DeleteProductImageAssociation(string articleNumber)
    {
        try
        {
            return _productImageRepository.Delete(pie => pie.ArticleNumber == articleNumber);
        }
        catch (Exception ex)
        {
            _errorLogger.Log(ex.Message, "ImageService.DeleteProductImageAssociation()", LogTypes.Error);
            return false;
        }
    }


}
