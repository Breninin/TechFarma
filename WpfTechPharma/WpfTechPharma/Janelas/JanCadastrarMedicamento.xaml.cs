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
    public partial class JanCadastrarMedicamento : Window
    {
        public JanCadastrarMedicamento()
        {
            InitializeComponent();
            InicializarManipuladoresEventos();
            LoadData();
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
            cbTarja.SelectionChanged += ComboBox_SelectionChanged;

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
                Ultis.ResetControls(this);
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
                Ultis.Check(this, cbTarja),
                Ultis.Check(this, edValorCompra),
                Ultis.Check(this, edValorVenda)
            };

            if (check.All(c => c))
            {
                try
                {
                    var Medicamento = new Medicamento
                    {
                        Nome = edNome.Text,
                        Marca = edMarca.Text,
                        ValorCompra = float.Parse(edValorCompra.Text),
                        ValorVenda = float.Parse(edValorVenda.Text),
                        Quantidade = int.Parse(edEstoque.Text),
                        Tarja = cbTarja.Text,
                        CodigoBarra = edCodigoBarras.Text,
                        Fornecedor = (Fornecedor)edFornecedor.SelectedItem
                    };

                    var MedicamentoDAO = new MedicamentoDAO();
                    MedicamentoDAO.Insert(Medicamento);
                    MessageBox.Show("Medicamento inserido com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao inserir o Medicamento: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
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
