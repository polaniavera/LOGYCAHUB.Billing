using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOGYCAHUB.Billing.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Save method.
        /// </summary>
        void SavePpal();
        void SaveTrazabilidad();
    }
}
