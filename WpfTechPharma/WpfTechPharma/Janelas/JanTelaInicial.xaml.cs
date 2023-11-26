using System;
using System.CodeDom;
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
    /// Lógica interna para JanTelaInicial.xaml
    /// </summary>
    public partial class JanTelaInicial : Window
    {
        public JanTelaInicial()
        {
            InitializeComponent();
            InicializarManipuladoresEventos();
        }

        private void InicializarManipuladoresEventos()
        {
            edLogin.TextChanged += TextBox_TextChanged;
            edSenha.TextChanged += TextBox_TextChanged;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            Utils.Check(this, textBox);
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = (PasswordBox)sender;
            Utils.Check(this, passwordBox);
        }

        private void btEntrar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Utils.Check(this, edLogin) && Utils.Check(this, edSenha))
                {
                    var dao = new UsuarioDAO();
                    var usuario = dao.Login(edLogin.Text, edSenha.Text);

                    if (usuario.NomeUsuario == edLogin.Text &&
                        usuario.Senha == edSenha.Text)
                    {
                        MainWindow mainWindow = new MainWindow(usuario.NomeUsuario);
                        mainWindow.Show();
                        this.Close();
                    }
                    else
                    {
                        throw new Exception("Usuario ou Senha incorretos!");
                    }
                }
                else
                {
                    throw new Exception("Informe o Usuario e a Senha!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao realizar o login: " + ex.Message, "ERRO", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btCadastrar_Click(object sender, RoutedEventArgs e)
        {
            JanCadastrarUsuario Jan = new JanCadastrarUsuario();
            Jan.Show();
        }

        /*
        private void chMostrarSenha_Checked(object sender, RoutedEventArgs e)
        {
            edSenha.PasswordChar = '\0';
        }

        private void chMostrarSenha_Unchecked(object sender, RoutedEventArgs e)
        {
            edSenha.PasswordChar = '•';
        }
        */
    }
}
