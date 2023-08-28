using FluentValidation;
using System.Linq;
using System.Windows.Controls;

namespace WpfTechPharma.Auxiliares
{
    public static class AuxiliarFluentValidation
    {
        public static (bool isValid, string error) CPF(string cpf)
        {
            CpfValidation validator = new CpfValidation();
            var result = validator.Validate(cpf);

            if (!result.IsValid)
            {
                string errors = string.Join("\n", result.Errors.Select(e => e.ErrorMessage));
                return (false, errors);
            }

            return (true, "");
        }

        public static (bool isValid, string error) CNPJ(string cnpj)
        {
            CnpjValidation validator = new CnpjValidation();
            var result = validator.Validate(cnpj);

            if (!result.IsValid)
            {
                string errors = string.Join("\n", result.Errors.Select(e => e.ErrorMessage));
                return (false, errors);
            }

            return (true, "");
        }

        public static (bool isValid, string error) Email(string email)
        {
            EmailValidation validator = new EmailValidation();
            var result = validator.Validate(email);

            if (!result.IsValid)
            {
                string errors = string.Join("\n", result.Errors.Select(e => e.ErrorMessage));
                return (false, errors);
            }

            return (true, "");
        }

        public static (bool isValid, string error) RG(string rg)
        {
            RgValidation validator = new RgValidation();
            var result = validator.Validate(rg);

            if (!result.IsValid)
            {
                string errors = string.Join("\n", result.Errors.Select(e => e.ErrorMessage));
                return (false, errors);
            }

            return (true, "");
        }
        public static (bool isValid, string error) ComboBox(ComboBox comboBox)
        {
            if (comboBox.SelectedItem == null)
            {
                return (false, "Selecione algum item");
            }
            return (true, "");
        }

        public static (bool isValid, string error) Texto(string text, int size)
        {
            StringValidation validator = new StringValidation(size);
            var result = validator.Validate(text);

            if (!result.IsValid)
            {
                string errors = string.Join("\n", result.Errors.Select(e => e.ErrorMessage));
                return (false, errors);
            }

            return (true, "");
        }
    }

    public class CpfValidation : AbstractValidator<string>
    {
        public CpfValidation()
        {
            RuleFor(cpf => cpf)
                .NotEmpty().WithMessage("não pode estar vazio")
                .Length(11).WithMessage("deve conter 11 dígitos")
                .IsValidCPF().WithMessage("CPF inválido");
        }
    }

    public class CnpjValidation : AbstractValidator<string>
    {
        public CnpjValidation()
        {
            RuleFor(cnpj => cnpj)
                .NotEmpty().WithMessage("não pode estar vazio")
                .Length(14).WithMessage("deve conter 11 dígitos")
                .IsValidCNPJ().WithMessage("CNPJ inválido");
        }
    }

    public class EmailValidation : AbstractValidator<string>
    {
        public EmailValidation()
        {
            RuleFor(email => email)
                .NotEmpty().WithMessage("não pode estar vazio")
                .EmailAddress().WithMessage("deve ser um endereço de email válido")
                .Must(BeValidEmail).WithMessage("email inválido");
        }

        private bool BeValidEmail(string email)
        {
            string[] validDomains = new string[]
            {
                "@gmail.com", "@outlook.com", "@hotmail.com", "@live.com", "@yahoo.com",
                "@yahoo.com.br", "@icloud.com", "@aol.com", "@protonmail.com", "@zoho.com",
                "@yandex.com", "@mail.com", "@gmx.com", "@fastmail.com", "@tutanota.com", "@mail.ru"
            };

            return validDomains.Any(domain => email.EndsWith(domain));
        }
    }

    public class RgValidation : AbstractValidator<string>
    {

        public RgValidation()
        {
            RuleFor(rg => rg)
              .NotEmpty().WithMessage("não pode estar vazio")
              .Length(9).WithMessage("deve conter 9 dígitos")
              .Must(BeValidRG).WithMessage("RG inválido");

        }
        private bool BeValidRG(string rg)
        {
            int estadoEmissivo = int.Parse(rg.Substring(0, 2));
            if (estadoEmissivo < 1 || estadoEmissivo > 26)
                return false;

            int soma = 0;
            for (int i = 0; i < 8; i++)
                soma += (rg[i] - '0') * (9 - i);

            int digitoVerificador = rg[8] - '0';
            return soma % 11 == digitoVerificador;
        }
    }
    public class StringValidation : AbstractValidator<string>
    {
        public StringValidation(int size)
        {
            RuleFor(text => text)
                .NotEmpty().WithMessage("não pode estar vazia")
                .Must(text => HaveMoreThanSizeLetters(text, size)).WithMessage($"deve conter mais de {size} letra(s)");
        }

        private bool HaveMoreThanSizeLetters(string text, int size)
        {
            return text.Count(char.IsLetter) > size;
        }
    }
}
