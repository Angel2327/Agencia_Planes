using System;
using System.Collections.Generic;

namespace Agencia_Planes.Models
{
    public partial class Compra
    {
        public DateTime FechaCompra { get; set; }
        public int Cedula { get; set; }
        public long CodigoPlan { get; set; }
        public string CodigoDeVuelo { get; set; } = null!;
        public long PrecioPagado { get; set; }
        public int NumeroPersonas { get; set; }

        public virtual Persona Cedula_Compra { get; set; } = null!;
        public virtual TransporteEnAvion CodigoDeVuelo_Compra { get; set; } = null!;
        public virtual PlanViaje CodigoPlan_Compra { get; set; } = null!;
    }
}
