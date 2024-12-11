using Microsoft.EntityFrameworkCore;
using System.Runtime;
using WebApi.Models;

public class ApplicationDbContext : DbContext
{


    // Добавляем конструктор, который принимает DbContextOptions
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) // Передаем параметры в базовый класс DbContext
    {
    }

 


protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Настройка связи "один к одному" с явным указанием внешнего ключа
        modelBuilder.Entity<ITInfo>()
            .HasOne(i => i.OilCompany)  // ITInfo имеет одну OilCompany
            .WithOne(o => o.ITInfo)     // OilCompany имеет одну ITInfo
            .HasForeignKey<ITInfo>(i => i.id);  // Указываем внешний ключ в ITInfo
    }


    public DbSet<OilCompany> OilCompanies { get; set; }
    public DbSet<ITInfo> ITInfos { get; set; }

}

