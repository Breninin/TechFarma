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
    /// Lógica interna para JanCadastrarInsumo.xaml
    /// </summary>
    public partial class JanCadastrarInsumo : Window
    {
        private int _id = 0;
        private Insumo _insumo = new Insumo();
        private bool _update = false;

        public JanCadastrarInsumo()
        {
            InitializeComponent();
            InicializarManipuladoresEventos();
            LoadData();
        }

        public JanCadastrarInsumo(int id)
        {
            _id = id;

            InitializeComponent();
            InicializarManipuladoresEventos();
            LoadData();
            FillForm();
        }

        // Inicializa os manipuladores de eventos para os controles de entrada de texto e combobox
        private void InicializarManipuladoresEventos()
        {
            edValorCompra.TextChanged += TextBox_TextChanged;
            edEstoque.TextChanged += TextBox_TextChanged;
            edCodigoBarras.TextChanged += TextBox_TextChanged;
            edMarca.TextChanged += TextBox_TextChanged;
            edNome.TextChanged += TextBox_TextChanged;
            edFornecedor.SelectionChanged += ComboBox_SelectionChanged;
            
        }

        // Manipulador de evento para alterações em caixas de texto
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            Ultis.Check(this, textBox);
        }

        // Manipulador de evento para alterações em combobox
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            Ultis.Check(this, comboBox);
        }

        private void FillForm()
        {
            var insumoDAO = new InsumoDAO();
            _insumo = insumoDAO.GetById(_id);

            var fornecedorDAO = new FornecedorDAO();
            var fornecedor = fornecedorDAO.GetById(_insumo.Fornecedor.Id);

            edValorCompra.Text = _insumo.ValorCompra.ToString();
            edEstoque.Text = _insumo.Quantidade.ToString();
            edCodigoBarras.Text = _insumo.CodigoBarra;
            edMarca.Text = _insumo.Marca;
            edNome.Text = _insumo.Nome;
            edFornecedor.SelectedIndex = (fornecedor.Id - 1);

            _update = true;
        }

        // Manipulador de evento para o botão "Limpar"
        private void btLimpar_Click(object sender, RoutedEventArgs e)
        {
            // Exibe uma caixa de diálogo de confirmação antes de limpar os controles
            if (MessageBox.Show("Deseja realmente cancelar?", "Aviso", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Ultis.ResetControls(this);
                _update = false;
            }
        }

        // Manipulador de evento para o botão "Salvar"
        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            // Lista de verificação para verificar se todos os campos foram preenchidos corretamente
            List<bool> check = new List<bool>
            {
                Ultis.Check(this, edValorCompra),
                Ultis.Check(this, edEstoque),
                Ultis.Check(this, edCodigoBarras),
                Ultis.Check(this, edMarca),
                Ultis.Check(this, edNome),
                Ultis.Check(this, edFornecedor)
            };

            // Verifica se todos os campos foram preenchidos corretamente
            if (check.All(c => c))
            {
                try
                {
                    if (_update)
                    {
                        var fornecedorDAO = new FornecedorDAO();

                        // Cria um objeto Insumo com base nos valores inseridos nos campos
                        var insumo = new Insumo
                        {
                            Id = _id,
                            ValorCompra = float.Parse(edValorCompra.Text),
                            Quantidade = Convert.ToInt32(edEstoque.Text),
                            CodigoBarra = edCodigoBarras.Text,
                            Marca = edMarca.Text,
                            Nome = edNome.Text,
                            Fornecedor = fornecedorDAO.GetById(edFornecedor.SelectedIndex + 1)
                        };

                        // Cria uma instância do InsumoDAO e insere o insumo no banco de dados
                        var insumoDAO = new InsumoDAO();

                        insumoDAO.Update(insumo);
                        MessageBox.Show("Insumo atualizado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                        _update = false;
                    }
                    else
                    {
                        // Cria um objeto Insumo com base nos valores inseridos nos campos
                        var insumo = new Insumo
                        {
                            ValorCompra = float.Parse(edValorCompra.Text),
                            Quantidade = Convert.ToInt32(edEstoque.Text),
                            CodigoBarra = edCodigoBarras.Text,
                            Marca = edMarca.Text,
                            Nome = edNome.Text,
                            Fornecedor = (Fornecedor)edFornecedor.SelectedItem
                        };

                        // Cria uma instância do InsumoDAO e insere o insumo no banco de dados
                        var insumoDAO = new InsumoDAO();

                        insumoDAO.Insert(insumo);
                        MessageBox.Show("Insumo inserido com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao inserir o insumo: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                // Após a inserção do insumo, redefine os controles para o estado inicial
                Ultis.ResetControls(this);
                this.Close();
            }
            else
            {
                check.Clear();
            }
        }

        // Carrega os dados dos fornecedores para o combobox
        private void LoadData()
        {
            try
            {
                // Limpa e preenche o combobox edFornecedor com os fornecedores existentes no banco de dados
                edFornecedor.ItemsSource = null;
                edFornecedor.Items.Clear();
                edFornecedor.ItemsSource = new FornecedorDAO().List();
                edFornecedor.DisplayMemberPath = "RazaoSocial";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Não Executado", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
