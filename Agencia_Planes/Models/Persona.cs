using System;
using System.Collections.Generic;

namespace Agencia_Planes.Models
{
    public partial class Persona
    {
        public Persona()
        {
            Compras = new HashSet<Compra>();
        }

        public int Cedula { get; set; }
        public string Nombre { get; set; } = null!;
        public short Edad { get; set; }
        public string Usuario { get; set; } = null!;
        public string Contraseña { get; set; } = null!;
        public string Correo { get; set; } = null!;

        public virtual ICollection<Compra> Compras { get; set; }
    }
}
