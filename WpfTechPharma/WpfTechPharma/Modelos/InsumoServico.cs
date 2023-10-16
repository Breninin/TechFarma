using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTechPharma.Modelos
{
    class InsumoServico
    {
        public int Id { get; set; }

        public int QuantidadeInsumo { get; set; }

        public Insumo Insumo { get; set; }

        public Servico Servico { get; set; }
    }
}
