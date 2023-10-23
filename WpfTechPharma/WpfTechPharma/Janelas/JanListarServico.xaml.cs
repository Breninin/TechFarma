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
    /// Lógica interna para JanListarServico.xaml
    /// </summary>
    public partial class JanListarServico : Window
    {
        public JanListarServico()
        {
            InitializeComponent();
            CarregarServicos();
        }

        private void CarregarServicos()
        {
            try
            {
                ServicoDAO servicoDAO = new ServicoDAO();
                List<Servico> servicos = servicoDAO.List();
                dgvServicos.ItemsSource = servicos;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar os serviços: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btEditar_Click(object sender, RoutedEventArgs e)
        {
            var servicoSelected = dgvServicos.SelectedItem as Servico;

            var jan = new JanCadastrarServico(servicoSelected.Id);
            jan.ShowDialog();
            CarregarServicos();
        }

        private void btExluir_Click(object sender, RoutedEventArgs e)
        {
            var servicoSelected = dgvServicos.SelectedItem as Servico;

            var result = MessageBox.Show($"Deseja mesmo remover o Servico {servicoSelected.Nome}?", "Excluir", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            try
            {
                if (result == MessageBoxResult.Yes)
                {
                    var servicoDAO = new ServicoDAO();
                    servicoDAO.Delete(servicoSelected);
                    CarregarServicos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao excluir o Servico: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
