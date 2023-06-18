using MaterialDesignThemes.Wpf;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
using WpfTechPharma.Auxiliares;
using WpfTechPharma.Modelos;

namespace WpfTechPharma.Janelas
{
    public partial class JanCadastrarCliente : Window
    {
        public JanCadastrarCliente()
        {
            InitializeComponent();
            LoadData();
            edNome.TextChanged += TextBox_TextChanged;
            edCelular.TextChanged += TextBox_TextChanged;
            edRG.TextChanged += TextBox_TextChanged;
            edEmail.TextChanged += TextBox_TextChanged;
            cbEndereco.LostFocus += ComboBox_LostFocus;
            cbSexo.LostFocus += ComboBox_LostFocus;
            edCPF.LostFocus += EdCPF_LostFocus;
            dpDataNascimento.LostFocus += DatePicker_LostFocus;
        }

        private void EdCPF_LostFocus(object sender, RoutedEventArgs e)
        {
            Ultis.check(edCPF, 11);
        }

        private void ComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            Ultis.check(comboBox);
        }

        private void DatePicker_LostFocus(object sender, RoutedEventArgs e)
        {
            DatePicker datePicker = (DatePicker)sender;
            Ultis.check(datePicker);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Deseja Realmente Cancelar?", "Aviso", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            Ultis.check(textBox);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<bool> check = new List<bool>();
            check.Add(Ultis.check(edNome));
            check.Add(Ultis.check(edCelular));
            check.Add(Ultis.check(edCPF, 11));
            check.Add(Ultis.check(edRG));
            check.Add(Ultis.check(edEmail));
            check.Add(Ultis.check(cbEndereco));
            check.Add(Ultis.check(cbSexo));
            check.Add(Ultis.check(dpDataNascimento));

            if (check.All(c => c == true))
            {
                var Cliente = new Cliente();
                Cliente.Nome = edNome.Text;
                Cliente.Email = edEmail.Text;
                Cliente.Contato = edCelular.Text;
                Cliente.RG = edRG.Text;
                Cliente.CPF = edCPF.Text;
                Cliente.Nascimento = (DateTime)dpDataNascimento.SelectedDate;
                Cliente.Sexo = cbSexo.Text;
                Cliente.Endereco = (Endereco) cbEndereco.SelectedItem;

                try
                {
                    var clienteDAO = new ClienteDAO();
                    clienteDAO.Insert(Cliente);
                    MessageBox.Show("Cliente inserido com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao inserir o cliente: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                Close();
            }
            else
            {
                check.Clear();
            }
        }

        private void LoadData()
        {
            dpDataNascimento.SelectedDate = DateTime.Now;
            try
            {
                cbEndereco.ItemsSource = null; 
                cbEndereco.Items.Clear();
                cbEndereco.ItemsSource = new EnderecoDAO().List();
                cbEndereco.DisplayMemberPath = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Não Executado", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}