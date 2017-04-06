namespace LOGYCAHUB.Billing.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using LOGYCAHUB.Billing.DAL;

    public class EmpresaEntity
    {
        public decimal ID_EMPRESA { get; set; }
        public decimal ID_ROL { get; set; }
        public string NOMBRE { get; set; }
        public string NIT { get; set; }
        public string EAN_EMPRESA_GLN { get; set; }
        public int ID_TIPO_CANAL { get; set; }
        public string BUZON_X_CANAL { get; set; }
        public string PASSWORD_VAN { get; set; }
        public int ID_FORMATO_DOCUMENTO { get; set; }
        public string ID_CIUDAD { get; set; }
        public bool ACTIVO { get; set; }
        public decimal CONTADOR_ID_ARTICULO { get; set; }
        public decimal CONTADOR_ALC_ID_GRUPO_DESCUENTO { get; set; }
        public decimal CONTADOR_ID_TIPO_CLIENTE { get; set; }
        public decimal CONTADOR_ID_PUNTOVENTA { get; set; }
        public string URL { get; set; }
        public string IMG_EMPRESA { get; set; }
        public string CONTACTO_EMP { get; set; }
        public string TELEFONO { get; set; }
        public string DIRECCION { get; set; }
        public string FAX { get; set; }
        public string EMAIL { get; set; }
        public string EMAIL_CONTACTO { get; set; }
        public bool BLOQUEADO { get; set; }
        public Nullable<bool> COPIADO { get; set; }
        public decimal CONTADOR_ID_COMPRADOR { get; set; }
        public string ID_PAIS { get; set; }
        public bool VECTORIZACION { get; set; }
        public string GLN_BUZON { get; set; }
        public Nullable<bool> REGISTRO_GDSN { get; set; }
        public Nullable<bool> ENVIAR_FOTOS { get; set; }
        public Nullable<bool> PERMITEENVIOFTP { get; set; }
        public Nullable<bool> REG_GLOBALREGISTRY { get; set; }
        public Nullable<decimal> CONTADOR_ID_DOCUMENTO { get; set; }
        public string CELULAR { get; set; }
        public string CODIGO_POSTAL { get; set; }
        public string BARRIO { get; set; }
        public string DIRECCION_NUMERO { get; set; }
        public string DIRECCION_COMPLEMENTO { get; set; }
        public byte ID_TIPO_LINEA { get; set; }

        public virtual CRM_EMPRESA CRM_EMPRESA { get; set; }
    }
}
