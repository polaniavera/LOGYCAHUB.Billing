using LOGYCAHUB.Billing.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOGYCAHUB.Billing.BLL
{
    public class MessageServices : IMessageServices
    {
        private readonly UnitOfWork _unitOfWork;
        /// <summary>
        /// Public constructor.
        /// </summary>
        public MessageServices()
        {
            _unitOfWork = new UnitOfWork();

        }

        public string GetParametersmessages(string method, string codmessage)
        {
            string estado = null;
            var msj = _unitOfWork.MessagesRepository.GetManyQueryable(c => c.Method == method && c.MessageCod == codmessage).SingleOrDefault();

            estado = msj.MessageDetail;

            return estado;


        }
    }
}
