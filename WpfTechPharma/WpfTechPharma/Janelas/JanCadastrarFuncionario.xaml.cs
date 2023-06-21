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
        public JanCadastrarFuncionario()
        {
            InitializeComponent();
            InitializeEventHandlers();
            LoadData();
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
                Ultis.ResetControls(this);
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
                    var funcionario = new Funcionario
                    {
                        Nome = edNome.Text,
                        Sexo = cbSexo.Text,
                        Nascimento = (DateTime)dpDataNascimento.SelectedDate,
                        RG = edRG.Text,
                        CPF = edCPF.Text,
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
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao inserir o funcionário: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
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
