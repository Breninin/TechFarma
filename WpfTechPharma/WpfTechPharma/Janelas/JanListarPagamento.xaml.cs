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
    /// Lógica interna para JanListarPagamento.xaml
    /// </summary>
    public partial class JanListarPagamento : Window
    {
        public JanListarPagamento()
        {
            InitializeComponent();
            CarregarPagamentos();
        }

        private void CarregarPagamentos()
        {
            try
            {
                PagamentoDAO PagamentoDAO = new PagamentoDAO();
                List<Pagamento> Pagamentos = PagamentoDAO.List();
                dgvPagamentos.ItemsSource = Pagamentos;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar os Pagamentos: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
