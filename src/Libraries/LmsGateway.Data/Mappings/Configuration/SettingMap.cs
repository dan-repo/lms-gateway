using LmsGateway.Domain.Configuration;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LmsGateway.Data.Mappings.Configuration
{
    public class SettingMap
    {
        public SettingMap(EntityTypeBuilder<Setting> entityBuilder)
        {
            entityBuilder.ToTable("SETTING");

            entityBuilder.HasKey(x => x.Id);

            entityBuilder.Property(x => x.Name).IsRequired().HasMaxLength(50);
        }


    }
}
