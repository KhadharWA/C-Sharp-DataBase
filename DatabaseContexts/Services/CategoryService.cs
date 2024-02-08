
using Shared.DTOs;
using Shared.Entities;
using Shared.Interfaces;
using Shared.Repositories;
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
            
            var categoryEntity = _categoryRepository.GetOne(c => c.CategoryName == categoryName);
            if (categoryEntity == null)
            {
                
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

    public CategoryDTO GetCategoryById(int id)
    {
        var category = _categoryRepository.GetOne(x => x.Id == id);
        if (category == null) return null!;

        return new CategoryDTO
        {
            Id = category.Id,
            CategoryName = category.CategoryName
        };
    }

    public IEnumerable<CategoryDTO> GetAllCategories()
    {
        try
        {
            var categories = _categoryRepository.ReadAll();
            if (categories == null || !categories.Any())
            { 
                return Enumerable.Empty<CategoryDTO>();  
            }

            
            var categoryDTOs = categories.Select(c => new CategoryDTO
            {
                Id = c.Id,
                CategoryName = c.CategoryName
            });

            return categoryDTOs;
        }
        catch (Exception ex)
        {
            _errorLogger.Log(ex.Message, "CategoryService.GetAllCategories()", LogTypes.Error);
            return Enumerable.Empty<CategoryDTO>();  
        }
    }

    public bool UpdateCategory(int id, string newCategoryName)
    {
        try
        {
            var category = _categoryRepository.GetOne(c => c.Id == id);
            if (category == null)
            {
                _errorLogger.Log($"Category with ID {id} not found.", "CategoryService.UpdateCategory()", LogTypes.Error);
                return false; 
            }

            category.CategoryName = newCategoryName; 
            _categoryRepository.Update(c => c.Id == id, category); 
            return true;
        }
        catch (Exception ex)
        {
            _errorLogger.Log(ex.Message, "CategoryService.UpdateCategory()", LogTypes.Error);
            return false;
        }
    }

    public bool DeleteCategory(int id)
    {
        try
        {
            var category = _categoryRepository.GetOne(c => c.Id == id);
            if (category == null)
            {
                _errorLogger.Log($"Category with ID {id} not found.", "CategoryService.DeleteCategory()", LogTypes.Warn);
                return false; 
            }

            

            _categoryRepository.Delete(c => c.Id == id); 
            return true;
        }
        catch (Exception ex)
        {
            _errorLogger.Log(ex.Message, "CategoryService.DeleteCategory()", LogTypes.Error);
            return false;
        }
    }
}
