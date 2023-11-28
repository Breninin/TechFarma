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
    public partial class JanCadastrarMedicamento : Window
    {
        private int _id = 0;
        private Medicamento _medicamento = new Medicamento();
        private bool _update = false;

        public JanCadastrarMedicamento()
        {
            InitializeComponent();
            InicializarManipuladoresEventos();
            LoadData();
        }

        public JanCadastrarMedicamento(int id)
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
            edValorVenda.TextChanged += TextBox_TextChanged;
            edValorCompra.TextChanged += TextBox_TextChanged;
            edFornecedor.SelectionChanged += ComboBox_SelectionChanged;
            cbTarja.SelectionChanged += ComboBox_SelectionChanged;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            Utils.Check(this, textBox);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            Utils.Check(this, comboBox);
        }

        private void FillForm()
        {
            var medicamentoDAO = new MedicamentoDAO();
            _medicamento = medicamentoDAO.GetById(_id);

            var fornecedorDAO = new FornecedorDAO();
            var fornecedor = fornecedorDAO.GetById(_medicamento.Fornecedor.Id);

            edNome.Text = _medicamento.Nome;
            edMarca.Text = _medicamento.Marca;
            edValorCompra.Text = _medicamento.ValorCompra.ToString();
            edValorVenda.Text = _medicamento.ValorVenda.ToString();
            edEstoque.Text = _medicamento.Quantidade.ToString();
            cbTarja.Text = _medicamento.Tarja;
            edCodigoBarras.Text = _medicamento.CodigoBarra;
            edFornecedor.Text = fornecedor.RazaoSocial;
                
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
                Utils.ResetControls(this);
                _update = false;
            }
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {

            List<bool> check = new List<bool>
            {
                Utils.Check(this, edNome),
                Utils.Check(this, edCodigoBarras),
                Utils.Check(this, edMarca),
                Utils.Check(this, edFornecedor),
                Utils.Check(this, edEstoque),
                Utils.Check(this, cbTarja),
                Utils.Check(this, edValorCompra),
                Utils.Check(this, edValorVenda)
            };

            if (check.All(c => c))
            {
                try
                {
                    if (_update)
                    {
                        var fornecedorDAO = new FornecedorDAO();

                        var medicamento = new Medicamento
                        {
                            Id = _id,
                            Nome = edNome.Text,
                            Marca = edMarca.Text,
                            ValorCompra = float.Parse(edValorCompra.Text),
                            ValorVenda = float.Parse(edValorVenda.Text),
                            Quantidade = int.Parse(edEstoque.Text),
                            Tarja = cbTarja.Text,
                            CodigoBarra = edCodigoBarras.Text,
                            Fornecedor = fornecedorDAO.GetById(edFornecedor.SelectedIndex + 1)
                        };

                        var MedicamentoDAO = new MedicamentoDAO();

                        MedicamentoDAO.Update(medicamento);
                        MessageBox.Show("Medicamento atualizado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                        _update = false;
                    }
                    else
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
                        var resultado = MedicamentoDAO.Insert(Medicamento);
                        MessageBox.Show(resultado, "Resultado", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao inserir o Medicamento: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                Utils.ResetControls(this);
                this.Close();
            }
            else
            {
                check.Clear();
            }
        }
    }
}
