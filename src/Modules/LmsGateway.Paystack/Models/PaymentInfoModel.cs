using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LmsGateway.Paystack.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;

namespace LmsGateway.Paystack.Models
{
    public class PaymentInfoModel
    {
        private const string CARD_FOLDER = "/paystack/images/card";

        public PaymentInfoModel(string logoPhysicalPath)
        {
            IconUrl = "/paystack/images/logo.png";
            Currencies = new List<SelectListItem>();
            CardIconUrls = GetAllCardIconUrl(logoPhysicalPath);
        }

        public string IconUrl { get; set; }
        public List<string> CardIconUrls { get; set; }
        public List<PaystackSupportedCurrency> SupportedCurrencies { get; set; }
        public PaystackSupportedCurrency SupportedCurrency { get; set; }

        //[AllowHtml]
        public string Currency { get; set; }

        public IList<SelectListItem> Currencies { get; set; }

        public List<string> GetAllCardIconUrl(string logoPhysicalPath)
        {
            List<string> cardIcons = null;
            string folderPath = string.Format(@"{0}\Modules\LmsGateway.Paystack\wwwroot\images\card", logoPhysicalPath);

            if (!Directory.Exists(folderPath))
            {
                return cardIcons;
            }

            IEnumerable<string> filePaths = Directory.EnumerateFiles(folderPath);
            if (filePaths != null && filePaths.Count() > 0)
            {
                cardIcons = new List<string>();

                filePaths = filePaths.OrderBy(x => x);
                foreach (string filePath in filePaths)
                {
                    FileInfo file = new FileInfo(filePath);
                    string fileVirtualPath = string.Format(CARD_FOLDER + "/{0}", file.Name);
                    cardIcons.Add(fileVirtualPath);
                }
            }

            return cardIcons;
        }


    }
}
