using System;

namespace WpfTechPharma.Modelos
{
    class Cliente
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Sexo { get; set; }

        public DateTime? Nascimento { get; set; }

        public string RG { get; set; }

        public string CPF { get; set; }

        public string Email { get; set; }

        public string Contato { get; set; }

        public Endereco Endereco { get; set; }
    }
}
