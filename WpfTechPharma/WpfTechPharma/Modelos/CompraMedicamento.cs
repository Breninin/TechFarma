using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTechPharma.Modelos
{
    class CompraMedicamento
    {
        public int Id { get; set; }

        public int QuantidadeItem { get; set; }

        public float ValorItem { get; set; }

        public Compra Compra { get; set; }

        public Medicamento Medicamento { get; set; }
    }
}
