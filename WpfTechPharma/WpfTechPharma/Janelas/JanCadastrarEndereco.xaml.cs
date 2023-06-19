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
            // Adiciona manipuladores de eventos para os campos de texto
            edCEP.TextChanged += TextBox_TextChanged;
            edCidade.TextChanged += TextBox_TextChanged;
            edComplemento.TextChanged += TextBox_TextChanged;
            edBairro.TextChanged += TextBox_TextChanged;
            edNumer.TextChanged += TextBox_TextChanged;
            edRua.TextChanged += TextBox_TextChanged;

            // Adiciona manipulador de evento para a lista suspensa (ComboBox)
            cbUF.SelectionChanged += ComboBox_SelectionChanged;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            Ultis.Check(this, textBox); // Executa a verificação do campo de texto usando a classe Ultis
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            Ultis.Check(this, comboBox); // Executa a verificação da lista suspensa (ComboBox) usando a classe Ultis
        }

        private void btLimpar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Deseja realmente cancelar?", "Aviso", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                Ultis.ResetControls(this); // Limpa os campos usando a classe Ultis caso o usuário confirme o cancelamento
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            List<bool> check = new List<bool>
            {
                // Executa a verificação de todos os campos usando a classe Ultis e armazena o resultado em uma lista
                Ultis.Check(this, edCEP),
                Ultis.Check(this, edCidade),
                Ultis.Check(this, edComplemento),
                Ultis.Check(this, cbUF),
                Ultis.Check(this, edBairro),
                Ultis.Check(this, edNumer),
                Ultis.Check(this, edRua)
            };

            if (check.All(c => c)) // Verifica se todos os campos foram preenchidos corretamente (todos os elementos da lista são verdadeiros)
            {
                try
                {
                    // Cria um novo objeto de Endereco com base nos valores dos campos preenchidos
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
                    enderecoDAO.Insert(endereco); // Insere o objeto de Endereco no banco de dados usando a classe EnderecoDAO
                    MessageBox.Show("Endereço inserido com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao inserir o endereço: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                Ultis.ResetControls(this); // Limpa os campos usando a classe Ult
            }
            else
            {
                check.Clear(); // Limpa a lista de verificação, pois nem todos os campos foram preenchidos corretamente
            }
        }
    }
}