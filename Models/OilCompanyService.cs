using Microsoft.EntityFrameworkCore;
using WebApi.Models;

public class OilCompanyService
{
    private readonly ApplicationDbContext _context;

    public OilCompanyService(ApplicationDbContext context)
    {
        _context = context;
    }

    // Метод для получения всех компаний с включением ITInfo
    public async Task<IEnumerable<OilCompanyDTO>> GetAllOilCompaniesAsync()
    {
        var oilCompanies = await _context.OilCompanies
            .Include(o => o.ITInfo)  // Включаем связанный объект ITInfo
            .ToListAsync();

        return oilCompanies.Select(o => new OilCompanyDTO
        {
            id = o.id,
            name = o.name,
            industry = o.industry,
            cms = o.ITInfo?.cms,
            externaljs = o.ITInfo?.externaljs,
            sociallinks = o.ITInfo?.sociallinks
        }).ToList();
    }

    // Метод для получения компании по ID с включением ITInfo
    public async Task<OilCompanyDTO> GetOilCompanyByIdAsync(int id)
    {
        var oilCompany = await _context.OilCompanies
            .Include(o => o.ITInfo)  // Включаем связанный объект ITInfo
            .Where(o => o.id == id)   // Фильтрация по id
            .FirstOrDefaultAsync();

        if (oilCompany == null)
        {
            return null;
        }

        return new OilCompanyDTO
        {
            id = oilCompany.id,
            name = oilCompany.name,
            industry = oilCompany.industry,
            cms = oilCompany.ITInfo?.cms,
            externaljs = oilCompany.ITInfo?.externaljs,
            sociallinks = oilCompany.ITInfo?.sociallinks
        };
    }

    // Создать компанию
    public async Task<OilCompanyDTO> CreateOilCompanyAsync(OilCompanyDTO oilCompanyDTO)
    {
        // Создаем объект OilCompany
        var oilCompany = new OilCompany
        {
            name = oilCompanyDTO.name,
            industry = oilCompanyDTO.industry
        };

        // Создаем связанный объект ITInfo
        var itInfo = new ITInfo
        {
            cms = oilCompanyDTO.cms,
            externaljs = oilCompanyDTO.externaljs,
            sociallinks = oilCompanyDTO.sociallinks,
            OilCompany = oilCompany  // Связываем с компанией
        };

        // Добавляем компанию и IT информацию в контекст
        _context.OilCompanies.Add(oilCompany);
        _context.ITInfos.Add(itInfo);
        await _context.SaveChangesAsync();

        // Возвращаем созданную компанию в DTO
        return new OilCompanyDTO
        {
            id = oilCompany.id,
            name = oilCompany.name,
            industry = oilCompany.industry,
            cms = itInfo.cms,
            sociallinks = itInfo.sociallinks
        };
    }

    public async Task<OilCompanyDTO> UpdateOilCompanyAsync(int id, OilCompanyDTO oilCompanyDTO)
    {
        // Получаем существующую компанию по id
        var existingOilCompany = await _context.OilCompanies.FindAsync(id);
        var existingITInfo = await _context.ITInfos.FirstOrDefaultAsync(i => i.OilCompanyid == id);

        if (existingOilCompany == null || existingITInfo == null)
        {
            return null;  // Если компания или IT информация не найдены
        }

        // Обновляем информацию о компании
        existingOilCompany.name = oilCompanyDTO.name;
        existingOilCompany.industry = oilCompanyDTO.industry;

        // Обновляем информацию о IT
        existingITInfo.cms = oilCompanyDTO.cms;
        existingITInfo.externaljs = oilCompanyDTO.externaljs;
        existingITInfo.sociallinks = oilCompanyDTO.sociallinks;

        // Устанавливаем объекты как измененные
        _context.Entry(existingOilCompany).State = EntityState.Modified;
        _context.Entry(existingITInfo).State = EntityState.Modified;

        // Сохраняем изменения в базе данных
        await _context.SaveChangesAsync();

        // Возвращаем обновленный DTO
        return new OilCompanyDTO
        {
            id = existingOilCompany.id,
            name = existingOilCompany.name,
            industry = existingOilCompany.industry,
            cms = existingITInfo.cms,
            sociallinks = existingITInfo.sociallinks
        };
    }

    public async Task<bool> DeleteOilCompanyAsync(int id)
    {
        // Находим компанию с включением связанной информации о IT
        var oilCompany = await _context.OilCompanies
            .Include(o => o.ITInfo)  // Включаем ITInfo для удаления
            .FirstOrDefaultAsync(o => o.id == id);

        if (oilCompany == null)
        {
            return false;  // Компания не найдена
        }

        // Удаляем ITInfo, если оно существует
        if (oilCompany.ITInfo != null)
        {
            _context.ITInfos.Remove(oilCompany.ITInfo);
        }

        // Удаляем саму компанию
        _context.OilCompanies.Remove(oilCompany);
        await _context.SaveChangesAsync();

        return true;  // Успешное удаление
    }
}
