using System;
using System.Collections.Generic;

namespace Agencia_Planes.Models
{
    public partial class PlanViaje
    {
        public PlanViaje()
        {
            Compras = new HashSet<Compra>();
        }

        public long CodigoPlan { get; set; }
        public string CodigoCiudad { get; set; } = null!;
        public string NombrePlan { get; set; } = null!;
        public string ActividadesIncluidas { get; set; } = null!;
        public int Costo { get; set; }
        public string IncluyeHospedaje { get; set; } = null!;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        public virtual Ciudade CodigoCiudad_PlanViaje { get; set; } = null!;
        public virtual ICollection<Compra> Compras { get; set; }
    }
}
