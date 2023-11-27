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

        private List<CarrinhoItem> carrinho = new List<CarrinhoItem>();
        private double ValorTotal = 0;
        public JanRealizarCompra()
        {
            InitializeComponent();
            InitializeEventHandlers();
            LoadData();
        }

        private void InitializeEventHandlers()
        {
            edParcelas.TextChanged += edParcelas_TextChanged;
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

            edValorTotal.Content = "VALOR TOTAL: 0 R$";

            Utils.AddNumericMask(edParcelas);
            Utils.AddNumericMask(edQuant);
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

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DatePicker datePicker = (DatePicker)sender;
            Utils.Check(this, datePicker);
        }

        private void edParcelas_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            Utils.Check(this, textBox);
            if (int.TryParse(textBox.Text, out int numeroParcelas) && numeroParcelas > 0)
            {
                double valorParcela = ValorTotal / numeroParcelas;
                edValorParcelas.Text = valorParcela.ToString();
            }
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
            List<bool> check = new List<bool> {
        Utils.Check(this, cbProduto),
        Utils.Check(this, edQuant),
    };

            if (check.All(c => c))
            {
                try
                {
                    TipoObjeto objetoSelecionado = cbProduto.SelectedItem as TipoObjeto;

                    if (int.TryParse(edQuant.Text, out int quantidade) && quantidade > 0)
                    {
                        CarrinhoItem itemExistente = carrinho.FirstOrDefault(item => item.TipoObjeto == objetoSelecionado);

                        if (itemExistente != null)
                        {
                            itemExistente.Quantidade += quantidade;
                        }
                        else
                        {
                            CarrinhoItem novoItem = new CarrinhoItem
                            {
                                TipoObjeto = objetoSelecionado,
                                Quantidade = quantidade,
                            };

                            carrinho.Add(novoItem);
                        }

                        dgvProdutos.ItemsSource = null;
                        dgvProdutos.ItemsSource = carrinho;
                        AtualizarValorTotalCarrinho();
                    }
                    else
                    {
                        MessageBox.Show("A quantidade deve ser um número válido maior que zero.", "Erro de Entrada", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    Utils.ResetControls(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao adicionar item ao carrinho: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    Console.WriteLine(ex.ToString());
                }
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
                    edValorUnitario.Text = objeto.ValorCompra.ToString("0.00");
                }
            }
            else
            {
                edValorUnitario.Text = string.Empty;
            }
        }

        private void cbFormaPag_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            Utils.Check(this, comboBox);

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
                edParcelas.Clear();
                edValorParcelas.Clear();
            }

            edParcelas.Visibility = isCartaoCreditoSelected ? Visibility.Visible : Visibility.Collapsed;
            edValorParcelas.Visibility = isCartaoCreditoSelected ? Visibility.Visible : Visibility.Collapsed;
            iconParcela.Visibility = isCartaoCreditoSelected ? Visibility.Visible : Visibility.Collapsed;
            iconValorParcela.Visibility = isCartaoCreditoSelected ? Visibility.Visible : Visibility.Collapsed;
        }
        public void AtualizarValorTotalCarrinho()
        {
            double total = 0;
            foreach (var item in carrinho)
            {
                dynamic objeto = item.TipoObjeto.Objeto;
                total += objeto.ValorCompra * item.Quantidade;
            }
            edValorTotal.Content = "VALOR TOTAL: " + total + " R$";
            ValorTotal = total;
        }

        private void btExcluir_Click(object sender, RoutedEventArgs e)
        {
            List<CarrinhoItem> itensSelecionados = carrinho.Where(item => item.IsSelected).ToList();

            if (itensSelecionados.Count > 0)
            {
                MessageBoxResult result = MessageBox.Show("Tem certeza que deseja excluir os itens selecionados?", "Confirmação de Exclusão", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    foreach (CarrinhoItem item in itensSelecionados)
                    {
                        carrinho.Remove(item);
                    }

                    dgvProdutos.ItemsSource = null;
                    dgvProdutos.ItemsSource = carrinho;
                    AtualizarValorTotalCarrinho();
                }
            }
            else
            {
                MessageBox.Show("Nenhum item selecionado para exclusão.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btFinalizar_Click(object sender, RoutedEventArgs e)
        {
            if (carrinho.Count != 0)
            {
                int parcelas;
                bool isCartaoCreditoSelected = cbFormaPag.SelectedItem != null && ((ComboBoxItem)cbFormaPag.SelectedItem).Content.ToString() == "Cartão de Crédito";
                if (!isCartaoCreditoSelected) parcelas = 1;
                else parcelas = Convert.ToInt32(edParcelas.Text);

                var despesa = new Despesa
                {
                    Data = (DateTime)dpCompra.SelectedDate,
                    Valor = (float)ValorTotal,
                    Descricao = "reabastecimento do estoque atual",
                    Tipo = "Compra Estoque",
                    QuantidadeParcelas = parcelas
                };

                var despesaDAO = new DespesaDAO();
                despesaDAO.Insert(despesa);

                if (parcelas == 1)
                {
                    DateTime dataParcela = (DateTime)dpCompra.SelectedDate;
                    dataParcela = dataParcela.AddDays(30);
                    int lastIdDesp = new DespesaDAO().GetLastInsertID();

                    var pagamento = new Pagamento
                    {
                        Data = (DateTime)dpCompra.SelectedDate,
                        Valor = (float) ValorTotal,
                        FormaPagamento = cbFormaPag.Text,
                        Status = "Em Andamento",
                        NumeroParcela = 1,
                        Vencimento = dataParcela,
                        Despesa = new DespesaDAO().GetById(lastIdDesp),
                        Caixa = new CaixaDAO().GetById(1)
                    };

                    var pagamentoDAO = new PagamentoDAO();
                    pagamentoDAO.Insert(pagamento);
                }
                else if (parcelas > 1)
                {
                    DateTime dataParcela = (DateTime)dpCompra.SelectedDate;
                    int lastIdDesp = new DespesaDAO().GetLastInsertID();

                    for (int i = 1; i <= parcelas; i++)
                    {
                        var pagamento = new Pagamento
                        {
                            Data = (DateTime)dpCompra.SelectedDate,
                            Valor = float.Parse(edValorParcelas.Text),
                            FormaPagamento = cbFormaPag.Text,
                            Status = "Em Andamento",
                            NumeroParcela = i,
                            Vencimento = dataParcela.AddDays(30 * i),
                           Despesa = new DespesaDAO().GetById(lastIdDesp),
                            Caixa = new CaixaDAO().GetById(1)
                        };

                        var pagamentoDAO = new PagamentoDAO();
                        pagamentoDAO.Insert(pagamento);
                    }
                }
            }
            else
            {
                MessageBox.Show("O carrinho esta vazio.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
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

public class CarrinhoItem
{
    public TipoObjeto TipoObjeto { get; set; }
    public int Quantidade { get; set; }
    public bool IsSelected { get; set; }
    public CarrinhoItem()
    {
        IsSelected = false;
    }
}