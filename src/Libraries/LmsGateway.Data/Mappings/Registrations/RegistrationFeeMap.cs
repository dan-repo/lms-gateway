using LmsGateway.Domain.Registrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LmsGateway.Data.Mappings.Registrations
{
    public class RegistrationFeeMap
    {
        public RegistrationFeeMap(EntityTypeBuilder<RegistrationFee> entityBuilder)
        {
            entityBuilder.ToTable("REGISTRATION_FEE");

            entityBuilder.HasKey(x => x.Id);

            entityBuilder.Property(x => x.RegistrationPeriodId).HasColumnName("Registration_Period_Id");
            entityBuilder.Property(x => x.ProgrammeId).HasColumnName("Programme_Id");
            entityBuilder.Property(x => x.DepartmentId).HasColumnName("Department_Id");
            entityBuilder.Property(x => x.LevelId).HasColumnName("Level_Id");
            entityBuilder.Property(x => x.AmountPayable).HasColumnName("Amount_Payable");
            entityBuilder.Property(x => x.AccessCharge).HasColumnName("Access_Charge");
            entityBuilder.Property(x => x.CanMakePartPayment).HasColumnName("Can_Make_Part_Payment");
            entityBuilder.Property(x => x.CreatedOn).HasColumnName("Created_On");
            entityBuilder.Property(x => x.CreatedBy).HasColumnName("Created_By").HasMaxLength(450);

            

        }



    }
}
