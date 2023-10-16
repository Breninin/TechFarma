using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTechPharma.Modelos
{
    class Venda
    {
        public int Id { get; set; }

        public DateTime? Data { get; set; }

        public float Valor { get; set; }

        public float Desconto { get; set; }

        public int QuantidadeParcelas { get; set; }

        public Cliente Cliente { get; set; }

        public Funcionario Funcionario { get; set; }
    }
}
