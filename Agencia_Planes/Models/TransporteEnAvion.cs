using System;
using System.Collections.Generic;

namespace Agencia_Planes.Models
{
    public partial class TransporteEnAvion
    {
        public TransporteEnAvion()
        {
            Compras = new HashSet<Compra>();
        }

        public string CodigoDeVuelo { get; set; } = null!;
        public string Abordo { get; set; } = null!;
        public string Aeroliena { get; set; } = null!;
        public DateTime HoraSalida { get; set; }
        public DateTime HoraLlegada { get; set; }

        public virtual ICollection<Compra> Compras { get; set; }
    }
}
