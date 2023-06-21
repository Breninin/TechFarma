using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTechPharma.Modelos
{
    internal class Produto
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Marca { get; set; }

        public float ValorCompra { get; set; }

        public float ValorVenda { get; set; }

        public int Quantidade { get; set; }

        public string Tipo { get; set; }

        public string CodigoBarra { get; set; }

        public Fornecedor Fornecedor { get; set; }
    }
}