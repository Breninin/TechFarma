using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfTechPharma.Auxiliares;
using WpfTechPharma.Modelos;

namespace WpfTechPharma.Janelas
{
    public partial class JanCadastrarFuncionario : Window
    {
        public JanCadastrarFuncionario()
        {
            InitializeComponent();
            LoadData();
            edNome.TextChanged += TextBox_TextChanged;
            edContato.TextChanged += TextBox_TextChanged;
            edRG.TextChanged += TextBox_TextChanged;
            edEmail.TextChanged += TextBox_TextChanged;
            cbEndereco.LostFocus += ComboBox_LostFocus;
            cbSexo.LostFocus += ComboBox_LostFocus;
            edCPF.LostFocus += EdCPF_LostFocus;
            dpDataNascimento.LostFocus += DatePicker_LostFocus;
            edfuncao.TextChanged += TextBox_TextChanged;
            edsalario.TextChanged += TextBox_TextChanged;
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

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            Ultis.check(textBox);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Deseja Realmente Cancelar?", "Aviso", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            List<bool> check = new List<bool>();
            check.Add(Ultis.check(edNome));
            check.Add(Ultis.check(edContato));
            check.Add(Ultis.check(edCPF, 11));
            check.Add(Ultis.check(edRG));
            check.Add(Ultis.check(edEmail));
            check.Add(Ultis.check(cbEndereco));
            check.Add(Ultis.check(cbSexo));
            check.Add(Ultis.check(dpDataNascimento));
            check.Add(Ultis.check(edfuncao));
            check.Add(Ultis.check(edsalario));


            if (check.All(c => c == true))
            {
                try
                {
                    var funcionario = new Funcionario();
                    funcionario.Nome = edNome.Text;
                    funcionario.Sexo = cbSexo.Text;
                    funcionario.Nascimento = (DateTime)dpDataNascimento.SelectedDate;
                    funcionario.RG = edRG.Text;
                    funcionario.CPF = edCPF.Text;
                    funcionario.Email = edEmail.Text;
                    funcionario.Contato = edContato.Text;
                    funcionario.Funcao = edfuncao.Text;
                    funcionario.Salario = float.Parse(edsalario.Text);
                    funcionario.Endereco = (Endereco)cbEndereco.SelectedItem;

                    var funcionarioDAO = new FuncionarioDAO();
                    funcionarioDAO.Insert(funcionario);

                    MessageBox.Show("Funcionário inserido com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao inserir o funcionário: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
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
