namespace LOGYCAHUB.Billing.DAL.GenericRepository
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Configuration;

    public class PricatsEmpresaCustomRepository
    {
        internal CABASNET_TRAZABILIDADEntities ContextTrazabilidad;
        internal CABASNET_PPAL_CO_V1_0Entities ContextPPAL;
        internal DbSet<PRICATS_X_EMPRESA> DbSetTrazabilidad;
        internal DbSet<EMPRESA> DbSetPPAL;

        /// <summary>
        /// Public Constructor,initializes privately declared local variables.
        /// </summary>
        /// <param name="context"></param>
        public PricatsEmpresaCustomRepository()
        {
            this.ContextTrazabilidad = new CABASNET_TRAZABILIDADEntities();
            this.DbSetTrazabilidad = ContextTrazabilidad.Set<PRICATS_X_EMPRESA>();

            this.ContextPPAL = new CABASNET_PPAL_CO_V1_0Entities();
            this.DbSetPPAL = ContextPPAL.Set<EMPRESA>();
        }


        /// <summary>
        /// valida si el GLN del Proveedor existe y es unico
        /// </summary>
        /// <param name="glnproveedor"></param>
        /// <returns></returns>
        public string Validateintegrityglnprov(string glnproveedor)
        {
            string estado = "GLN del Proveedor no exixtente o asociado a mas de una Empresa";
            string ok = "GLN Correcto";
            string msg;

            try
            {

                var valempr = (from empr in ContextPPAL.EMPRESA
                               join cmr in ContextPPAL.CRM_EMPRESA
                               on empr.ID_EMPRESA equals cmr.ID_EMPRESA
                               where empr.EAN_EMPRESA_GLN == glnproveedor.ToString()
                               select empr).Single();

                var qry = (from p in ContextTrazabilidad.PRICATS_X_EMPRESA
                           where p.ID_EMPRESA == valempr.ID_EMPRESA && p.EAN_EMPRESA_GLN == glnproveedor
                           select p).Count();

                if (qry == 1)
                {
                    estado = ok;
                }
                else
                {
                    msg = estado;
                }
            }
            catch
            {
                msg = estado;
            }

            return estado;
        }

        /// <summary>
        /// Method Validateintegrityglncome
        /// Valida que el gln del comerciante exista en PPAL Empresa
        /// </summary>
        /// <param name="glncomerce"></param>
        /// <returns></returns>
        public string Validateintegrityglncomer(string glncomerce)
        {
            string estado = "GLN del Comerciante no exixtente o asociado a mas de una Empresa";
            string ok = "GLN Correcto";
            string msg;

            try
            {
                var qry = ContextPPAL.EMPRESA.Where(c => c.EAN_EMPRESA_GLN == glncomerce.ToString()).Count();


                if (qry == 1)
                {
                    estado = ok;
                }
                else
                {
                    msg = estado;
                }
            }
            catch
            {
                msg = estado;
            }

            return estado;
        }

        /// <summary>
        /// Valida el BGM
        /// </summary>
        /// <param name="bmg"></param>
        /// <param name="glnProv"></param>
        /// <param name="glnComer"></param>
        ///// <param name="_fecha"></param>
        /// <returns></returns>
        /// ,DateTime _fecha  Este parametro se agregara cuando se aclare el tema
        public string ValidateProcesedPricat(string bmg, string glnProv, string glnComer)
        {
            string estado;

            //Agregar Expresion Regular;
            var qry = (from aud in ContextTrazabilidad.AUDIT_MAESTRO
                       where aud.bgm == bmg && aud.glnProv == glnProv && aud.glnComer == glnComer &&
                        aud.bgm.Substring(1, 6) == "PRUEBA" && aud.bgm.Length.Equals(10) && !aud.bgm.Equals(string.Empty)
                       select aud).Count();

            if (qry == 1)
            {
                estado = "El Pricat ya fue procesado";
                //estado = "El BGM es de Prueba, no se validan Saldos";
            }
            else
            {
                estado = "El Pricat no  ha sido procesado";
                //estado = "El BGM no es de Prueba, se validan saldos";
            }

            return estado;

        }

        /// <summary>
        /// SupportForce 
        /// Valida  que el cliente tengo soporte activo
        /// </summary>
        /// <param name="glnProv"></param>
        /// <returns></returns>
        public string SupportForce(string glnProv)
        {
            string ok;
            string err;
            ok = "Soporte Vigente";
            err = "No tiene Soporte Vigente";

            string estado;
            var soport = (from crm in ContextPPAL.CRM_EMPRESA
                          join empr in ContextPPAL.EMPRESA
                          on crm.ID_EMPRESA equals empr.ID_EMPRESA
                          where empr.EAN_EMPRESA_GLN.Equals(glnProv.ToString())
                          select crm).Single();

            if (soport.IND_SOPORTE_VIGENTE == true)
            {
                estado = ok;
            }
            else
            {
                estado = err;
            }

            return estado;

        }
        /// <summary>
        /// Validatebalance
        /// Valida que el NUM_PRICATS_COMPRADOS sea mayor a l de NUM_PRICATS_PROCESADOS
        /// </summary>
        /// <param name="glnProv"></param>
        /// <returns></returns>
        public string Validatebalance(string glnProv)
        {
            string estado;
            var balance = ContextTrazabilidad.PRICATS_X_EMPRESA.Where(c => c.EAN_EMPRESA_GLN == glnProv).Single();

            if (balance.NUM_PRICATS_COMPRADOS > balance.NUM_PRICATS_PROCESADOS)
            {
                estado = "Se Puede realizar el Descuento";
            }
            else
            {
                estado = "No se Puede realizar el Descuento Porque el NUM_PRICATS_COMPRADOS es menor que el NUM_PRICATS_PROCESADOS ";
            }

            return estado;
        }
        /// <summary>
        /// ExpirationDate
        /// Valida que la fecha de Expiracion sea mayor o igual a la fecha actual
        /// </summary>
        /// <param name="glnProv"></param>
        /// <returns></returns>
        public string ExpirationDate(string glnProv)
        {
            string err = "La FECHA_EXPIRACION es menor a la fecha Actual";
            string ok = "Se Puede Realizar el Descuento";

            string estado;
            var expirationDate = ContextTrazabilidad.PRICATS_X_EMPRESA.Where(c => c.EAN_EMPRESA_GLN == glnProv).Single();

            if (expirationDate.FECHA_EXPIRACION >= DateTime.Today)
            {
                estado = ok;
            }
            else
            {
                estado = err;
            }

            return estado;
        }



        /// <summary>
        /// Return PricatsxEmpresa object by GlnProveedor
        /// </summary>
        /// <param name="glnProveedor"></param>
        /// <returns>PRICATS_X_EMPRESA</returns>
        public PRICATS_X_EMPRESA GetByGln(string glnProveedor)
        {
            return ContextTrazabilidad.PRICATS_X_EMPRESA.Where(c => c.EAN_EMPRESA_GLN.Equals(glnProveedor)).First();
        }
    }
}
