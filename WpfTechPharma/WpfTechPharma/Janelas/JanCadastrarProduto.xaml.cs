using MySqlX.XDevAPI;
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
    public partial class JanCadastrarProduto : Window
    {
        private int _id = 0;
        private Produto _produto = new Produto();
        private bool _update = false;

        public JanCadastrarProduto()
        {
            InitializeComponent();
            InicializarManipuladoresEventos();
            LoadData();
        }

        public JanCadastrarProduto(int id)
        {
            _id = id;

            InitializeComponent();
            InicializarManipuladoresEventos();
            LoadData();
            FillForm();
        }

        private void InicializarManipuladoresEventos()
        {
            edNome.TextChanged += TextBox_TextChanged;
            edMarca.TextChanged += TextBox_TextChanged;
            edEstoque.TextChanged += TextBox_TextChanged;
            edCodigoBarras.TextChanged += TextBox_TextChanged;
            edTipo.SelectionChanged += ComboBox_SelectionChanged;
            edValorVenda.TextChanged += TextBox_TextChanged;
            edValorCompra.TextChanged += TextBox_TextChanged;
            edFornecedor.SelectionChanged += ComboBox_SelectionChanged;
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

        private void FillForm()
        {
            var produtoDAO = new ProdutoDAO();
            _produto = produtoDAO.GetById(_id);

            var fornecedorDAO = new FornecedorDAO();
            var fornecedor = fornecedorDAO.GetById(_produto.Fornecedor.Id);

            edNome.Text = _produto.Nome;
            edMarca.Text = _produto.Marca;
            edValorCompra.Text = _produto.ValorCompra.ToString();
            edValorVenda.Text = _produto.ValorVenda.ToString();
            edTipo.Text = _produto.Tipo;
            edEstoque.Text = _produto.Quantidade.ToString();
            edCodigoBarras.Text = _produto.CodigoBarra;
            edFornecedor.SelectedIndex = (fornecedor.Id - 1);

            _update = true;
        }

        private void LoadData()
        {
            try
            {
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
                Ultis.Check(this, edTipo),
                Ultis.Check(this, edCodigoBarras),
                Ultis.Check(this, edMarca),
                Ultis.Check(this, edFornecedor),
                Ultis.Check(this, edEstoque),
                Ultis.Check(this, edValorCompra),
                Ultis.Check(this, edValorVenda)
            };

            if (check.All(c => c))
            {
                try
                {
                    if (_update)
                    {
                        var fornecedorDAO = new FornecedorDAO();

                        var Produto = new Produto
                        {
                            Id = _id,
                            Nome = edNome.Text,
                            Marca = edMarca.Text,
                            ValorCompra = float.Parse(edValorCompra.Text),
                            ValorVenda = float.Parse(edValorVenda.Text),
                            Tipo = edTipo.Text,
                            Quantidade = int.Parse(edEstoque.Text),
                            CodigoBarra = edCodigoBarras.Text,
                            Fornecedor = fornecedorDAO.GetById(edFornecedor.SelectedIndex + 1)
                        };

                        var ProdutoDAO = new ProdutoDAO();

                        ProdutoDAO.Update(Produto);
                        MessageBox.Show("Produto atualizado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                        _update = false;
                    }
                    else
                    {
                        var Produto = new Produto
                        {
                            Nome = edNome.Text,
                            Marca = edMarca.Text,
                            ValorCompra = float.Parse(edValorCompra.Text),
                            ValorVenda = float.Parse(edValorVenda.Text),
                            Tipo = edTipo.Text,
                            Quantidade = int.Parse(edEstoque.Text),
                            CodigoBarra = edCodigoBarras.Text,
                            Fornecedor = (Fornecedor)edFornecedor.SelectedItem
                        };

                        var ProdutoDAO = new ProdutoDAO();
                        var resultado = ProdutoDAO.Insert(Produto);
                        MessageBox.Show(resultado, "Resultado", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao inserir o Produto: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
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
