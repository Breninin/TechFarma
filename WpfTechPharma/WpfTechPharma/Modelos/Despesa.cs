using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTechPharma.Modelos
{
    class Despesa
    {
        public int Id { get; set; }

        public DateTime? Data { get; set; }

        public float Valor { get; set; }

        public string Descricao { get; set; }

        public string Tipo { get; set; }

        public int QuantidadeParcelas { get; set; }
    }
}
