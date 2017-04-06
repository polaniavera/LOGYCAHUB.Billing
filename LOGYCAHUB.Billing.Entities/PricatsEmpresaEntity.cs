using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOGYCAHUB.Billing.Entities
{
    public class PricatsEmpresaEntity
    {
        public string EAN_EMPRESA_GLN { get; set; }

        public int ID_EMPRESA { get; set; }

        public string ID_PAIS { get; set; }

        public int NUM_PRICATS_COMPRADOS { get; set; }

        public int NUM_PRICATS_PROCESADOS { get; set; }

        public Nullable<System.DateTime> FECHA_EXPIRACION { get; set; }

        public Nullable<bool> DESCONTAR_PRICAT { get; set; }

        public Nullable<System.DateTime> FECHA_ACTUALIZACION { get; set; }

        public Nullable<System.DateTime> FECHA_VENCIMIENTO_ORDEN { get; set; }

        public Nullable<int> ID_ORDEN_COMPRA { get; set; }
    }
}
