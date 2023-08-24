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
        public static (bool isValid, string error) ComboBox(ComboBox comboBox)
        {
            if (comboBox.SelectedItem == null)
            {
                return (false, "Selecione algum item");
            }
            return (true, "");
        }

        public static (bool isValid, string error) Texto(string text)
        {
            StringValidation validator = new StringValidation();
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

    public class StringValidation : AbstractValidator<string>
    {
        public StringValidation()
        {
            RuleFor(text => text)
                .NotEmpty().WithMessage("não pode estar vazia")
                .Must(HaveMoreThanOneLetter).WithMessage("deve conter mais de 1 letra");
        }

        private bool HaveMoreThanOneLetter(string text)
        {
            var lettersCount = text.Count(char.IsLetter);
            return lettersCount > 1;
        }
    }
}
