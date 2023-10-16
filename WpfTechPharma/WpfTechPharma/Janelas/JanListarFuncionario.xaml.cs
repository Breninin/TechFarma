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
    /// Lógica interna para JanListarFuncionario.xaml
    /// </summary>
    public partial class JanListarFuncionario : Window
    {
        public JanListarFuncionario()
        {
            InitializeComponent();
            CarregarFuncionarios();
        }

        private void CarregarFuncionarios()
        {
            try
            {
                List<Funcionario> funcionarios = new FuncionarioDAO().List();
                dgvFuncionarios.ItemsSource = funcionarios;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar os funcionários: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btEditar_Click(object sender, RoutedEventArgs e)
        {
            var funcionarioSelected = dgvFuncionarios.SelectedItem as Funcionario;

            var jan = new JanCadastrarFuncionario(funcionarioSelected.Id);
            jan.ShowDialog();
            CarregarFuncionarios();
        }

        private void btExluir_Click(object sender, RoutedEventArgs e)
        {
            var funcionarioSelected = dgvFuncionarios.SelectedItem as Funcionario;

            var result = MessageBox.Show($"Deseja mesmo remover o funcionario {funcionarioSelected.Nome}?", "Excluir", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            try
            {
                if (result == MessageBoxResult.Yes)
                {
                    var FuncionarioDAO = new FuncionarioDAO();
                    FuncionarioDAO.Delete(funcionarioSelected);
                    CarregarFuncionarios();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao excluir o funcionario: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
