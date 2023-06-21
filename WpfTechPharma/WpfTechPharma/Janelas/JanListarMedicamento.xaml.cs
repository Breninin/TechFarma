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
    /// Lógica interna para JanListarMedicamento.xaml
    /// </summary>
    public partial class JanListarMedicamento : Window
    {
        public JanListarMedicamento()
        {
            InitializeComponent();
            CarregarMedicamentos();
        }

        private void CarregarMedicamentos()
        {
            try
            {
                MedicamentoDAO MedicamentoDAO = new MedicamentoDAO();
                List<Medicamento> Medicamentos = MedicamentoDAO.List();
                dgvMedicamentos.ItemsSource = Medicamentos;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar os Medicamentos: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
