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

        private void btCadastrarServiço_Click(object sender, RoutedEventArgs e)
        {
            JanCadastrarServico Jan = new JanCadastrarServico();
            Jan.Show();
        }

        private void btCadastrarLogin_Click(object sender, RoutedEventArgs e)
        {
            JanCadastrarLogin Jan = new JanCadastrarLogin();
            Jan.Show();
        }
    }
}