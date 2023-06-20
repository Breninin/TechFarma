using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTechPharma.Modelos
{
    internal class Medicamento
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Marca { get; set; }

        public string Peso_Volume { get; set; }

        public float Valor_Compra { get; set; }

        public float Valor_Venda { get; set; }

        public int Quantidade { get; set; }

        public string Tarja { get; set; }

        public string Codigo_Barra { get; set; }

        public Fornecedor Fornecedor { get; set; }
    }
}
