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
    /// Lógica interna para JanCadastrarFornecedor.xaml
    /// </summary>
    public partial class JanCadastrarFornecedor : Window
    {
        public JanCadastrarFornecedor()
        {
            InitializeComponent();
            InitializeEventHandlers();
            LoadData();
        }

        // Inicializa os manipuladores de eventos para os campos de texto e seleção
        private void InitializeEventHandlers()
        {
            edRazaoSocial.TextChanged += TextBox_TextChanged;
            edNomeFantasia.TextChanged += TextBox_TextChanged;
            edCnpj.TextChanged += TextBox_TextChanged;
            edContato.TextChanged += TextBox_TextChanged;
            edEmail.TextChanged += TextBox_TextChanged;
            cbEndereco.SelectionChanged += ComboBox_SelectionChanged;
            edInscricaoEstadual.TextChanged += TextBox_TextChanged;
        }

        // Manipulador de evento para a alteração do texto nos campos TextBox
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            Ultis.Check(this, textBox);
        }

        // Manipulador de evento para a alteração da seleção no ComboBox
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            Ultis.Check(this, comboBox);
        }

        // Carrega os dados iniciais do ComboBox cbEndereco
        private void LoadData()
        {
            try
            {
                cbEndereco.ItemsSource = null; // Remove a origem de dados atual
                cbEndereco.Items.Clear(); // Remove todos os itens existentes
                cbEndereco.ItemsSource = new EnderecoDAO().List(); // Define uma nova origem de dados
                cbEndereco.DisplayMemberPath = "CEP"; // Define a propriedade a ser exibida nos itens do ComboBox
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Não Executado", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Limpa os campos quando o botão "Limpar" é clicado
        private void btLimpar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Deseja realmente limpar?", "Aviso", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                Ultis.ResetControls(this);
        }

        // Salva os dados do fornecedor quando o botão "Salvar" é clicado
        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            List<bool> check = new List<bool>
            {
                Ultis.Check(this, edRazaoSocial),
                Ultis.Check(this, edNomeFantasia),
                Ultis.Check(this, edCnpj),
                Ultis.Check(this, edContato),
                Ultis.Check(this, edEmail),
                Ultis.Check(this, cbEndereco),
                Ultis.Check(this, edInscricaoEstadual)
            };

            if (check.All(c => c))
            {
                try
                {
                    var fornecedor = new Fornecedor
                    {
                        RazaoSocial = edRazaoSocial.Text,
                        NomeFantasia = edNomeFantasia.Text,
                        CNPJ = edCnpj.Text,
                        Contato = edContato.Text,
                        Email = edEmail.Text,
                        Endereco = (Endereco)cbEndereco.SelectedItem,
                        InscrcaoEstadual = edInscricaoEstadual.Text
                    };
                    var fornecedorDAO = new FornecedorDAO();
                    fornecedorDAO.Insert(fornecedor);
                    MessageBox.Show("Fornecedor inserido com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao inserir o fornecedor: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
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
