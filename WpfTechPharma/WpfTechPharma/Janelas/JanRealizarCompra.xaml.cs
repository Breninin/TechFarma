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
            edParcelas.TextChanged += TextBox_TextChanged;
            edParcelas.TextChanged += TextBox_TextChanged;
            edQuant.TextChanged += TextBox_TextChanged;
            cbProduto.SelectionChanged += ComboBox_SelectionChanged;
            cbFormaPag.SelectionChanged += cbFormaPag_SelectionChanged;
            dpCompra.SelectedDateChanged += DatePicker_SelectedDateChanged;

            edValorParcelas.IsEnabled = false;
            edValorUnitario.IsEnabled = false;

            edParcelas.Visibility = Visibility.Collapsed;
            edValorParcelas.Visibility = Visibility.Collapsed;
            iconValorParcela.Visibility = Visibility.Collapsed;
            iconParcela.Visibility = Visibility.Collapsed;

            cbFormaPag.HorizontalAlignment = HorizontalAlignment.Center;
            cbFormaPag.Width = 400;

            Ultis.AddNumericMask(edParcelas);
            Ultis.AddNumericMask(edQuant);
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
                cbProduto.ItemsSource = null;
                cbProduto.Items.Clear();

                List<TipoObjeto> produtos = new List<TipoObjeto>();

                produtos.AddRange(new ProdutoDAO().List().Select(p => new TipoObjeto { Objeto = p }));
                produtos.AddRange(new MedicamentoDAO().List().Select(m => new TipoObjeto { Objeto = m }));
                produtos.AddRange(new InsumoDAO().List().Select(i => new TipoObjeto { Objeto = i }));

                cbProduto.ItemsSource = produtos;
                cbProduto.DisplayMemberPath = "Objeto.Nome";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Não Executado", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void InserirProduto()
        {
            TipoObjeto itemSelecionado = cbProduto.SelectedItem as TipoObjeto;

            if (itemSelecionado != null)
            {
                string tipo = itemSelecionado.ObterTipo();

                switch (tipo)
                {
                    case nameof(Produto):
                        new ProdutoDAO().Insert(itemSelecionado.Objeto as Produto);
                        break;

                    case nameof(Medicamento):
                        new MedicamentoDAO().Insert(itemSelecionado.Objeto as Medicamento);
                        break;

                    case nameof(Insumo):
                        new InsumoDAO().Insert(itemSelecionado.Objeto as Insumo);
                        break;

                    default:
                        break;
                }
            }
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            List<bool> check = new List<bool>
            {
                Ultis.Check(this, dpCompra),
                Ultis.Check(this, cbProduto),
                Ultis.Check(this, cbFormaPag),
                Ultis.Check(this, edParcelas),
                Ultis.Check(this, edQuant),
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

        private void cbProduto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TipoObjeto itemSelecionado = cbProduto.SelectedItem as TipoObjeto;
            if (itemSelecionado != null)
            {
                dynamic objeto = itemSelecionado.Objeto;

                if (objeto != null && objeto.ValorCompra != null)
                {
                    edValorUnitario.Text = (objeto.ValorCompra).ToString("0.00");
                }
            }
            else
            {
                edValorUnitario.Text = string.Empty;
            }
        }

        private void edParcelas_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void cbFormaPag_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            Ultis.Check(this, comboBox);

            bool isCartaoCreditoSelected = cbFormaPag.SelectedItem != null && ((ComboBoxItem)cbFormaPag.SelectedItem).Content.ToString() == "Cartão de Crédito";
            if (isCartaoCreditoSelected)
            {
                cbFormaPag.HorizontalAlignment = HorizontalAlignment.Left;
                cbFormaPag.Width = 250;
            }
            else
            {
                cbFormaPag.HorizontalAlignment = HorizontalAlignment.Center;
                cbFormaPag.Width = 400;   
            }

            edParcelas.Visibility = isCartaoCreditoSelected ? Visibility.Visible : Visibility.Collapsed;
            edValorParcelas.Visibility = isCartaoCreditoSelected ? Visibility.Visible : Visibility.Collapsed;
            iconParcela.Visibility = isCartaoCreditoSelected ? Visibility.Visible : Visibility.Collapsed;
            iconValorParcela.Visibility = isCartaoCreditoSelected ? Visibility.Visible : Visibility.Collapsed;
        }

    }
}

public class TipoObjeto
{
    public object Objeto { get; set; }

    public string ObterTipo()
    {
        return Objeto?.GetType().Name;
    }
}
