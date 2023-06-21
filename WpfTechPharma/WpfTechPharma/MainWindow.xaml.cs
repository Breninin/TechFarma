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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfTechPharma.Janelas;

namespace WpfTechPharma
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //CADASTRAR

        private void btCadastrarCliente_Click(object sender, RoutedEventArgs e)
        {
            JanCadastrarCliente Jan = new JanCadastrarCliente();
            Jan.Show();
        }

        private void btCadastrarFuncionario_Click(object sender, RoutedEventArgs e)
        {
            JanCadastrarFuncionario Jan = new JanCadastrarFuncionario();
            Jan.Show();
        }

        private void btCadastrarFornecedor_Click(object sender, RoutedEventArgs e)
        {
            JanCadastrarFornecedor Jan = new JanCadastrarFornecedor();
            Jan.Show();
        }

        private void btCadastrarMedicamento_Click(object sender, RoutedEventArgs e)
        {
            JanCadastrarMedicamento Jan = new JanCadastrarMedicamento();
            Jan.Show();
        }

        private void btCadastrarProduto_Click(object sender, RoutedEventArgs e)
        {
            JanCadastrarProduto Jan = new JanCadastrarProduto();
            Jan.Show();
        }

        private void btCadastrarInsumo_Click(object sender, RoutedEventArgs e)
        {
            JanCadastrarInsumo Jan = new JanCadastrarInsumo();
            Jan.Show();
        }

        private void btCadastrarServiço_Click(object sender, RoutedEventArgs e)
        {
            JanCadastrarServico Jan = new JanCadastrarServico();
            Jan.Show();
        }

        private void btCadastrarEndereco_Click(object sender, RoutedEventArgs e)
        {
            JanCadastrarEndereco Jan = new JanCadastrarEndereco();
            Jan.Show();
        }

        private void btCadastrarUsuario_Click(object sender, RoutedEventArgs e)
        {
            JanCadastrarUsuario Jan = new JanCadastrarUsuario();
            Jan.Show();
        }

        //LISTAR

        private void btListarCliente_Click(object sender, RoutedEventArgs e)
        {
            JanListarCliente Jan = new JanListarCliente();
            Jan.Show();
        }

        private void btListarFuncionario_Click(object sender, RoutedEventArgs e)
        {
            JanListarFuncionario Jan = new JanListarFuncionario();
            Jan.Show();
        }

        private void btListarFornecedor_Click(object sender, RoutedEventArgs e)
        {
            JanListarFornecedor Jan = new JanListarFornecedor();
            Jan.Show();
        }

        private void btListarMedicamento_Click(object sender, RoutedEventArgs e)
        {
            JanListarMedicamento Jan = new JanListarMedicamento();
            Jan.Show();
        }

        private void btListarProduto_Click(object sender, RoutedEventArgs e)
        {
            JanListarProduto Jan = new JanListarProduto();
            Jan.Show();
        }
        private void btListarInsumo_Click(object sender, RoutedEventArgs e)
        {
            JanListarInsumo Jan = new JanListarInsumo();
            Jan.Show();
        }

        private void btListarServiço_Click(object sender, RoutedEventArgs e)
        {
            JanListarServico Jan = new JanListarServico();
            Jan.Show();
        }

        private void btListarEndereco_Click(object sender, RoutedEventArgs e)
        {
            JanListarEndereco Jan = new JanListarEndereco();
            Jan.Show();
        }

        private void btListarUsuario_Click(object sender, RoutedEventArgs e)
        {
            JanListarUsuario Jan = new JanListarUsuario();
            Jan.Show();
        }
    }
}
