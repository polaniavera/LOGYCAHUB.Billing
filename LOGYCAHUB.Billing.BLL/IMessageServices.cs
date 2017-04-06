using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOGYCAHUB.Billing.BLL
{
    public interface IMessageServices
    {
        string GetParametersmessages(string method, string codmessage);
    }
}
