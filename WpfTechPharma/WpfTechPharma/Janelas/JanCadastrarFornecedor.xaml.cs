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
        private int _id = 0;
        private Fornecedor _fornecedor = new Fornecedor();
        private bool _update = false;

        public JanCadastrarFornecedor()
        {
            InitializeComponent();
            InitializeEventHandlers();
            LoadData();
        }

        public JanCadastrarFornecedor(int id)
        {
            _id = id;

            InitializeComponent();
            InitializeEventHandlers();
            LoadData();
            FillForm();
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
        }

        // Manipulador de evento para a alteração do texto nos campos TextBox
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            Utils.Check(this, textBox);
        }

        // Manipulador de evento para a alteração da seleção no ComboBox
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            Utils.Check(this, comboBox);
        }

        private void FillForm()
        {
            var fornecedorDAO = new FornecedorDAO();
            _fornecedor = fornecedorDAO.GetById(_id);

            var enderecoDAO = new EnderecoDAO();
            var endereco = enderecoDAO.GetById(_fornecedor.Endereco.Id);

            edRazaoSocial.Text = _fornecedor.RazaoSocial;
            edNomeFantasia.Text = _fornecedor.NomeFantasia;
            edCnpj.Text = _fornecedor.CNPJ;
            edContato.Text = _fornecedor.Contato;
            edEmail.Text = _fornecedor.Email;
            cbEndereco.Text = endereco.CEP;

            _update = true;
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

        private void btLimpar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Deseja realmente limpar?", "Aviso", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Utils.ResetControls(this);
                _update = false;
            }
        }

        // Salva os dados do fornecedor quando o botão "Salvar" é clicado
        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            List<bool> check = new List<bool>
            {
                Utils.Check(this, edRazaoSocial),
                Utils.Check(this, edNomeFantasia),
                Utils.Check(this, edCnpj),
                Utils.Check(this, edContato),
                Utils.Check(this, edEmail),
                Utils.Check(this, cbEndereco)
            };

            if (check.All(c => c))
            {
                try
                {
                    if (_update)
                    {
                        var enderecoDAO = new EnderecoDAO();

                        var fornecedor = new Fornecedor
                        {
                            Id = _id,
                            RazaoSocial = edRazaoSocial.Text,
                            NomeFantasia = edNomeFantasia.Text,
                            CNPJ = edCnpj.Text,
                            Contato = edContato.Text,
                            Email = edEmail.Text,
                            Endereco = enderecoDAO.GetById(cbEndereco.SelectedIndex + 1)
                        };

                        var fornecedorDAO = new FornecedorDAO();

                        fornecedorDAO.Update(fornecedor);
                        MessageBox.Show("Fornecedor atualizado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                        _update = false;
                    }
                    else
                    {
                        var fornecedor = new Fornecedor
                        {
                            RazaoSocial = edRazaoSocial.Text,
                            NomeFantasia = edNomeFantasia.Text,
                            CNPJ = edCnpj.Text,
                            Contato = edContato.Text,
                            Email = edEmail.Text,
                            Endereco = (Endereco)cbEndereco.SelectedItem
                        };

                        var fornecedorDAO = new FornecedorDAO();
                        var resultado = fornecedorDAO.Insert(fornecedor);
                        MessageBox.Show(resultado, "Resultado", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao inserir o fornecedor: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                Utils.ResetControls(this);
                this.Close();
            }
            else
            {
                check.Clear();
            }
        }

        // Limpa os campos quando o botão "Limpar" é clicado

    }
}
