
using Shared.Entities;
using Shared.Interfaces;
using Shared.Utils;


namespace   Shared.Services;

public class CategoryService(ICategoryRepository categoryRepository, IErrorLogger errorLogger)
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IErrorLogger _errorLogger = errorLogger;

    public CategoryEntity GetorCreateCategory(string categoryName)
    {
        try
        {
            // Check if the category already exists
            var categoryEntity = _categoryRepository.GetOne(c => c.CategoryName == categoryName);
            if (categoryEntity == null)
            {
                // Create a new category if it doesn't exist
                categoryEntity = new CategoryEntity { CategoryName = categoryName };
                _categoryRepository.Create(categoryEntity);
            }

            return categoryEntity;
        }
        catch (Exception ex)
        {
            _errorLogger.Log(ex.Message, "CategoryService.GetorCreateCategory()", LogTypes.Error);
            return null!;
        }
    }
}
