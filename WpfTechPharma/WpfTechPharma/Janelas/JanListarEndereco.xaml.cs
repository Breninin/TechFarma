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
    /// Lógica interna para JanListarEndereco.xaml
    /// </summary>
    public partial class JanListarEndereco : Window
    {
        public JanListarEndereco()
        {
            InitializeComponent();
            CarregarEnderecos();
        }

        private void CarregarEnderecos()
        {
            try
            {
                EnderecoDAO enderecoDAO = new EnderecoDAO();
                List<Endereco> enderecos = enderecoDAO.List();
                dgvEnderecos.ItemsSource = enderecos;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar os endereços: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btEditar_Click(object sender, RoutedEventArgs e)
        {
            var enderecoSelected = dgvEnderecos.SelectedItem as Endereco;

            var jan = new JanCadastrarEndereco(enderecoSelected.Id);
            jan.ShowDialog();
            CarregarEnderecos();
        }
        

        private void btExluir_Click(object sender, RoutedEventArgs e)
        {
            var enderecoSelected = dgvEnderecos.SelectedItem as Endereco;

            var result = MessageBox.Show($"Deseja mesmo remover o endereco {enderecoSelected.CEP}?", "Excluir", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            try
            {
                if (result == MessageBoxResult.Yes)
                {
                    var EnderecoDAO = new EnderecoDAO();
                    EnderecoDAO.Delete(enderecoSelected);
                    CarregarEnderecos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao excluir o endereco: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
