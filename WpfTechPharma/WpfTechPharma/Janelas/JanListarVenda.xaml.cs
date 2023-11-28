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
    /// Lógica interna para JanListarVenda.xaml
    /// </summary>
    public partial class JanListarVenda : Window
    {
        public JanListarVenda()
        {
            InitializeComponent();
            CarregarVendas();
        }

        private void CarregarVendas()
        {
            try
            {
                VendaDAO vendaDAO = new VendaDAO();
                List<Venda> vendas = vendaDAO.List();
                dgvVendas.ItemsSource = vendas;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar os vendas: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
