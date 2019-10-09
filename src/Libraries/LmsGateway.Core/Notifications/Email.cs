using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LmsGateway.Core.Notifications
{
    public class Email
    {
        public Email()
        {
            FromEmailAddress = new EmailAddress();
            ToEmailAddress = new EmailAddress();
        }

        public EmailAddress FromEmailAddress { get; set; }
        public EmailAddress ToEmailAddress { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }

    }




}
