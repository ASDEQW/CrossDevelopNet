using WebApi.Models;

public class ITInfo
{
    public int id { get; set; }
    public string cms { get; set; }
    public string externaljs { get; set; }
    public string sociallinks { get; set; }

    // Внешний ключ, ссылающийся на OilCompany
    public int OilCompanyid { get; set; }  // Это внешний ключ на OilCompany
    public OilCompany OilCompany { get; set; }  // Навигационное свойство для связи с OilCompany
}
