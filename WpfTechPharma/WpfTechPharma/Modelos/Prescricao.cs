using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTechPharma.Modelos
{
    class Prescricao
    {
        public int Id { get; set; }

        public DateTime? Data { get; set; }

        public string Patologia { get; set; }

        public DateTime? Vencimento { get; set; }

        public string NomeEmissor { get; set; }

        public string ClinicaEmissora { get; set; }

        public string ScanDocumento { get; set; }

        public Cliente Cliente { get; set; }

        public Medicamento Medicamento { get; set; }

        public Funcionario Funcionario { get; set; }

        public Venda Venda { get; set; }
    }
}
