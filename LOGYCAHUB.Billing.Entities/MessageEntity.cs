namespace LOGYCAHUB.Billing.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class MessageEntity
    {

        public int Id { get; set; }

        public string MessageCod { get; set; }
        public string Method { get; set; }
        public string MessageDetail { get; set; }

        public bool State { get; set; }
    }
}
