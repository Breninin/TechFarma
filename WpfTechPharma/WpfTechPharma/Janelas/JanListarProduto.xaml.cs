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
using WpfTechPharma.Modelos;

namespace WpfTechPharma.Janelas
{
    /// <summary>
    /// Lógica interna para JanListarProduto.xaml
    /// </summary>
    public partial class JanListarProduto : Window
    {
        public JanListarProduto()
        {
            InitializeComponent();
            CarregarProdutos();
        }

        private void CarregarProdutos()
        {
            try
            {
                ProdutoDAO ProdutoDAO = new ProdutoDAO();
                List<Produto> Produtos = ProdutoDAO.List();
                dgvProdutos.ItemsSource = Produtos;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar os Produtos: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btEditar_Click(object sender, RoutedEventArgs e)
        {
            var produtoSelected = dgvProdutos.SelectedItem as Produto;

            var jan = new JanCadastrarProduto(produtoSelected.Id);
            jan.ShowDialog();
            CarregarProdutos();
        }

        private void btExluir_Click(object sender, RoutedEventArgs e)
        {
            var produtoSelected = dgvProdutos.SelectedItem as Produto;

            var result = MessageBox.Show($"Deseja mesmo remover o Produto {produtoSelected.Nome}?", "Excluir", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            try
            {
                if (result == MessageBoxResult.Yes)
                {
                    var produtoDAO = new ProdutoDAO();
                    produtoDAO.Delete(produtoSelected);
                    CarregarProdutos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao excluir o Produto: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
