using WebApi.Models;

public class OilCompanyDTO
{
    public int id { get; set; }
    public string name { get; set; }
    public string industry { get; set; }
    public string cms { get; set; }  // Новое поле с маленькой буквы
    public string externaljs { get; set; }  // Новое поле с маленькой буквы
    public string sociallinks { get; set; }  // Новое поле с маленькой буквы

    public static OilCompanyDTO FromModels(OilCompany oilCompany, ITInfo itInfo)
    {
        return new OilCompanyDTO
        {
            id = oilCompany.id,
            name = oilCompany.name,
            industry = oilCompany.industry,
            cms = itInfo.cms,
            externaljs = itInfo.externaljs,  
            sociallinks = itInfo.sociallinks
        };
    }
}
