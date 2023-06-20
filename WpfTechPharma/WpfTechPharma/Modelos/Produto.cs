using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTechPharma.Modelos
{
    internal class Produto
    { 
        public int id { get; set; }

        public string nome { get; set; }

        public string marca { get; set; }

        public float valor_compra { get; set; }

        public float valor_venda { get; set; }

        public int quantidade { get; set; }

        public string tipo { get; set; }

        public string codigo_barra { get; set; }

        public Fornecedor Fornecedor { get; set; }
    }
}
