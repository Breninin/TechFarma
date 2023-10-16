using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTechPharma.Modelos
{
    class Servico
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Duracao { get; set; }

        public string Tipo { get; set; }

        public float ValorVenda { get; set; }
    }
}