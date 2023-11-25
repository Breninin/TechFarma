using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfTechPharma.Auxiliares;
using WpfTechPharma.Modelos;

namespace WpfTechPharma.Janelas
{
    /// <summary>
    /// Lógica interna para JanRealizarCompra.xaml
    /// </summary>
    public partial class JanRealizarCompra : Window
    {
        public JanRealizarCompra()
        {
            InitializeComponent();
            InitializeEventHandlers();
            LoadData();
        }

        private void InitializeEventHandlers()
        {
            edHorarioCompra.TextChanged += TextBox_TextChanged;
            edFormaPag.TextChanged += TextBox_TextChanged;
            edParcelas.TextChanged += TextBox_TextChanged;
            edValorParcelas.TextChanged += TextBox_TextChanged;
            edParcelas.TextChanged += TextBox_TextChanged;
            edQuant.TextChanged += TextBox_TextChanged;
            edValorUnitario.TextChanged += TextBox_TextChanged;
            cbFuncionaio.SelectionChanged += ComboBox_SelectionChanged;
            cbFornecedor.SelectionChanged += ComboBox_SelectionChanged;
            cbProduto.SelectionChanged += ComboBox_SelectionChanged;
            dpCompra.SelectedDateChanged += DatePicker_SelectedDateChanged;

            Ultis.AddNumericMask(edParcelas);
            Ultis.AddNumericMask(edValorParcelas);
            Ultis.AddNumericMask(edQuant);
            Ultis.AddNumericMask(edValorUnitario);
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

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DatePicker datePicker = (DatePicker)sender;
            Ultis.Check(this, datePicker);
        }

        private void LoadData()
        {
            try
            {
                cbFornecedor.ItemsSource = null;
                cbFornecedor.Items.Clear();
                cbFornecedor.ItemsSource = new FornecedorDAO().List();
                cbFornecedor.DisplayMemberPath = "NomeFantasia";

                cbFuncionaio.ItemsSource = null;
                cbFuncionaio.Items.Clear();
                cbFuncionaio.ItemsSource = new FuncionarioDAO().List();
                cbFuncionaio.DisplayMemberPath = "Nome";

                cbProduto.ItemsSource = null;
                cbProduto.Items.Clear();
                cbProduto.ItemsSource = new ProdutoDAO().List();
                cbProduto.DisplayMemberPath = "Nome";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Não Executado", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            List<bool> check = new List<bool>
            {
                Ultis.Check(this, dpCompra),
                Ultis.Check(this, cbFornecedor),
                Ultis.Check(this, cbFuncionaio),
                Ultis.Check(this, cbProduto),
                Ultis.Check(this, edHorarioCompra),
                Ultis.Check(this, edFormaPag),
                Ultis.Check(this, edParcelas),
                Ultis.Check(this, edValorParcelas),
                Ultis.Check(this, edQuant),
                Ultis.Check(this, edValorUnitario)
            };

            if (check.All(c => c))
            {
                try
                {

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
