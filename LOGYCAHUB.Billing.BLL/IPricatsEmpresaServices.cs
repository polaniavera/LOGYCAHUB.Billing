namespace LOGYCAHUB.Billing.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using LOGYCAHUB.Billing.Entities;

    public interface IPricatsEmpresaServices
    {
        PricatsEmpresaEntity GetPricatsEmpresaById(int empresaId);

        IEnumerable<PricatsEmpresaEntity> GetAllPricatsEmpresas();

        //string GetParametersmessages(string method, string codmessage);

        string ValidacionDescuento(ParametersEntity parametersEntity);

        string DecreasePricat(string glnProveedor);

        

        // int CreatePricatsEmpresa(PricatsEmpresaEntity pricatsEmpresaEntity);
        // bool UpdatePricatsEmpresa(int empresaId, PricatsEmpresaEntity pricatsEmpresaEntity);
        // bool DeletePricatsEmpresa(int empresaId);
    }
}
