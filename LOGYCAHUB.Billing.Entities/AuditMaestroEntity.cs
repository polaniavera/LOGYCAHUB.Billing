using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOGYCAHUB.Billing.Entities
{
    public class AuditMaestroEntity
    {
        public int id_Trazabilidad { get; set; }
        public string nombreDocumentoOriginal { get; set; }
        public string nombreDocumento { get; set; }
        public string bgm { get; set; }
        public string glnProv { get; set; }
        public string glnComer { get; set; }
        public Nullable<System.DateTime> fechaLlegada { get; set; }
        public Nullable<int> id_Formato { get; set; }
        public string id_Pais { get; set; }
        public Nullable<bool> recepcion { get; set; }
        public Nullable<bool> validarXMLInt { get; set; }
        public Nullable<bool> procesamiento { get; set; }
        public Nullable<bool> moduloCadena { get; set; }
        public Nullable<bool> generacion { get; set; }
        public Nullable<bool> confirmacion { get; set; }
        public Nullable<System.DateTime> fechaUltimoEstado { get; set; }
        public string descripcionEstado { get; set; }
        public string linkDescarga { get; set; }
        public string linkError { get; set; }
        public Nullable<System.DateTime> fechaEnvio { get; set; }
        public string estadoEnvio { get; set; }
        public string sendRef { get; set; }
        public Nullable<bool> envioExitoso { get; set; }
        public string error { get; set; }
        public string buzonDest { get; set; }
        public string estadoCadena { get; set; }
        public Nullable<int> id_Causal { get; set; }
        public string otraCausal { get; set; }
        public Nullable<bool> revisado { get; set; }
        public Nullable<System.DateTime> fechaAplicacion { get; set; }
        public Nullable<int> contadorRecepcion { get; set; }
        public Nullable<int> idEstado { get; set; }
        public string idErrorProceso { get; set; }
        public byte ID_TIPO_LINEA { get; set; }
    }
}
