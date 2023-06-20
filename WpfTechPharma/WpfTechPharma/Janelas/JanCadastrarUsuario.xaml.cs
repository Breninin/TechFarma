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
using WpfTechPharma.Auxiliares;
using WpfTechPharma.Modelos;

namespace WpfTechPharma.Janelas
{
    /// <summary>
    /// Lógica interna para JanCadastrarLogin.xaml
    /// </summary>
    public partial class JanCadastrarUsuario : Window
    {
        public JanCadastrarUsuario()
        {
            InitializeComponent();
            InicializarManipuladoresEventos();
            LoadData();
        }

        // Inicializa os manipuladores de eventos para os controles da janela
        private void InicializarManipuladoresEventos()
        {
            edSenhaUsuario.TextChanged += TextBox_TextChanged_Dual_2;
            edConfirmarSenha.TextChanged += TextBox_TextChanged_Dual_1;
            edNomeUsuario.TextChanged += TextBox_TextChanged;
            cbNomeFuncionario.SelectionChanged += ComboBox_SelectionChanged;
        }

        // Manipulador de evento para alterações de texto em TextBoxes
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            Ultis.Check(this, textBox);
        }

        // Manipulador de evento para alterações de texto em TextBoxes (comparação de duas TextBoxes)
        private void TextBox_TextChanged_Dual_1(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            Ultis.CheckBoxEqual(this, textBox, edSenhaUsuario);
        }

        // Manipulador de evento para alterações de texto em TextBoxes (comparação de duas TextBoxes)
        private void TextBox_TextChanged_Dual_2(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            Ultis.CheckBoxEqual(this, textBox, edConfirmarSenha);
        }

        // Manipulador de evento para alterações de seleção em ComboBoxes
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            Ultis.Check(this, comboBox);
        }

        // Manipulador de evento para o botão "Limpar"
        private void BtLimpar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Deseja realmente cancelar?", "Aviso", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                Ultis.ResetControls(this);
        }

        // Manipulador de evento para o botão "Salvar"
        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            List<bool> check = new List<bool>
            {
                Ultis.Check(this, edNomeUsuario),
                Ultis.Check(this, cbNomeFuncionario),
                Ultis.CheckBoxEqual(this, edSenhaUsuario, edConfirmarSenha)
            };

            if (check.All(c => c))
            {
                try
                {
                    var usuario = new Usuario
                    {
                        NomeUsuario = edNomeUsuario.Text,
                        Senha = edSenhaUsuario.Text,
                        Funcionario = (Funcionario)cbNomeFuncionario.SelectedItem
                    };

                    var usuarioDAO = new UsuarioDAO();
                    usuarioDAO.Insert(usuario);

                    MessageBox.Show("Login inserido com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao inserir o login: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                Ultis.ResetControls(this);
            }
            else
            {
                check.Clear();
            }
        }

        // Carrega os dados para o ComboBox "cbNomeFuncionario"
        private void LoadData()
        {
            try
            {
                cbNomeFuncionario.ItemsSource = null;
                cbNomeFuncionario.Items.Clear();
                cbNomeFuncionario.ItemsSource = new FuncionarioDAO().List();
                cbNomeFuncionario.DisplayMemberPath = "Nome";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Não Executado", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
