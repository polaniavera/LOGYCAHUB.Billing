using LOGYCAHUB.Billing.DAL;
using LOGYCAHUB.Billing.DAL.UnitOfWork;
using LOGYCAHUB.Billing.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOGYCAHUB.Billing.BLL.Token
{
    public class TokenServices : ITokenServices
    {
        #region Private member variables.
        private readonly UnitOfWork _unitOfWork;
        private readonly ITokenContainer tokenContainer;
        #endregion

        #region Public constructor.
        /// <summary>
        /// Public constructor.
        /// </summary>
        public TokenServices(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            tokenContainer = new TokenContainer();
    }
        #endregion


        #region Public member methods.

        /// <summary>
        ///  Function to generate unique token with expiry against the provided userId.
        ///  Also add a record in database for generated token.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public TokensEntity GenerateToken(int userId)
        {
            string token = Guid.NewGuid().ToString();
            DateTime issuedOn = DateTime.Now;
            //DateTime expiredOn = DateTime.Now.AddSeconds(Convert.ToDouble(ConfigurationManager.AppSettings["AuthTokenExpiry"]));
            DateTime expiredOn = DateTime.Now.AddSeconds(900);
            var tokendomain = new TOKENS
            {
                ID_USER = userId,
                AUTH_TOKEN = token,
                EMISION = issuedOn,
                EXPIRACION = expiredOn
            };

            _unitOfWork.TokenRepository.Insert(tokendomain);
            _unitOfWork.SaveTrazabilidad();
            var tokenModel = new TokensEntity()
            {
                ID_USER = userId,
                EMISION = issuedOn,
                EXPIRACION = expiredOn,
                AUTH_TOKEN = token
            };

            return tokenModel;
        }

        /// <summary>
        /// Method to validate token against expiry and existence in database.
        /// </summary>
        /// <param name="tokenId"></param>
        /// <returns></returns>
        public bool ValidateToken(string tokenId)
        {
            var token = _unitOfWork.TokenRepository.Get(t => t.AUTH_TOKEN == tokenId && t.EXPIRACION > DateTime.Now);
            if (token != null && !(DateTime.Now > token.EXPIRACION))
            {
                //token.EXPIRACION = token.EXPIRACION.AddSeconds(Convert.ToDouble(ConfigurationManager.AppSettings["AuthTokenExpiry"]));
                token.EXPIRACION = token.EXPIRACION.AddSeconds(900);
                _unitOfWork.TokenRepository.Update(token);
                _unitOfWork.SaveTrazabilidad();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Method to kill the provided token id.
        /// </summary>
        /// <param name="tokenId">true for successful delete</param>
        public bool Kill(string tokenId)
        {
            _unitOfWork.TokenRepository.Delete(x => x.AUTH_TOKEN == tokenId);
            _unitOfWork.SaveTrazabilidad();
            var isNotDeleted = _unitOfWork.TokenRepository.GetMany(x => x.AUTH_TOKEN == tokenId).Any();
            if (isNotDeleted) { return false; }
            return true;
        }

        /// <summary>
        /// Delete tokens for the specific deleted user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>true for successful delete</returns>
        public bool DeleteByUserId(int userId)
        {
            _unitOfWork.TokenRepository.Delete(x => x.ID_USER == userId);
            _unitOfWork.SaveTrazabilidad();

            var isNotDeleted = _unitOfWork.TokenRepository.GetMany(x => x.ID_USER == userId).Any();
            return !isNotDeleted;
        }

        #endregion
    }
}
