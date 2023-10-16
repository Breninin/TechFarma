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
    /// Lógica interna para JanCadastrarFuncionario.xaml
    /// </summary>
    public partial class JanCadastrarFuncionario : Window
    {
        private int _id = 0;
        private Funcionario _funcionario = new Funcionario();
        private bool _update = false;

        public JanCadastrarFuncionario()
        {
            InitializeComponent();
            InitializeEventHandlers();
            LoadData();
        }

        public JanCadastrarFuncionario(int id)
        {
            _id = id;

            InitializeComponent();
            InitializeEventHandlers();
            LoadData();
            FillForm();
        }

        // Inicializa os manipuladores de eventos
        private void InitializeEventHandlers()
        {
            edNome.TextChanged += TextBox_TextChanged;
            edContato.TextChanged += TextBox_TextChanged;
            edRG.TextChanged += TextBox_TextChanged;
            edEmail.TextChanged += TextBox_TextChanged;
            cbEndereco.SelectionChanged += ComboBox_SelectionChanged;
            cbSexo.SelectionChanged += ComboBox_SelectionChanged;
            edCPF.TextChanged += TextBox_TextChanged_CPF;
            dpDataNascimento.SelectedDateChanged += DatePicker_SelectedDateChanged;
            edfuncao.TextChanged += TextBox_TextChanged;
            edsalario.TextChanged += TextBox_TextChanged;
        }

        // Manipulador de evento para a alteração do texto nos campos TextBox
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            Ultis.Check(this, textBox);
        }

        // Manipulador de evento para a alteração do texto de CPF
        private void TextBox_TextChanged_CPF(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            Ultis.Check(this, textBox, 11);
        }

        // Manipulador de evento para a alteração da seleção no ComboBox
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            Ultis.Check(this, comboBox);
        }

        // Manipulador de evento para a alteração da data selecionada no DatePicker
        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DatePicker datePicker = (DatePicker)sender;
            Ultis.Check(this, datePicker);
        }

        private void FillForm()
        {
            var funcionarioDAO = new FuncionarioDAO();
            var _funcionario = funcionarioDAO.GetById(_id);

            var enderecoDAO = new EnderecoDAO();
            var endereco = enderecoDAO.GetById(_funcionario.Endereco.Id);

            edNome.Text = _funcionario.Nome;
            cbSexo.Text = _funcionario.Sexo;
            dpDataNascimento.SelectedDate = (DateTime)_funcionario.Nascimento;
            edRG.Text = _funcionario.RG;
            edCPF.Text = _funcionario.CPF.Replace("_", "").Replace(".", "").Replace("-", "").Replace(",", "");
            edEmail.Text = _funcionario.Email;
            edContato.Text = _funcionario.Contato;
            edfuncao.Text = _funcionario.Funcao;
            edsalario.Text = _funcionario.Salario.ToString();
            cbEndereco.SelectedIndex = (endereco.Id - 1);

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
                Ultis.ResetControls(this);
                _update = false;
            }
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            List<bool> check = new List<bool>
            {
                Ultis.Check(this, edNome),
                Ultis.Check(this, edContato),
                Ultis.Check(this, edCPF, 11),
                Ultis.Check(this, edRG),
                Ultis.Check(this, edEmail),
                Ultis.Check(this, cbEndereco),
                Ultis.Check(this, cbSexo),
                Ultis.Check(this, dpDataNascimento),
                Ultis.Check(this, edfuncao),
                Ultis.Check(this, edsalario)
            };

            if (check.All(c => c))
            {
                try
                {
                    if (_update)
                    {
                        var enderecoDAO = new EnderecoDAO();

                        var funcionario = new Funcionario
                        {
                            Id = _id,
                            Nome = edNome.Text,
                            Sexo = cbSexo.Text,
                            Nascimento = (DateTime)dpDataNascimento.SelectedDate,
                            RG = edRG.Text,
                            CPF = edCPF.Text.Replace(",", "."),
                            Email = edEmail.Text,
                            Contato = edContato.Text,
                            Funcao = edfuncao.Text,
                            Salario = float.Parse(edsalario.Text),
                            Endereco = enderecoDAO.GetById(cbEndereco.SelectedIndex + 1)
                        };

                        var funcionarioDAO = new FuncionarioDAO();

                        funcionarioDAO.Update(funcionario);
                        MessageBox.Show("Funcionario atualizado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                        _update = false;
                    }
                    else
                    {
                        var funcionario = new Funcionario
                        {
                            Nome = edNome.Text,
                            Sexo = cbSexo.Text,
                            Nascimento = (DateTime)dpDataNascimento.SelectedDate,
                            RG = edRG.Text,
                            CPF = edCPF.Text.Replace(",", "."),
                            Email = edEmail.Text,
                            Contato = edContato.Text,
                            Funcao = edfuncao.Text,
                            Salario = float.Parse(edsalario.Text),
                            Endereco = (Endereco)cbEndereco.SelectedItem
                        };

                        var funcionarioDAO = new FuncionarioDAO();

                        funcionarioDAO.Insert(funcionario);
                        MessageBox.Show("Funcionario inserido com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao inserir o funcionário: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                Ultis.ResetControls(this);
                this.Close();
            }
            else
            {
                check.Clear();
            }
        }
    }
}
