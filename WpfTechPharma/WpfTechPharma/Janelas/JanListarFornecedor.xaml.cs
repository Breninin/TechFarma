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
    /// Lógica interna para JanListarFornecedor.xaml
    /// </summary>
    public partial class JanListarFornecedor : Window
    {
        public JanListarFornecedor()
        {
            InitializeComponent();
            CarregarFornecedores();
        }

        private void CarregarFornecedores()
        {
            try
            {
                FornecedorDAO fornecedorDAO = new FornecedorDAO();
                List<Fornecedor> fornecedores = fornecedorDAO.List();
                dgvFornecedores.ItemsSource = fornecedores;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar os fornecedores: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btEditar_Click(object sender, RoutedEventArgs e)
        {
            var fornecedorSelected = dgvFornecedores.SelectedItem as Fornecedor;

            var jan = new JanCadastrarFornecedor(fornecedorSelected.Id);
            jan.ShowDialog();
            CarregarFornecedores();
        }

        private void btExluir_Click(object sender, RoutedEventArgs e)
        {
            var fornecedorSelected = dgvFornecedores.SelectedItem as Fornecedor;

            var result = MessageBox.Show($"Deseja mesmo remover o fornecedor {fornecedorSelected.NomeFantasia}?", "Excluir", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            try
            {
                if (result == MessageBoxResult.Yes)
                {
                    var fornecedorDAO = new FornecedorDAO();
                    fornecedorDAO.Delete(fornecedorSelected);
                    CarregarFornecedores();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao excluir o fornecedor: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
