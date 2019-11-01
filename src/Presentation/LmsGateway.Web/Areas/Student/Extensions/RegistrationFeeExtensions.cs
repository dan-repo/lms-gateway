using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LmsGateway.Core.Extensions;
using LmsGateway.Domain.Registrations;
using LmsGateway.Web.Areas.Student.Models;
using LmsGateway.Core.Infrastructure;

namespace LmsGateway.Web.Areas.Student.Extensions
{
    public static class RegistrationFeeExtensions
    {
        public static RegistrationFee ToRegistrationFee(this RegistrationFeeModel registrationFeeModel)
        {
            Guard.NotNull(registrationFeeModel, nameof(registrationFeeModel));

            RegistrationFee registrationFee = new RegistrationFee();
            return ToRegistrationFeeHelper(registrationFeeModel, registrationFee);
        }

        public static RegistrationFee ToRegistrationFee(this RegistrationFeeModel registrationFeeModel, RegistrationFee registrationFee)
        {
            Guard.NotNull(registrationFee, nameof(registrationFee));
            Guard.NotNull(registrationFeeModel, nameof(registrationFeeModel));
                        
            return ToRegistrationFeeHelper(registrationFeeModel, registrationFee);
        }

        public static RegistrationFeeModel ToModel(this RegistrationFee registrationFee)
        {
            Guard.NotNull(registrationFee, nameof(registrationFee));

            RegistrationFeeModel registrationFeeModel = new RegistrationFeeModel();
            return ToModelHelper(registrationFeeModel, registrationFee);
        }

        public static IList<RegistrationFeeModel> ToModels(this IList<RegistrationFee> registrationFees)
        {
            if (registrationFees == null || registrationFees.Count <= 0)
            {
                throw new ArgumentNullException("registrationFees");
            }

            IList<RegistrationFeeModel> registrationFeeModels = new List<RegistrationFeeModel>();
            foreach (RegistrationFee registrationFee in registrationFees)
            {
                RegistrationFeeModel registrationFeeModel = new RegistrationFeeModel();
                registrationFeeModels.Add(ToModelHelper(registrationFeeModel, registrationFee));
            }

            return registrationFeeModels;
        }

        public static IList<RegistrationFee> ToRegistrationFees(this IList<RegistrationFeeModel> registrationFeeModels, bool includeId)
        {
            if (registrationFeeModels == null || registrationFeeModels.Count <= 0)
            {
                throw new ArgumentNullException("registrationFeeModels");
            }
            
            IList<RegistrationFee> registrationFees = new List<RegistrationFee>();
            foreach (RegistrationFeeModel registrationFeeModel in registrationFeeModels)
            {
                RegistrationFee registrationFee = new RegistrationFee();
                if (includeId)
                {
                    registrationFee.Id = registrationFeeModel.Id;
                }

                registrationFee = ToRegistrationFeeHelper(registrationFeeModel, registrationFee);
                registrationFees.Add(registrationFee);
            }

            return registrationFees;
        }

        private static RegistrationFee ToRegistrationFeeHelper(RegistrationFeeModel registrationFeeModel, RegistrationFee registrationFee)
        {
            registrationFee = registrationFee ?? new RegistrationFee();
           
            if (registrationFeeModel != null)
            {
                registrationFee.Id = registrationFeeModel.Id;
                registrationFee.AmountPayable = registrationFeeModel.AmountPayable;
                registrationFee.AccessCharge = registrationFeeModel.AccessCharge;
                registrationFee.CanMakePartPayment = registrationFeeModel.CanMakePartPayment;
                registrationFee.CreatedOn = registrationFeeModel.CreatedOn;
                registrationFee.CreatedBy = registrationFeeModel.CreatedBy;
                registrationFee.RegistrationPeriodId = registrationFeeModel.RegistrationPeriodId;
                registrationFee.LevelId = registrationFeeModel.LevelId;
                registrationFee.DepartmentId = registrationFeeModel.DepartmentId;
                registrationFee.ProgrammeId = registrationFeeModel.ProgrammeId;

                if (registrationFeeModel.Level != null && registrationFeeModel.Level.Id > 0)
                {
                    registrationFee.Level = registrationFeeModel.Level;
                }
                if (registrationFeeModel.Department != null && registrationFeeModel.Department.Id > 0)
                {
                    registrationFee.Department = registrationFeeModel.Department;
                }
                if (registrationFeeModel.RegistrationPeriod != null && registrationFeeModel.RegistrationPeriod.Id > 0)
                {
                    registrationFee.RegistrationPeriod = registrationFeeModel.RegistrationPeriod;
                }
                if (registrationFeeModel.Programme != null && registrationFeeModel.Programme.Id > 0)
                {
                    registrationFee.Programme = registrationFeeModel.Programme;
                }
            }
            
            return registrationFee;
        }

        private static RegistrationFeeModel ToModelHelper(RegistrationFeeModel registrationFeeModel, RegistrationFee registrationFee)
        {
            registrationFeeModel = registrationFeeModel ?? new RegistrationFeeModel();

            if (registrationFee != null)
            {
                registrationFeeModel.Id = registrationFee.Id;
                registrationFeeModel.AmountPayable = registrationFee.AmountPayable;
                registrationFeeModel.AccessCharge = registrationFee.AccessCharge;
                registrationFeeModel.CanMakePartPayment = registrationFee.CanMakePartPayment;
                registrationFeeModel.CreatedOn = registrationFee.CreatedOn;
                registrationFeeModel.CreatedBy = registrationFee.CreatedBy;
                registrationFeeModel.RegistrationPeriodId = registrationFee.RegistrationPeriodId;
                registrationFeeModel.LevelId = registrationFee.LevelId;
                registrationFeeModel.DepartmentId = registrationFee.DepartmentId;
                registrationFeeModel.ProgrammeId = registrationFee.ProgrammeId;

                if (registrationFee.Level != null && registrationFee.Level.Id > 0)
                {
                    registrationFeeModel.Level = registrationFee.Level;
                }
                if (registrationFee.Department != null && registrationFee.Department.Id > 0)
                {
                    registrationFeeModel.Department = registrationFee.Department;
                }
                if (registrationFee.RegistrationPeriod != null && registrationFee.RegistrationPeriod.Id > 0)
                {
                    registrationFeeModel.RegistrationPeriod = registrationFee.RegistrationPeriod;
                }
                if (registrationFee.Programme != null && registrationFee.Programme.Id > 0)
                {
                    registrationFeeModel.Programme = registrationFee.Programme;
                }
            }

            return registrationFeeModel;
        }

       



    }
}
