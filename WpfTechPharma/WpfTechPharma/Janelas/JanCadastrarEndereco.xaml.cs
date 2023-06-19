using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfTechPharma.Auxiliares;
using WpfTechPharma.Modelos;

namespace WpfTechPharma.Janelas
{
    public partial class JanCadastrarEndereco : Window
    {
        public JanCadastrarEndereco()
        {
            InitializeComponent();
            InicializarManipuladoresEventos();
        }

        private void InicializarManipuladoresEventos()
        {
            edCEP.TextChanged += TextBox_TextChanged;
            edCidade.TextChanged += TextBox_TextChanged;
            edComplemento.TextChanged += TextBox_TextChanged;
            cbUF.SelectionChanged += ComboBox_SelectionChanged;
            edBairro.TextChanged += TextBox_TextChanged;
            edNumer.TextChanged += TextBox_TextChanged;
            edRua.TextChanged += TextBox_TextChanged;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            Ultis.Check(this, textBox);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            Ultis.Check(this, comboBox);
        }

        private void btLimpar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Deseja realmente cancelar?", "Aviso", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                Ultis.ResetControls(this);
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            List<bool> check = new List<bool>
            {
                Ultis.Check(this, edCEP),
                Ultis.Check(this, edCidade),
                Ultis.Check(this, edComplemento),
                Ultis.Check(this, cbUF),
                Ultis.Check(this, edBairro),
                Ultis.Check(this, edNumer),
                Ultis.Check(this, edRua)
            };

            if (check.All(c => c))
            {
                try
                {
                    var endereco = new Endereco
                    {
                        CEP = edCEP.Text,
                        Cidade = edCidade.Text,
                        Complemento = edComplemento.Text,
                        Estado = cbUF.Text,
                        Bairro = edBairro.Text,
                        Numero = Convert.ToInt32(edNumer.Text),
                        Rua = edRua.Text
                    };

                    var enderecoDAO = new EnderecoDAO();
                    enderecoDAO.Insert(endereco);
                    MessageBox.Show("Endereço inserido com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao inserir o endereço: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                Ultis.ResetControls(this);
            }
            else
            {
                check.Clear();
            }
        }
    }
}
