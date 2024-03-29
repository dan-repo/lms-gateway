﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using LmsGateway.Domain.Configuration;
using LmsGateway.Data.Mappings.Configuration;
using LmsGateway.Data.Mappings.Registrations;
using LmsGateway.Domain.Registrations;
using LmsGateway.Domain.Medias;
using LmsGateway.Data.Mappings.Medias;
using LmsGateway.Core.Data;

namespace LmsGateway.Data
{
    public class EFDataContext : DbContext
    {
        public EFDataContext(DbContextOptions<EFDataContext> options)
           : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Setting>(x => new SettingMap(x));
            modelBuilder.Entity<Course>(x => new CourseMap(x));
            modelBuilder.Entity<Department>(x => new DepartmentMap(x));
            modelBuilder.Entity<Level>(x => new LevelMap(x));
            modelBuilder.Entity<Programme>(x => new ProgrammeMap(x));
            modelBuilder.Entity<RegistrationFee>(x => new RegistrationFeeMap(x));
            modelBuilder.Entity<Registration>(x => new RegistrationMap(x));
            modelBuilder.Entity<RegistrationDetail>(x => new RegistrationDetailMap(x));
            modelBuilder.Entity<RegistrationPeriod>(x => new RegistrationPeriodMap(x));
            modelBuilder.Entity<Semester>(x => new SemesterMap(x));
            modelBuilder.Entity<Session>(x => new SessionMap(x));
            modelBuilder.Entity<AdmissionList>(x => new AdmissionListMap(x));
            modelBuilder.Entity<AdmissionListDetail>(x => new AdmissionListDetailMap(x));
            modelBuilder.Entity<Faculty>(x => new FacultyMap(x));
            modelBuilder.Entity<Media>(x => new MediaMap(x));
            modelBuilder.Entity<Student>(x => new StudentMap(x));
            modelBuilder.Entity<StudentLevel>(x => new StudentLevelMap(x));
            
            base.OnModelCreating(modelBuilder);
        }



        //public override int SaveChanges()
        //{
        //    return base.SaveChanges();
        //}

        //private void OnSavingChanges(object sender, EventArgs e)
        //{
        //    //Sender is of type ObjectContext. Can get current and original values, and
        //    //cancel/modify the save operation as desired.
        //    var context = sender as ObjectContext;
        //    if (context == null) return;

        //    foreach (ObjectStateEntry entry in context.ObjectStateManager.GetObjectStateEntries(EntityState.Modified | EntityState.Added | EntityState.Deleted))
        //    {
        //        if (entry == null || entry.Entity == null) return;

        //        string entityName = entry.Entity.GetType().Name;

        //        Dictionary<string, string> current = null;
        //        Dictionary<string, string> original = null;

        //        switch (entry.State)
        //        {
        //            case EntityState.Modified:
        //                {
        //                    original = GetOriginalValues(entry);
        //                    current = GetCurrentValues(entry);
        //                    break;
        //                }
        //            case EntityState.Deleted:
        //                {
        //                    original = GetOriginalValues(entry);
        //                    break;
        //                }
        //            case EntityState.Added:
        //                {
        //                    current = GetCurrentValues(entry);
        //                    break;
        //                }
        //        }
        //    }
        //}

        //private Dictionary<string, string> GetOriginalValues(ObjectStateEntry entity)
        //{
        //    Dictionary<string, string> original = null;
        //    DbDataRecord originalValues = entity.OriginalValues;

        //    if (originalValues != null)
        //    {
        //        original = new Dictionary<string, string>();
        //        for (int i = 0; i < originalValues.FieldCount; i++)
        //        {
        //            original.Add(originalValues.GetName(i), originalValues.GetValue(i).ToString());
        //        }
        //    }

        //    return original;
        //}

        //private Dictionary<string, string> GetCurrentValues(ObjectStateEntry entity)
        //{
        //    Dictionary<string, string> current = null;
        //    CurrentValueRecord currentValues = entity.CurrentValues;

        //    if (currentValues != null)
        //    {
        //        current = new Dictionary<string, string>();
        //        for (int i = 0; i < currentValues.FieldCount; i++)
        //        {
        //            current.Add(currentValues.GetName(i), currentValues.GetValue(i).ToString());
        //        }
        //    }

        //    return current;
        //}




    }
}
