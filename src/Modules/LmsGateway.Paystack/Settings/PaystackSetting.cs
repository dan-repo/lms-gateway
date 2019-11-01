using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LmsGateway.Core.Configuration;

namespace LmsGateway.Paystack.Settings
{
    public class PaystackSetting : ISetting
    {
        //[SmartResourceDisplayName("Plugins.SmartStore.Paystack.Fields.ApiBaseUrl")]
        public string ApiBaseUrl { get; set; }

        //[SmartResourceDisplayName("Plugins.SmartStore.Paystack.Fields.ReferencePrefix")]
        public string ReferencePrefix { get; set; }

        //[SmartResourceDisplayName("Plugins.SmartStore.Paystack.Fields.PublicKey")]
        public string PublicKey { get; set; }

        //[SmartResourceDisplayName("Plugins.SmartStore.Paystack.Fields.SecretKey")]
        public string SecretKey { get; set; }

        //[SmartResourceDisplayName("Plugins.SmartStore.Paystack.Fields.Key")]
        public string Key
        {
            get { return UsePublicKey == true ? PublicKey : SecretKey; }
        }

        //[SmartResourceDisplayName("Plugins.SmartStore.Paystack.Fields.SendMailOnFailedTransaction")]
        public bool SendMailOnFailedTransaction { get; set; }

        //[SmartResourceDisplayName("Plugins.SmartStore.Paystack.Fields.UsePublicKey")]
        public bool UsePublicKey { get; set; }

        //[SmartResourceDisplayName("Plugins.SmartStore.Paystack.Fields.ListTransactionEndPoint")]
        public string ListTransactionEndPoint { get; set; }

        //[SmartResourceDisplayName("Plugins.SmartStore.Paystack.Fields.VerifyTransactionEndPoint")]
        public string VerifyTransactionEndPoint { get; set; }

        //[SmartResourceDisplayName("Plugins.SmartStore.Paystack.Fields.FindTransactionEndPount")]
        public string FetchTransactionEndPoint { get; set; }

        //[SmartResourceDisplayName("Plugins.SmartStore.Paystack.Fields.InitializeTransactionEndPoint")]
        public string InitializeTransactionEndPoint { get; set; }

        //[SmartResourceDisplayName("Plugins.SmartStore.Paystack.Fields.TransactionTotalEndPoint")]
        public string TransactionTotalEndPoint { get; set; }
        //[SmartResourceDisplayName("Plugins.SmartStore.Paystack.Fields.FetchSettlementsEndPoint")]
        public string FetchSettlementsEndPoint { get; set; }

        //[SmartResourceDisplayName("Plugins.SmartStore.Paystack.Fields.APIAcceptHeaderKey")]
        public string APIAcceptHeaderKey { get; set; }

        //[SmartResourceDisplayName("Plugins.SmartStore.Paystack.Fields.APIAcceptHeaderValue")]
        public string APIAcceptHeaderValue { get; set; }

        //[SmartResourceDisplayName("Plugins.SmartStore.Paystack.Fields.APIAuthorizationHeaderKey")]
        public string APIAuthorizationHeaderKey { get; set; }

        //[SmartResourceDisplayName("Plugins.SmartStore.Paystack.Fields.APIAuthorizationHeaderValue")]
        public string APIAuthorizationHeaderValue { get; set; }

        //[SmartResourceDisplayName("Plugins.SmartStore.Paystack.Fields.APIFromQueryParameterKey")]
        public string APIFromQueryParameterKey { get; set; }

        //[SmartResourceDisplayName("Plugins.SmartStore.Paystack.Fields.APIToQueryParameterKey")]
        public string APIToQueryParameterKey { get; set; }

        //[SmartResourceDisplayName("Plugins.SmartStore.Paystack.Fields.APIAmountQueryParameterKey")]
        public string APIAmountQueryParameterKey { get; set; }

        //[SmartResourceDisplayName("Plugins.SmartStore.Paystack.Fields.APIStatusQueryParameterKey")]
        public string APIStatusQueryParameterKey { get; set; }

        //[SmartResourceDisplayName("Plugins.SmartStore.Paystack.Fields.APIReferenceParameterKey")]
        public string APIReferenceParameterKey { get; set; }

        //[SmartResourceDisplayName("Plugins.SmartStore.Paystack.Fields.APIAmountParameterKey")]
        public string APIAmountParameterKey { get; set; }

        //[SmartResourceDisplayName("Plugins.SmartStore.Paystack.Fields.APICallbackUrlParameterKey")]
        public string APICallbackUrlParameterKey { get; set; }

        //[SmartResourceDisplayName("Plugins.SmartStore.Paystack.Fields.APIEmailParameterKey")]
        public string APIEmailParameterKey { get; set; }

        //[SmartResourceDisplayName("Plugins.SmartStore.Paystack.Fields.APICustomFieldsParameterKey")]
        public string APICustomFieldsParameterKey { get; set; }

        //[SmartResourceDisplayName("Plugins.SmartStore.Paystack.Fields.APIMetaDataParameterKey")]
        public string APIMetaDataParameterKey { get; set; }

        //[SmartResourceDisplayName("Plugins.SmartStore.Paystack.Fields.TransactionSearchDateFormat")]
        public string TransactionSearchDateFormat { get; set; }


    }
}
