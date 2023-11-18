using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTechPharma.Modelos
{
    class Caixa
    {
        public int Id { get; set; }

        public int Numero { get; set; }

        public DateTime? Data { get; set; }

        public string HorarioInicial { get; set; }

        public string HorarioFinal { get; set; }

        public string Status { get; set; }

        public float SaldoInicial { get; set; }

        public float SaldoFinal { get; set; }

        public float TotalEntrada { get; set; }

        public float TotalSaida { get; set; }
    }
}
