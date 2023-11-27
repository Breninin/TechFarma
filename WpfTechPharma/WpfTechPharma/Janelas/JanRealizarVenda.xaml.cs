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
    /// Lógica interna para JanRealizarVenda.xaml
    /// </summary>
    public partial class JanRealizarVenda : Window
    {
        private List<CarrinhoItem> carrinho = new List<CarrinhoItem>();
        private double ValorTotal = 0;
        public JanRealizarVenda()
        {
            InitializeComponent();
            InitializeEventHandlers();
            LoadData();
        }

        private void InitializeEventHandlers()
        {
            edParcelas.TextChanged += edParcelas_TextChanged;
            cbFormaPag.SelectionChanged += cbFormaPag_SelectionChanged;
            dpDataVenda.SelectedDateChanged += DatePicker_SelectedDateChanged;
            edDesconto.TextChanged += edDesconto_TextChanged;
            edDesconto.LostFocus += edDesconto_LostFocus;

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
            Utils.AddNumericMask(edDesconto);

            edDesconto.Text = "0";
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
                if (double.TryParse(edDesconto.Text, out double descontoPercentual))
                {
                    descontoPercentual = Math.Max(0, Math.Min(descontoPercentual, 100));
                    descontoPercentual /= 100.0;
                    double valorComDesconto = ValorTotal * (1 - descontoPercentual);
                    double valorParcela = valorComDesconto / numeroParcelas;

                    edValorParcelas.Text = valorParcela.ToString();
                }
            }
        }
        private void edDesconto_LostFocus(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrEmpty(textBox.Text))
            {
                edDesconto.Text = "0";
            }
            AtualizarValorTotalCarrinho();
        }

        private void edDesconto_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (int.TryParse(edParcelas.Text, out int numeroParcelas) && numeroParcelas > 0)
            {
                if (double.TryParse(textBox.Text, out double descontoPercentual))
                {
                    descontoPercentual = Math.Max(0, Math.Min(descontoPercentual, 100));
                    descontoPercentual /= 100.0;

                    double valorComDesconto = ValorTotal * (1 - descontoPercentual);

                    double valorParcela = (descontoPercentual > 0) ? valorComDesconto / numeroParcelas : ValorTotal / numeroParcelas;

                    edValorParcelas.Text = valorParcela.ToString();
                }
            }
            AtualizarValorTotalCarrinho();
        }

        private void LoadData()
        {
            try
            {
                cbProduto.ItemsSource = null;
                cbProduto.Items.Clear();

                cbFuncionaio.ItemsSource = null;
                cbFuncionaio.Items.Clear();

                cbCliente.ItemsSource = null;
                cbCliente.Items.Clear();

                List<TipoObjeto> produtosMedicamentos = new List<TipoObjeto>();

                var produtosComQuantidade = new ProdutoDAO().List().Where(p => p.Quantidade > 0);
                foreach (var produto in produtosComQuantidade)
                {
                    CarrinhoItem itemExistente = carrinho.FirstOrDefault(item => ((dynamic)item).TipoObjeto.Objeto.Id == produto.Id);

                    if (itemExistente != null)
                    {
                        TipoObjeto objeto = itemExistente.TipoObjeto;
                        dynamic produtoObjeto = objeto.Objeto;
                        int quantidadeDisponivel = produto.Quantidade - produtoObjeto.Quantidade;
                        if (quantidadeDisponivel != 0)
                        {
                            produtosMedicamentos.Add(new TipoObjeto { Objeto = produto });
                        }
                    }
                    else
                    {
                        produtosMedicamentos.Add(new TipoObjeto { Objeto = produto });
                    }
                }

                var medicamentosComQuantidade = new MedicamentoDAO().List().Where(m => m.Quantidade > 0);
                foreach (var medicamento in medicamentosComQuantidade)
                {
                    CarrinhoItem itemExistente = carrinho.FirstOrDefault(item => ((dynamic)item).TipoObjeto.Objeto.Id == medicamento.Id);
                    if (itemExistente != null)
                    {
                        TipoObjeto objeto = itemExistente.TipoObjeto;
                        dynamic medicamentoObjeto = objeto.Objeto;
                        int quantidadeDisponivel = medicamento.Quantidade - medicamentoObjeto.Quantidade;
                        if (quantidadeDisponivel != 0)
                        {
                            produtosMedicamentos.Add(new TipoObjeto { Objeto = medicamento });
                        }
                    }
                    else
                    {
                        produtosMedicamentos.Add(new TipoObjeto { Objeto = medicamento });
                    }
                }

                cbProduto.ItemsSource = produtosMedicamentos;
                cbProduto.DisplayMemberPath = "Objeto.Nome";

                ClienteDAO clienteDAO = new ClienteDAO();
                List<Cliente> clientes = clienteDAO.List();

                cbCliente.ItemsSource = clientes;
                cbCliente.DisplayMemberPath = "Nome";

                FuncionarioDAO funcionarioDAO = new FuncionarioDAO();
                List<Funcionario> funcionarios = funcionarioDAO.List();

                cbFuncionaio.ItemsSource = funcionarios;
                cbFuncionaio.DisplayMemberPath = "Nome";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Não Executado", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void btFinalizar_Click(object sender, RoutedEventArgs e)
        {
            if (carrinho.Count != 0)
            {

                bool isCartaoCreditoSelected = cbFormaPag.SelectedItem != null && ((ComboBoxItem)cbFormaPag.SelectedItem).Content.ToString() == "Cartão de Crédito";

                List<bool> check = new List<bool> {
                 Utils.Check(this, dpDataVenda),
                 Utils.Check(this, cbFormaPag),
                 Utils.Check(this, cbCliente),
                 Utils.Check(this, cbFuncionaio ),
                };

                List<bool> checkParcel = new List<bool> {
                 Utils.Check(this, edParcelas),
                };

                bool isCheck = false;

                bool isParcelasValidas = int.TryParse(edParcelas.Text, out int parcelass) && parcelass > 0;

                if (check.All(c => c))
                {
                    isCheck = true;

                    if ((isCartaoCreditoSelected && checkParcel.All(c => c) && !isParcelasValidas) || string.IsNullOrEmpty(edParcelas.Text))
                    {
                        isCheck = false;
                    }
                }

                if (isCheck)
                {

                    int parcelas;
                    if (!isCartaoCreditoSelected) parcelas = 1;
                    else parcelas = Convert.ToInt32(edParcelas.Text);

                    var venda = new Venda
                    {
                        Data = (DateTime)dpDataVenda.SelectedDate,
                        Valor = (float)ValorTotal,
                        Desconto = float.Parse(edDesconto.Text),
                        QuantidadeParcelas = parcelas,
                        Cliente = (Cliente)cbCliente.SelectedItem,
                        Funcionario = (Funcionario)cbFuncionaio.SelectedItem
                    };

                    var vendaDAO = new VendaDAO();
                    vendaDAO.Insert(venda);

                    int lastIdVend = new VendaDAO().GetLastInsertID();
                    float desconto = float.Parse(edDesconto.Text);
                    float fatorDesconto = 1 - (desconto / 100);

                    if (parcelas == 1)
                    {
                        DateTime dataParcela = (DateTime)dpDataVenda.SelectedDate;
                        dataParcela = dataParcela.AddDays(30);

                        var recebimento = new Recebimento
                        {
                            Data = (DateTime)dpDataVenda.SelectedDate,
                            Valor = (float)ValorTotal * fatorDesconto,
                            FormaRecebimento = cbFormaPag.Text,
                            Status = "Em Andamento",
                            NumeroParcela = 1,
                            Vencimento = dataParcela,
                            Venda = new VendaDAO().GetById(lastIdVend),
                            Caixa = new CaixaDAO().GetById(1)
                        };

                        var recebimentoDAO = new RecebimentoDAO();
                        recebimentoDAO.Insert(recebimento);
                    }
                    else if (parcelas > 1)
                    {
                        DateTime dataParcela = (DateTime)dpDataVenda.SelectedDate;

                        for (int i = 1; i <= parcelas; i++)
                        {
                            var recebimento = new Recebimento
                            {
                                Data = (DateTime)dpDataVenda.SelectedDate,
                                Valor = (float)ValorTotal * fatorDesconto,
                                FormaRecebimento = cbFormaPag.Text,
                                Status = "Em Andamento",
                                NumeroParcela = i,
                                Vencimento = dataParcela.AddDays(30 * i),
                                Venda = new VendaDAO().GetById(lastIdVend),
                                Caixa = new CaixaDAO().GetById(1)
                            };

                            var recebimentoDAO = new RecebimentoDAO();
                            recebimentoDAO.Insert(recebimento);
                        }
                    }

                    foreach (CarrinhoItem item in carrinho)
                    {
                        TipoObjeto itemSelecionado = item.TipoObjeto;
                        string tipo = itemSelecionado.ObterTipo();
                        dynamic objetoItem = itemSelecionado.Objeto;

                        switch (tipo)
                        {
                            case nameof(Produto):

                                Produto objetoProduto = new ProdutoDAO().GetById(objetoItem.Id);

                                var vendaProduto = new VendaProduto
                                {
                                    QuantidadeItem = item.Quantidade,
                                    ValorItem = item.Quantidade * objetoItem.ValorCompra,
                                    Venda = new VendaDAO().GetById(lastIdVend),
                                    Produto = objetoProduto
                                };

                                objetoProduto.Quantidade -= item.Quantidade;

                                var vendaProdutoDAO = new VendaProdutoDAO();
                                vendaProdutoDAO.Insert(vendaProduto);

                                var produtoDAO = new ProdutoDAO();
                                produtoDAO.Update(objetoProduto);

                                break;
                            case nameof(Medicamento):

                                Medicamento objetoMedicamento = new MedicamentoDAO().GetById(objetoItem.Id);

                                var vendaMedicamento = new VendaMedicamento
                                {
                                    QuantidadeItem = item.Quantidade,
                                    ValorItem = item.Quantidade * objetoItem.ValorCompra,
                                    Venda = new VendaDAO().GetById(lastIdVend),
                                    Medicamento = objetoMedicamento
                                };

                                objetoMedicamento.Quantidade -= item.Quantidade;

                                var vendaMedicamentoDAO = new VendaMedicamentoDAO();
                                vendaMedicamentoDAO.Insert(vendaMedicamento);

                                var medicamentoDAO = new MedicamentoDAO();
                                medicamentoDAO.Update(objetoMedicamento);

                                break;
                            default:
                                break;
                        }
                    }
                    carrinho.Clear();
                    AtualizarValorTotalCarrinho();
                    dgvProdutos.ItemsSource = null;
                    dgvProdutos.ItemsSource = carrinho;
                    Utils.ResetControls(this);
                    LoadData();
                }
                else
                {
                    if (!isParcelasValidas && isCartaoCreditoSelected && !string.IsNullOrEmpty(edParcelas.Text))
                    {
                        MessageBox.Show("O número de parcelas deve ser maior que zero.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Preencha todos os campos.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("O carrinho esta vazio.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
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
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Nenhum item selecionado para exclusão.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
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
                        TipoObjeto itemSelecionado = cbProduto.SelectedItem as TipoObjeto;
                        dynamic objeto = itemSelecionado.Objeto;
                        int estoqueAtual = objeto.Quantidade;

                        CarrinhoItem itemExistente = carrinho.FirstOrDefault(item => item.TipoObjeto == objetoSelecionado);

                        if (itemExistente != null)
                        {
                            int quantidadeDisponivel = estoqueAtual - itemExistente.Quantidade;
                            if (quantidade > quantidadeDisponivel)
                            {
                                MessageBox.Show($"Quantidade indisponível em estoque. Apenas {quantidadeDisponivel} unidades disponíveis.", "Estoque Insuficiente", MessageBoxButton.OK, MessageBoxImage.Warning);
                                edQuant.Text = quantidadeDisponivel.ToString();
                                return;
                            }

                            itemExistente.Quantidade += quantidade;

                            if (itemExistente.Quantidade == estoqueAtual)
                            {
                                LoadData();
                            }
                        }
                        else
                        {
                            if (quantidade > estoqueAtual)
                            {
                                MessageBox.Show($"Quantidade indisponível em estoque. Apenas {estoqueAtual} unidades disponíveis.", "Estoque Insuficiente", MessageBoxButton.OK, MessageBoxImage.Warning);
                                edQuant.Text = estoqueAtual.ToString();
                                return;
                            }

                            CarrinhoItem novoItem = new CarrinhoItem
                            {
                                TipoObjeto = objetoSelecionado,
                                Quantidade = quantidade,
                            };

                            carrinho.Add(novoItem);

                            if (novoItem.Quantidade == estoqueAtual)
                            {
                                LoadData();
                            }
                        }

                        dgvProdutos.ItemsSource = null;
                        dgvProdutos.ItemsSource = carrinho;
                        AtualizarValorTotalCarrinho();
                        Utils.ResetColors(this);
                        cbProduto.SelectedIndex = -1;
                        edQuant.Clear();
                        edValorUnitario.Clear();
                    }
                    else
                    {
                        MessageBox.Show("A quantidade deve ser um número válido maior que zero.", "Erro de Entrada", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao adicionar item ao carrinho: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Prencha todos os campos.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Error);
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

            double descontoPercentual = 0;
            if (double.TryParse(edDesconto.Text, out descontoPercentual))
            {
                double fatorDesconto = 1 - (descontoPercentual / 100.0);
                double valorComDesconto = total * fatorDesconto;
                edValorTotal.Content = "VALOR TOTAL: " + valorComDesconto.ToString() + " R$";
                ValorTotal = valorComDesconto;
            }
            else
            {
                edValorTotal.Content = "VALOR TOTAL: " + total.ToString() + " R$";
                ValorTotal = total;
            }
        }
    }
}
