using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTechPharma.Modelos
{
    class Compra
    {
        public int Id { get; set; }

        public DateTime? Data { get; set; }

        public float Valor { get; set; }

        public Despesa Despesa { get; set; }
    }
}
