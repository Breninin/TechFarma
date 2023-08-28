using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTechPharma.Modelos
{
    class Pagamento
    {
        public int Id { get; set; }

        public DateTime? Data { get; set; }

        public float Valor { get; set; }

        public string FormaPagamento { get; set; }

        public string Status { get; set; }

        public DateTime? Vencimento { get; set; }

        public int NumeroParcela { get; set; }

        public Despesa Despesa { get; set; }

        public Caixa Caixa { get; set; }
    }
}
