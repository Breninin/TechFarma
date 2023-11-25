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
using WpfTechPharma.Janelas;

namespace WpfTechPharma
{
    /// <summary>
    /// Lógica interna para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Atualize o TextBlock com a data e a hora atual
            UpdateDateTime();
        }

        // Método para atualizar a data e a hora
        private void UpdateDateTime()
        {
            DateTimeTextBlock.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdateDateTime();
        }

        private void Expander_1_Expanded(object sender, RoutedEventArgs e)
        {
            Expander_2.IsExpanded = false;
        }

        private void Expander_2_Expanded(object sender, RoutedEventArgs e)
        {
            Expander_1.IsExpanded = false;
        }

        // MOMENTO DIVERSIDADE

        private void Caixa_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Compra_Click(object sender, RoutedEventArgs e)
        {
            JanRealizarCompra Jan = new JanRealizarCompra();
            Jan.Show();
        }

        private void Venda_Click(object sender, RoutedEventArgs e)
        {
            JanRealizarVenda Jan = new JanRealizarVenda();
            Jan.Show();
        }

        private void Relatorio_Click(object sender, RoutedEventArgs e)
        {

        }

        //CADASTRAR

        private void CadastroCliente_Click(object sender, RoutedEventArgs e)
        {
            JanCadastrarCliente Jan = new JanCadastrarCliente();
            Jan.ShowDialog();
        }

        private void CadastroEndereco_Click(object sender, RoutedEventArgs e)
        {
            JanCadastrarEndereco Jan = new JanCadastrarEndereco();
            Jan.ShowDialog();
        }

        private void CadastroFornecedor_Click(object sender, RoutedEventArgs e)
        {
            JanCadastrarFornecedor Jan = new JanCadastrarFornecedor();
            Jan.ShowDialog();
        }

        private void CadastroFuncionario_Click(object sender, RoutedEventArgs e)
        {
            JanCadastrarFuncionario Jan = new JanCadastrarFuncionario();
            Jan.ShowDialog();
        }

        private void CadastroInsumo_Click(object sender, RoutedEventArgs e)
        {
            JanCadastrarInsumo Jan = new JanCadastrarInsumo();
            Jan.ShowDialog();
        }

        private void CadastroMedicamento_Click(object sender, RoutedEventArgs e)
        {
            JanCadastrarMedicamento Jan = new JanCadastrarMedicamento();
            Jan.ShowDialog();
        }

        private void CadastroProduto_Click(object sender, RoutedEventArgs e)
        {
            JanCadastrarProduto Jan = new JanCadastrarProduto();
            Jan.ShowDialog();
        }

        private void CadastroServico_Click(object sender, RoutedEventArgs e)
        {
            JanCadastrarServico Jan = new JanCadastrarServico();
            Jan.ShowDialog();
        }

        private void CadastroUsuario_Click(object sender, RoutedEventArgs e)
        {
            JanCadastrarUsuario Jan = new JanCadastrarUsuario();
            Jan.ShowDialog();
        }

        //LISTAR

        private void ConsultaCliente_Click(object sender, RoutedEventArgs e)
        {
            JanListarCliente Jan = new JanListarCliente();
            Jan.ShowDialog();
        }

        private void ConsultaEndereco_Click(object sender, RoutedEventArgs e)
        {
            JanListarEndereco Jan = new JanListarEndereco();
            Jan.ShowDialog();
        }

        private void ConsultaFornecedor_Click(object sender, RoutedEventArgs e)
        {
            JanListarFornecedor Jan = new JanListarFornecedor();
            Jan.ShowDialog();
        }

        private void ConsultaFuncionario_Click(object sender, RoutedEventArgs e)
        {
            JanListarFuncionario Jan = new JanListarFuncionario();
            Jan.ShowDialog();
        }

        private void ConsultaInsumo_Click(object sender, RoutedEventArgs e)
        {
            JanListarInsumo Jan = new JanListarInsumo();
            Jan.ShowDialog();
        }

        private void ConsultaMedicamento_Click(object sender, RoutedEventArgs e)
        {
            JanListarMedicamento Jan = new JanListarMedicamento();
            Jan.ShowDialog();
        }

        private void ConsultaProduto_Click(object sender, RoutedEventArgs e)
        {
            JanListarProduto Jan = new JanListarProduto();
            Jan.ShowDialog();
        }

        private void ConsultaServico_Click(object sender, RoutedEventArgs e)
        {
            JanListarServico Jan = new JanListarServico();
            Jan.ShowDialog();
        }

        private void ConsultaUsuario_Click(object sender, RoutedEventArgs e)
        {
            JanListarUsuario Jan = new JanListarUsuario();
            Jan.ShowDialog();
        }
    }
}
