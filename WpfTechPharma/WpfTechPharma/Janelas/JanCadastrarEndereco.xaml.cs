using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfTechPharma.Auxiliares;
using WpfTechPharma.Modelos;

namespace WpfTechPharma.Janelas
{
    /// <summary>
    /// Lógica interna para JanCadastrarEndereco.xaml
    /// </summary>
    public partial class JanCadastrarEndereco : Window
    {
        private int _id = 0;
        private Endereco _endereco = new Endereco();
        private bool _update = false;

        public JanCadastrarEndereco()
        {
            InitializeComponent();
            InicializarManipuladoresEventos();
        }

        public JanCadastrarEndereco(int id)
        {
            _id = id;

            InitializeComponent();
            InicializarManipuladoresEventos();
            FillForm();
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
            Utils.Check(this, textBox); // Executa a verificação do campo de texto usando a classe Ultis
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            Utils.Check(this, comboBox); // Executa a verificação da lista suspensa (ComboBox) usando a classe Ultis
        }

        private void FillForm()
        {
            var enderecoDAO = new EnderecoDAO();
            _endereco = enderecoDAO.GetById(_id);

            edCEP.Text = _endereco.CEP;
            edCidade.Text = _endereco.Cidade;
            edComplemento.Text = _endereco.Complemento;
            cbUF.Text = _endereco.Estado;
            edBairro.Text = _endereco.Bairro;
            edNumer.Text = _endereco.Numero.ToString();
            edRua.Text = _endereco.Rua;

            _update = true;
        }

        private void btLimpar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Deseja realmente cancelar?", "Aviso", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Utils.ResetControls(this); // Limpa os campos usando a classe Ultis caso o usuário confirme o cancelamento
                _update = false;
            }
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            List<bool> check = new List<bool>
            {
                // Executa a verificação de todos os campos usando a classe Ultis e armazena o resultado em uma lista
                Utils.Check(this, edCEP),
                Utils.Check(this, edCidade),
                Utils.Check(this, edComplemento),
                Utils.Check(this, cbUF),
                Utils.Check(this, edBairro),
                Utils.Check(this, edNumer),
                Utils.Check(this, edRua)
            };

            if (check.All(c => c)) // Verifica se todos os campos foram preenchidos corretamente (todos os elementos da lista são verdadeiros)
            {
                try
                {
                    if (_update)
                    {
                        // Cria um novo objeto de Endereco com base nos valores dos campos preenchidos
                        var endereco = new Endereco
                        {
                            Id = _id,
                            CEP = edCEP.Text,
                            Cidade = edCidade.Text,
                            Complemento = edComplemento.Text,
                            Estado = cbUF.Text,
                            Bairro = edBairro.Text,
                            Numero = Convert.ToInt32(edNumer.Text),
                            Rua = edRua.Text
                        };

                        var enderecoDAO = new EnderecoDAO();

                        enderecoDAO.Update(endereco); // Insere o objeto de Endereco no banco de dados usando a classe EnderecoDAO
                        MessageBox.Show("Endereço atualizado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                        _update = false;
                    }
                    else
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
                        var resultado = enderecoDAO.Insert(endereco); // Insere o objeto de Endereco no banco de dados usando a classe EnderecoDAO
                        MessageBox.Show(resultado, "Resultado", MessageBoxButton.OK, MessageBoxImage.Information);
                    } 
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao inserir o endereço: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                Utils.ResetControls(this); // Limpa os campos usando a classe Ult
                this.Close();
            }
            else
            {
                check.Clear(); // Limpa a lista de verificação, pois nem todos os campos foram preenchidos corretamente
            }
        }
    }
}
