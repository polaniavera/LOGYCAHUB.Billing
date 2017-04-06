namespace LOGYCAHUB.Billing.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using ActionFilters;
    using LOGYCAHUB.Billing.BLL;
    using LOGYCAHUB.Billing.Entities;
    
    [RoutePrefix("api/billing")]
    public class BillingController : ApiController
    {
        private readonly IPricatsEmpresaServices _pricatsEmpresaServices;

        #region Public Constructor

        /// <summary>
        /// Public constructor to initialize registro service instance
        /// </summary>
        public BillingController()
        {
            _pricatsEmpresaServices = new PricatsEmpresaServices();
        }

        #endregion

        // GET api/registro
        [Authorize]
        public HttpResponseMessage Get()
        {
            var pricatsEmpresas = _pricatsEmpresaServices.GetAllPricatsEmpresas();
            if (pricatsEmpresas != null)
            {
                var pricatsEmpresaEntities = pricatsEmpresas as List<PricatsEmpresaEntity> ?? pricatsEmpresas.ToList();
                if (pricatsEmpresaEntities.Any())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, pricatsEmpresaEntities);
                }
            }

            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Registro no encontrado");
        }

        // POST api/billing/decreasebalancels
        [Authorize]
        [Route("DecreaseBalanceLS")]
        public HttpResponseMessage DecreaseBalanceLS([FromBody] ParametersEntity parametersEntity)
        {
            string validacion = _pricatsEmpresaServices.ValidacionDescuento(parametersEntity);

            if (validacion.Equals("Correcto"))
            {
                // Metodo Descuento
                string decrease = _pricatsEmpresaServices.DecreasePricat(parametersEntity.GlnProveedor);
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, decrease);
            }

            return Request.CreateErrorResponse(HttpStatusCode.NotFound, validacion);
        }
    }
}
