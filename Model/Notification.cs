using System;
using System.Collections.Generic;
using System.Text;

namespace RequestManager.Model
{
    public class Notification
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Sender { get; set; }
        public DateTime CreatedOn { get; set; }
        public NotificationSeverity Severity { get; set; }
    }
}
