using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOGYCAHUB.Billing.BLL.Login
{
    public interface IUserApiServices
    {
        int Authenticate(string userName, string password);
    }
}
