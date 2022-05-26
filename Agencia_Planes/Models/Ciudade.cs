using System;
using System.Collections.Generic;

namespace Agencia_Planes.Models
{
    public partial class Ciudade
    {
        public Ciudade()
        {
            PlanViajes = new HashSet<PlanViaje>();
        }

        public string CodigoCiudad { get; set; } = null!;
        public string NombreCiudad { get; set; } = null!;
        public string Geografia { get; set; } = null!;
        public string Cultura { get; set; } = null!;
        public string Clima { get; set; } = null!;
        public string Geolocalizacion { get; set; } = null!;

        public virtual ICollection<PlanViaje> PlanViajes { get; set; }
    }
}
