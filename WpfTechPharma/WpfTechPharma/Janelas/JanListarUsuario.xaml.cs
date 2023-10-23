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
    /// Lógica interna para JanListarLogin.xaml
    /// </summary>
    public partial class JanListarUsuario : Window
    {
        public JanListarUsuario()
        {
            InitializeComponent();
            CarregarUsuarios();
        }

        private void CarregarUsuarios()
        {
            try
            {
                UsuarioDAO usuarioDAO = new UsuarioDAO();
                List<Usuario> usuarios = usuarioDAO.List();
                dgvUsuarios.ItemsSource = usuarios;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar os usuarios: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btEditar_Click(object sender, RoutedEventArgs e)
        {
            var usuarioSelected = dgvUsuarios.SelectedItem as Usuario;

            var jan = new JanCadastrarUsuario(usuarioSelected.Id);
            jan.ShowDialog();
            CarregarUsuarios();
        }

        private void btExluir_Click(object sender, RoutedEventArgs e)
        {
            var usuarioSelected = dgvUsuarios.SelectedItem as Usuario;

            var result = MessageBox.Show($"Deseja mesmo remover o Usuario {usuarioSelected.NomeUsuario}?", "Excluir", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            try
            {
                if (result == MessageBoxResult.Yes)
                {
                    var usuarioDAO = new UsuarioDAO();
                    usuarioDAO.Delete(usuarioSelected);
                    CarregarUsuarios();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao excluir o Usuario: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
