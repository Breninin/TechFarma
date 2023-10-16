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
    /// Lógica interna para JanListarCliente.xaml
    /// </summary>
    public partial class JanListarCliente : Window
    {
        public JanListarCliente()
        {
            InitializeComponent();
            CarregarClientes();
        }

        private void CarregarClientes()
        {
            try
            {
                ClienteDAO clienteDAO = new ClienteDAO();
                List<Cliente> clientes = clienteDAO.List();
                dgvClientes.ItemsSource = clientes;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar os clientes: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btEditar_Click(object sender, RoutedEventArgs e)
        {
            var clienteSelected = dgvClientes.SelectedItem as Cliente;

            var jan = new JanCadastrarCliente(clienteSelected.Id);
            jan.ShowDialog();
            CarregarClientes();
        }

        private void btExluir_Click(object sender, RoutedEventArgs e)
        {
            var clienteSelected = dgvClientes.SelectedItem as Cliente;

            var result = MessageBox.Show($"Deseja mesmo remover o cliente {clienteSelected.Nome}?", "Excluir", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            
            try
            {
                if (result == MessageBoxResult.Yes)
                {
                    var clienteDAO = new ClienteDAO();
                    clienteDAO.Delete(clienteSelected);
                    CarregarClientes();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao excluir o cliente: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
