using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTechPharma.Modelos
{
    class VendaMedicamento
    {
        public int Id { get; set; }

        public int QuantidadeItem { get; set; }

        public float ValorItem { get; set; }

        public Venda Venda { get; set; }

        public Medicamento Medicamento { get; set; }
    }
}
