using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alkanza.businessobjects
{
    public class Marcador
    {
        public Guid id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public decimal latitud { get; set; }
        public decimal longitud { get; set; }
        public int distancia { get; set; }
        public bool esCentroMedico { get; set; }

    }
}
