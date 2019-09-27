using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alkanza.businessobjects
{
    public class Historico
    {
        public Guid id { get; set; }
        public int radio { get; set; }
        public decimal latitud { get; set; }
        public decimal longitud { get; set; }
        public decimal resultado { get; set; }

        public DateTime fecha { get; set; }

        public List<Marcador> listMarcadores { get; set; }

        public Historico()
        {
            listMarcadores = new List<Marcador>();
        }
    }
}
