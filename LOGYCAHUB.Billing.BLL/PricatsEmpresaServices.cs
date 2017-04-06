using AutoMapper;
using LOGYCAHUB.Billing.DAL.GenericRepository;
using LOGYCAHUB.Billing.DAL;
using LOGYCAHUB.Billing.DAL.UnitOfWork;
using LOGYCAHUB.Billing.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using System.Transactions;
using System;
using System.Text.RegularExpressions;

namespace LOGYCAHUB.Billing.BLL
{
    /// <summary>
    /// Offers services for product specific CRUD operations
    /// </summary>
    public class PricatsEmpresaServices : IPricatsEmpresaServices
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly PricatsEmpresaCustomRepository _pricatsEmpresaCustomRepository;
        private readonly IMessageServices _messageServices;
        /// <summary>
        /// Public constructor.
        /// </summary>
        public PricatsEmpresaServices()
        {
            _unitOfWork = new UnitOfWork();
            _pricatsEmpresaCustomRepository = new PricatsEmpresaCustomRepository();
            _messageServices = new MessageServices();

        }

        /// <summary>
        /// Fetches registro details by id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public PricatsEmpresaEntity GetPricatsEmpresaById(int empresaId)
        {
            var pricatsEmpresa = _unitOfWork.PricatsEmpresaRepository.GetByID(empresaId);
            if (pricatsEmpresa != null)
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<PRICATS_X_EMPRESA, PricatsEmpresaEntity>();
                });
                var pricatsModel = Mapper.Map<PRICATS_X_EMPRESA, PricatsEmpresaEntity>(pricatsEmpresa);

                return pricatsModel;
            }
            return null;
        }

        /// <summary>
        /// Fetches all the registros.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PricatsEmpresaEntity> GetAllPricatsEmpresas()
        {
            var pricatsEmpresas = _unitOfWork.PricatsEmpresaRepository.GetAll().ToList();
            if (pricatsEmpresas.Any())
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<PRICATS_X_EMPRESA, PricatsEmpresaEntity>();
                });
                var pricatsModels = Mapper.Map<List<PRICATS_X_EMPRESA>, List<PricatsEmpresaEntity>>(pricatsEmpresas);

                return pricatsModels;
            }
            return null;
        }

        /// <summary>
        /// Realiza validaciones concernientes
        /// </summary>
        /// <returns></returns>
        #region Validaciones del Negocio
        public string ValidacionDescuento(ParametersEntity parametersEntity)
        {
            string strMessage;
            string mserr;
            string resultValidate;


            //Valida si el Pais es Colombia
            strMessage = _messageServices.GetParametersmessages(Properties.Resources.MethodValidateCountry, Properties.Resources.OKValidateCountry);
            mserr = _messageServices.GetParametersmessages(Properties.Resources.MethodCountry, Properties.Resources.IdPais);

            if (!(parametersEntity.IdPais.Equals(strMessage)))
                return mserr;


            // si es 0  SI valida saldos , si es 1  NO valida saldos.
            mserr = _messageServices.GetParametersmessages(Properties.Resources.MethodValidateGDSN, Properties.Resources.ValidateGDSN);
            if (!parametersEntity.TipoTransaccion.Equals(false))
                return mserr;


            //Valida tipo de BGM
            strMessage = _messageServices.GetParametersmessages(Properties.Resources.MethodOKMethodValidateBGM, Properties.Resources.OKMethodValidateBGM);
            mserr = _messageServices.GetParametersmessages(Properties.Resources.MethodValidateBGM, Properties.Resources.ValidateBGM);
            resultValidate = ValidateBGM(parametersEntity.Bgm);
            if (!resultValidate.Equals(strMessage))
                return mserr;

            //Valida gln Comerciantes
            resultValidate = SupplierValidation(parametersEntity.GlnComerciante);
            strMessage = _messageServices.GetParametersmessages(Properties.Resources.MethodOKSupplierValidation, Properties.Resources.OKSupplierValidation);
            mserr = _messageServices.GetParametersmessages(Properties.Resources.MethodSupplierValidation, Properties.Resources.SupplierValidation);
            if (!resultValidate.Equals(strMessage))
                return mserr;

            //Validacion Intregridad GLN Proveedor
            strMessage = _messageServices.GetParametersmessages(Properties.Resources.MethodOKValidateintegrity, Properties.Resources.OKValidateintegrity);
            resultValidate = _pricatsEmpresaCustomRepository.Validateintegrityglnprov(parametersEntity.GlnProveedor);
            mserr = _messageServices.GetParametersmessages(Properties.Resources.MethofValidateintegrityglnprov, Properties.Resources.Validateintegrityglnprov);
            if (!resultValidate.Equals(strMessage))
                return mserr;

            //validacion integridad GLN COmerciante
            strMessage = _messageServices.GetParametersmessages(Properties.Resources.MethodOKValidateintegrity, Properties.Resources.OKValidateintegrity);
            resultValidate = _pricatsEmpresaCustomRepository.Validateintegrityglncomer(parametersEntity.GlnComerciante);
            mserr = _messageServices.GetParametersmessages(Properties.Resources.MethodValidateintegrityglncomer, Properties.Resources.Validateintegrityglncomer);
            if (!resultValidate.Equals(strMessage))
                return mserr;

            //Valida si el soporte esta Vigente

            strMessage = _messageServices.GetParametersmessages(Properties.Resources.MethodOKSupportForce, Properties.Resources.OKSupportForce);
            resultValidate = _pricatsEmpresaCustomRepository.SupportForce(parametersEntity.GlnProveedor);
            mserr = _messageServices.GetParametersmessages(Properties.Resources.MethodSupportForce, Properties.Resources.SupportForce);
            if (!resultValidate.Equals(strMessage))
                return mserr;



            //Valida si los PRICATS comprados son mayores a los gastados
            strMessage = _messageServices.GetParametersmessages(Properties.Resources.MethodOKValidatebalance, Properties.Resources.OKValidatebalance);
            mserr = _messageServices.GetParametersmessages(Properties.Resources.MethodValidatebalance, Properties.Resources.Validatebalance);
            resultValidate = _pricatsEmpresaCustomRepository.Validatebalance(parametersEntity.GlnProveedor);

            if (!resultValidate.Equals(strMessage))
                return mserr;


            //Valida que la FECHA_EXPIRACION sea mayor o igual a la actual
            strMessage = _messageServices.GetParametersmessages(Properties.Resources.MethodOKExpirationDate, Properties.Resources.OKExpirationDate);
            mserr = _messageServices.GetParametersmessages(Properties.Resources.MethodExpirationDate, Properties.Resources.ExpirationDate);

            resultValidate = _pricatsEmpresaCustomRepository.ExpirationDate(parametersEntity.GlnProveedor);
            if (!resultValidate.Equals(strMessage))
                return mserr;



            return "Correcto";
        }



        //Valida Gln del Comerciante
        private string SupplierValidation(string _glnComerce)
        {
            string tipgln;
            string glnLOGYCASYNC = Properties.Resources.glnLOGYCA;

            if (!_glnComerce.Equals(glnLOGYCASYNC))
            {
                tipgln = "Pricat Dirigido a Comerciante";
            }
            else
            {
                tipgln = "Pricat dirigido a LOGYCA/SYNC";
            }
            return tipgln;
        }

        /// <summary>
        /// Decrease pricat from PricatsxEmpresa table
        /// </summary>
        /// <param name="glnComerciante"></param>
        /// <returns>bool</returns>
        public string DecreasePricat(string glnProveedor)
        {
            bool success = false;
            string saldo = string.Empty;
            string fechaExp = string.Empty;
            string result = string.Empty;

            if (glnProveedor != null && glnProveedor != string.Empty)
            {
                using (var scope = new TransactionScope())
                {
                    var pricat = _unitOfWork.PricatsEmpresaCustomRepository.GetByGln(glnProveedor);
                    if (pricat != null)
                    {
                        pricat.NUM_PRICATS_PROCESADOS = pricat.NUM_PRICATS_PROCESADOS + 1;
                        pricat.FECHA_ACTUALIZACION = DateTime.Now;
                        saldo = (pricat.NUM_PRICATS_COMPRADOS - pricat.NUM_PRICATS_PROCESADOS).ToString();
                        fechaExp = pricat.FECHA_EXPIRACION.ToString();

                        _unitOfWork.PricatsEmpresaRepository.Update(pricat);
                        _unitOfWork.SaveTrazabilidad();
                        scope.Complete();
                        success = true;
                    }
                }
                if (success)
                {
                    result = String.Format("Saldo restante = {0} - fecha expiración = {1}", saldo, fechaExp);
                }else
                {
                    result = "Error al decrementar el Pricat";
                }
            }
            return result;
        }

        public string ValidateBGM(string bmg)
        {

            string estado;
            string Rgex;
            Rgex = "^[0-9]{4}$";
            //Properties.Resources.regexBGMkey;
            Regex regexBGM = new Regex(Rgex);
            if ((bmg.StartsWith("PRUEBA") && bmg.Length == 10 && regexBGM.IsMatch(bmg.Substring(6, 4))))
            {
                estado = "El BGM es de Prueba, no se validan Saldos";
            }

            else
            {
                estado = "El BGM no es de Prueba, se validan saldos";
            }

            return estado;
        }



        #endregion



    }

}

