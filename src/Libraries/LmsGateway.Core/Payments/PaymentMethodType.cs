﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LmsGateway.Core.Payments
{
    public enum PaymentMethodType
    {
        Unknown = 0,
        /// <summary>
        /// All payment information is entered on the site
        /// </summary>
        Standard = 10,

        /// <summary>
        /// A customer is redirected to a third-party site in order to complete the payment
        /// </summary>
        Redirection = 15,

        /// <summary>
        /// Button
        /// </summary>
        Button = 20,

        /// <summary>
        /// All payment information is entered on the site and is available via button
        /// </summary>
        StandardAndButton = 25,

        /// <summary>
        /// Payment information is entered in checkout and customer is redirected to complete payment (e.g. 3D Secure) after order has been placed
        /// </summary>
        StandardAndRedirection = 30
    }



}
