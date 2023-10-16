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
    /// Lógica interna para JanListarInsumo.xaml
    /// </summary>
    public partial class JanListarInsumo : Window
    {
        public JanListarInsumo()
        {
            InitializeComponent();
            CarregarInsumos();
        }

        private void CarregarInsumos()
        {
            try
            {
                InsumoDAO insumoDAO = new InsumoDAO();
                List<Insumo> insumos = insumoDAO.List();
                dgvInsumos.ItemsSource = insumos;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar os insumos: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btEditar_Click(object sender, RoutedEventArgs e)
        {
            var insumoSelected = dgvInsumos.SelectedItem as Insumo;

            var jan = new JanCadastrarInsumo(insumoSelected.Id);
            jan.ShowDialog();
            CarregarInsumos();
        }

        private void btExluir_Click(object sender, RoutedEventArgs e)
        {
            var insumoSelected = dgvInsumos.SelectedItem as Insumo;

            var result = MessageBox.Show($"Deseja mesmo remover o insumo {insumoSelected.Nome}?", "Excluir", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            try
            {
                if (result == MessageBoxResult.Yes)
                {
                    var insumoDAO = new InsumoDAO();
                    insumoDAO.Delete(insumoSelected);
                    CarregarInsumos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao excluir o insumo: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
