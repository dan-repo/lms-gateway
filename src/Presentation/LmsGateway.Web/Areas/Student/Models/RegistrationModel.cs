using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LmsGateway.Domain.Registrations;
using Microsoft.AspNetCore.Mvc.Rendering;
using LmsGateway.Domain.Users;
using System.ComponentModel.DataAnnotations;

namespace LmsGateway.Web.Areas.Student.Models
{
    public class RegistrationModel
    {
        public RegistrationModel()
        {
            Fees = new List<SelectListItem>();
            Periods = new List<SelectListItem>();
            RegistrationFees = new List<RegistrationFee>();
            RegistrationPeriods = new List<RegistrationPeriod>();
        }

        [Required]
        public int Fee { get; set; }
        [Required]
        public int Period { get; set; }
        [Required]
        public int Currency { get; set; }
        public string TransactionReference { get; set; }

        public int Id { get; set; }
        [Required]
        public User Student { get; set; }
        [Required]
        public decimal AccessCharge { get; set; }
        [Required]
        public decimal AmountPayable { get; set; }
        [Required]
        public decimal RegistrationFee { get; set; }

        public bool PaymentSuccessful { get; set; }

        public List<SelectListItem> Fees { get; set; }
        public List<SelectListItem> Periods { get; set; }

        public List<RegistrationFee> RegistrationFees { get; set; }
        public List<RegistrationPeriod> RegistrationPeriods { get; set; }

        public RegistrationPeriod RegistrationPeriod { get; set; }

       
    }



}
