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
    public partial class JanCadastrarInsumo : Window
    {
        public JanCadastrarInsumo()
        {
            InitializeComponent();
            InicializarManipuladoresEventos();
            LoadData();
        }

        private void InicializarManipuladoresEventos()
        {
            edValorCompra.TextChanged += TextBox_TextChanged;
            edEstoque.TextChanged += TextBox_TextChanged;
            edCodigoBarras.TextChanged += TextBox_TextChanged;
            edMarca.TextChanged += TextBox_TextChanged;
            edNome.TextChanged += TextBox_TextChanged;
            edFornecedor.SelectionChanged += ComboBox_SelectionChanged;
            edTipo.SelectionChanged += ComboBox_SelectionChanged;
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
                Ultis.Check(this, edValorCompra),
                Ultis.Check(this, edEstoque),
                Ultis.Check(this, edCodigoBarras),
                Ultis.Check(this, edMarca),
                Ultis.Check(this, edNome),
                Ultis.Check(this, edFornecedor),
                Ultis.Check(this, edTipo)
            };

            if (check.All(c => c))
            {
                try
                {
                    var insumo = new Insumo
                    {
                        ValorCompra = float.Parse(edValorCompra.Text),
                        Quantidade = Convert.ToInt32(edEstoque.Text),
                        CodigoBarra = edCodigoBarras.Text,
                        Marca = edMarca.Text,
                        Nome = edNome.Text,
                        Fornecedor = (Fornecedor) edFornecedor.SelectedItem,
                        Tipo = edTipo.SelectedItem.ToString()
                    };

                    var insumoDAO = new InsumoDAO();
                    insumoDAO.Insert(insumo);
                    MessageBox.Show("Insumo inserido com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao inserir o insumo: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                Ultis.ResetControls(this);
            }
            else
            {
                check.Clear();
            }
        }
        private void LoadData()
        {
            try
            {
                edFornecedor.ItemsSource = null;
                edFornecedor.Items.Clear();
                edFornecedor.ItemsSource = new FornecedorDAO().List();
                edFornecedor.DisplayMemberPath = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Não Executado", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
