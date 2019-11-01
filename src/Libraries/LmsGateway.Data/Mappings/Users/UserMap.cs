using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using LmsGateway.Domain.Users;

namespace LmsGateway.Data.Mappings.Users
{
    public class UserMap
    {
        public UserMap(EntityTypeBuilder<User> entityBuilder)
        {
            entityBuilder.Property(u => u.AccessFailedCount).HasColumnName("Access_Failed_Count");
            entityBuilder.Property(u => u.ConcurrencyStamp).HasColumnName("Concurrency_Stamp");
            entityBuilder.Property(u => u.EmailConfirmed).HasColumnName("Email_Confirmed");
            entityBuilder.Property(u => u.LockoutEnabled).HasColumnName("Lockout_Enabled");
            entityBuilder.Property(u => u.LockoutEnd).HasColumnName("Lockout_End");
            entityBuilder.Property(u => u.NormalizedEmail).HasColumnName("Normalized_Email");
            entityBuilder.Property(u => u.NormalizedUserName).HasColumnName("Normalized_Username");
            entityBuilder.Property(u => u.PasswordHash).HasColumnName("Password_Hash");
            entityBuilder.Property(u => u.PhoneNumber).HasColumnName("Phone_Number");
            entityBuilder.Property(u => u.PhoneNumberConfirmed).HasColumnName("Phone_Number_Confirmed");
            entityBuilder.Property(u => u.SecurityStamp).HasColumnName("Security_Stamp");
            entityBuilder.Property(u => u.TwoFactorEnabled).HasColumnName("Two_Factor_Enabled");
            entityBuilder.Property(u => u.UserName).HasColumnName("Username");

            entityBuilder.Property(u => u.Type).HasColumnName("User_Type");
            entityBuilder.Property(u => u.RegNo).HasColumnName("Reg_No");
            entityBuilder.Property(u => u.DateVerified).HasColumnName("Date_Verified");
            entityBuilder.Property(u => u.CreatedOn).HasColumnName("Created_On");

        }
        


    }
}
