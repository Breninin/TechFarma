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
    }
}
