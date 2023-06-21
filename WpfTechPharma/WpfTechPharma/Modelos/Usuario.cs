using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTechPharma.Modelos
{
    internal class Usuario
    {
        public int Id { get; set; }

        public string NomeUsuario { get; set; }

        public string Senha { get; set; }

        public Funcionario Funcionario { get; set; }
    }
}
