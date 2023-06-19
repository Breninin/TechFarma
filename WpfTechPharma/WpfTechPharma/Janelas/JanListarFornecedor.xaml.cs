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
    public partial class JanListarFornecedor : Window
    {
        public JanListarFornecedor()
        {
            InitializeComponent();
            CarregarFornecedores();
        }

        private void CarregarFornecedores()
        {
            try
            {
                FornecedorDAO fornecedorDAO = new FornecedorDAO();
                List<Fornecedor> fornecedores = fornecedorDAO.List();
                dgvFornecedores.ItemsSource = fornecedores;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar os fornecedores: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
