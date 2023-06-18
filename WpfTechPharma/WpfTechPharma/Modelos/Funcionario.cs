using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTechPharma.Modelos
{
    class Funcionario
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Sexo { get; set; }

        public DateTime? Nascimento { get; set; }

        public string RG { get; set; }

        public string CPF { get; set; }

        public string Email { get; set; }

        public string Contato { get; set; }

        public string Funcao { get; set; }

        public float Salario { get; set; }

        public Endereco Endereco { get; set; }
    }
}
