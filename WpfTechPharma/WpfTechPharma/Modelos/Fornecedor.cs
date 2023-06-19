using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTechPharma.Modelos
{
    class Fornecedor
    {
        public int Id { get; set; }

        public string RazaoSocial { get; set; }

        public string NomeFantasia { get; set; }

        public string Contato { get; set; }

        public string CNPJ { get; set; }

        public string Email { get; set; }

        public Endereco Endereco { get; set; }

        public string InscrcaoEstado { get; set; }
    }
}
