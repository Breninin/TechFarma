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
    /// Lógica interna para JanListarRecebimento.xaml
    /// </summary>
    public partial class JanListarRecebimento : Window
    {
        public JanListarRecebimento()
        {
            InitializeComponent();
            CarregarRecebimentos();
        }

        private void CarregarRecebimentos()
        {
            try
            {
                RecebimentoDAO RecebimentoDAO = new RecebimentoDAO();
                List<Recebimento> Recebimentos = RecebimentoDAO.List();
                dgvRecebimentos.ItemsSource = Recebimentos;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar os Recebimentos: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
