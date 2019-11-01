using LmsGateway.Domain.Registrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LmsGateway.Data.Mappings.Registrations
{
    public class SessionMap
    {
        public SessionMap(EntityTypeBuilder<Session> entityBuilder)
        {
            entityBuilder.ToTable("SESSION");

            entityBuilder.HasKey(x => x.Id);

            entityBuilder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            entityBuilder.Property(x => x.StartDate).HasColumnName("Start_Date");
            entityBuilder.Property(x => x.EndDate).HasColumnName("End_Date");
            entityBuilder.Property(x => x.CreatedOn).HasColumnName("Created_On");
            entityBuilder.Property(x => x.CreatedBy).HasColumnName("Created_By").HasMaxLength(450);

        }



    }
}
