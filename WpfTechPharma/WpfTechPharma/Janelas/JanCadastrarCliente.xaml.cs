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
    /// Lógica interna para JanCadastrarCliente.xaml
    /// </summary>
    public partial class JanCadastrarCliente : Window
    {
        private int _id = 0;
        private Cliente _cliente = new Cliente();
        private bool _update = false;

        public JanCadastrarCliente()
        {
            InitializeComponent();
            InicializarManipuladoresEventos();
            LoadData();
        }

        public JanCadastrarCliente(int id)
        {
            _id = id;

            InitializeComponent();
            InicializarManipuladoresEventos();
            LoadData();
            FillForm();
        }

        // Inicializa os manipuladores de eventos
        private void InicializarManipuladoresEventos()
        {
            edNome.TextChanged += TextBox_TextChanged;
            edContato.TextChanged += TextBox_TextChanged;
            edRG.TextChanged += TextBox_TextChanged;
            edEmail.TextChanged += TextBox_TextChanged;
            cbEndereco.SelectionChanged += ComboBox_SelectionChanged;
            cbSexo.SelectionChanged += ComboBox_SelectionChanged;
            edCPF.TextChanged += TextBox_TextChanged_CPF;
            dpDataNascimento.SelectedDateChanged += DatePicker_SelectedDateChanged;
        }

        // Manipulador de eventos para o evento TextChanged dos controles TextBox
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            Utils.Check(this, textBox);
        }

        // Manipulador de evento para a alteração do texto de CPF
        private void TextBox_TextChanged_CPF(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            Utils.Check(this, textBox, 11);
        }

        // Manipulador de eventos para o evento SelectionChanged dos controles ComboBox
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            Utils.Check(this, comboBox);
        }

        // Manipulador de eventos para o evento SelectedDateChanged dos controles DatePicker
        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DatePicker datePicker = (DatePicker)sender;
            Utils.Check(this, datePicker);
        }

        private void FillForm()
        {
            var clienteDAO = new ClienteDAO();
            _cliente = clienteDAO.GetById(_id);

            var enderecoDAO = new EnderecoDAO();
            var endereco = enderecoDAO.GetById(_cliente.Endereco.Id);

            edNome.Text = _cliente.Nome;
            edEmail.Text = _cliente.Email;
            edContato.Text = _cliente.Contato;
            edRG.Text = _cliente.RG;
            edCPF.Text = _cliente.CPF.Replace("_", "").Replace(".", "").Replace("-", "").Replace(",", "");
            dpDataNascimento.SelectedDate = (DateTime)_cliente.Nascimento;
            cbEndereco.Text = endereco.CEP;
            cbSexo.Text = _cliente.Sexo;

            _update = true;
        }

        // Carrega os dados iniciais
        private void LoadData()
        {
            try
            {
                cbEndereco.ItemsSource = null;
                cbEndereco.Items.Clear();
                cbEndereco.ItemsSource = new EnderecoDAO().List();
                cbEndereco.DisplayMemberPath = "CEP";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Não Executado", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btLimpar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Deseja realmente limpar?", "Aviso", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Utils.ResetControls(this);
                _update = false;
            }
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            List<bool> check = new List<bool>
            {
                Utils.Check(this, edNome),
                Utils.Check(this, edContato),
                Utils.Check(this, edCPF, 11),
                Utils.Check(this, edRG),
                Utils.Check(this, edEmail),
                Utils.Check(this, cbEndereco),
                Utils.Check(this, cbSexo),
                Utils.Check(this, dpDataNascimento)
            };

            if (check.All(c => c))
            {
                try
                {
                    if (_update)
                    {
                        var enderecoDAO = new EnderecoDAO();

                        var cliente = new Cliente
                        {
                            Id = _id,
                            Nome = edNome.Text,
                            Email = edEmail.Text,
                            Contato = edContato.Text,
                            RG = edRG.Text,
                            CPF = edCPF.Text.Replace(",", "."),
                            Nascimento = (DateTime)dpDataNascimento.SelectedDate,
                            Sexo = cbSexo.Text,
                            Endereco = enderecoDAO.GetById(cbEndereco.SelectedIndex+1)
                        };

                        var clienteDAO = new ClienteDAO();

                        clienteDAO.Update(cliente);
                        MessageBox.Show("Cliente atualizado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                        _update = false;
                    }
                    else
                    {
                        var cliente = new Cliente
                        {
                            Nome = edNome.Text,
                            Email = edEmail.Text,
                            Contato = edContato.Text,
                            RG = edRG.Text,
                            CPF = edCPF.Text.Replace(",","."),
                            Nascimento = (DateTime)dpDataNascimento.SelectedDate,
                            Sexo = cbSexo.Text,
                            Endereco = (Endereco)cbEndereco.SelectedItem
                        };

                        var clienteDAO = new ClienteDAO();
                        var resultado = clienteDAO.Insert(cliente);
                        MessageBox.Show(resultado, "Resultado", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao inserir o cliente: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                Utils.ResetControls(this);
                this.Close();
            }
            else
            {
                check.Clear();
            }
        }

        private void edEmail_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
