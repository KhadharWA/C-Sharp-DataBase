

using Shared.DTOs;
using Shared.Entities;
using Shared.Interfaces;
using Shared.Repositories;
using Shared.Utils;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Shared.Services;

public class ManufacturesService(IManufacturesRepository manufacturesRepository, IErrorLogger errorLogger)
{
    private readonly IManufacturesRepository _manufacturesRepository = manufacturesRepository;
    private readonly IErrorLogger _errorLogger = errorLogger;


    public ManufacturesEntity GetOrCreateManufacturer(string manufacturerName)
    {
        try
        {
            var manufacturesEntity = _manufacturesRepository.GetOne(x => x.ManufacturerName == manufacturerName);
            if (manufacturesEntity == null)
            {
                manufacturesEntity = new ManufacturesEntity { ManufacturerName = manufacturerName };
                _manufacturesRepository.Create(manufacturesEntity);
            }

            return manufacturesEntity;
        }
        catch (Exception ex)
        {
            _errorLogger.Log(ex.Message, "ManufacturesService.GetOrCreateManufacturer()", LogTypes.Error);
            return null!;
        }
    }


    public ManufacturerDTO GetManufacturerById(int id)
    {
        var manufacturer = _manufacturesRepository.GetOne(x => x.Id == id);
        if (manufacturer == null) return null!;

        return new ManufacturerDTO
        {
            Id = manufacturer.Id,
            ManufacturerName = manufacturer.ManufacturerName
        };
    }


    public bool UpdateManufacturer(ManufacturesEntity manufacturer)
    {
        try
        {
            Expression<Func<ManufacturesEntity, bool>> predicate = x => x.Id == manufacturer.Id;
            
            var existingManufacturer = _manufacturesRepository.GetOne(predicate);

            if (existingManufacturer != null)
            {
                
                existingManufacturer.ManufacturerName = manufacturer.ManufacturerName;

                
                _manufacturesRepository.Update(predicate, existingManufacturer);

                return true;
            }
            else
            {
                
                Debug.WriteLine("Manufacturer not found");
                return false;
            }
        }
        catch (Exception ex)
        {
            
            Debug.WriteLine(ex);
            return false;
        }
    }

    public bool DeleteManufacturer(int manufacturerId)
    {
        try
        {
            

            Expression<Func<ManufacturesEntity, bool>> predicate = m => m.Id == manufacturerId;

            
            return _manufacturesRepository.Delete(predicate);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        
            return false;
        }
    }
}
