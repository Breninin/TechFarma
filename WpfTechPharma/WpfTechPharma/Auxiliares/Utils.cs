using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfTechPharma.Auxiliares
{
    static class Ultis
    {
        private static Dictionary<Control, Brush> originalBorderBrushes;
        private static Dictionary<Control, Brush> originalForegroundBrushes;

        // Salva as cores originais dos controles da janela
        public static void SaveColors(Window window)
        {
            originalBorderBrushes = new Dictionary<Control, Brush>();
            originalForegroundBrushes = new Dictionary<Control, Brush>();

            foreach (Control control in FindVisualChildren<Control>(window))
            {
                originalBorderBrushes[control] = control.BorderBrush;
                originalForegroundBrushes[control] = control.Foreground;
            }
        }

        // Restaura as cores originais dos controles da janela
        public static void ResetColors(Window window)
        {
            foreach (Control control in FindVisualChildren<Control>(window))
            {
                control.ClearValue(Control.BorderBrushProperty);
                control.ClearValue(Control.ForegroundProperty);
            }
        }

        // Redefine os controles da janela para os valores padrão
        public static void ResetControls(Window window)
        {
            SaveColors(window);

            foreach (Control control in FindVisualChildren<Control>(window))
            {
                switch (control)
                {
                    case TextBox textBox:
                        textBox.Clear();
                        break;

                    case ComboBox comboBox:
                        comboBox.SelectedIndex = -1;
                        break;

                    case DatePicker datePicker:
                        datePicker.SelectedDate = null;
                        break;

                    default:
                        break;
                }

                control.ClearValue(Control.BorderBrushProperty);
                control.ClearValue(Control.ForegroundProperty);
            }
        }

        // Método para encontrar todos os controles filho de um elemento visual
        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject dependencyObject) where T : DependencyObject
        {
            if (dependencyObject != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(dependencyObject); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(dependencyObject, i);
                    if (child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        // Verifica se um TextBox possui a quantidade correta de caracteres
        public static bool Check(Window window, TextBox e, int length)
        {
            SaveColors(window);
            string text = e.Text.Replace("_", "").Replace(".", "").Replace("-", "");

            if (text.Length == length)
            {
                SetValidColors(e);
                return true;
            }
            else
            {
                SetInvalidColors(e);
                return false;
            }
        }

        // Verifica se um ComboBox está selecionado
        public static bool Check(Window window, ComboBox e)
        {
            SaveColors(window);
            if (e.SelectedIndex != -1)
            {
                SetValidColors(e);
                return true;
            }
            else
            {
                SetInvalidColors(e);
                return false;
            }
        }

        // Verifica se um TextBox não está vazio
        public static bool Check(Window window, TextBox e)
        {
            SaveColors(window);
            if (!string.IsNullOrEmpty(e.Text))
            {
                SetValidColors(e);
                return true;
            }
            else
            {
                SetInvalidColors(e);
                return false;
            }
        }

        // Verifica se um DatePicker possui uma data selecionada
        public static bool Check(Window window, DatePicker e)
        {
            SaveColors(window);
            if (e.SelectedDate != null)
            {
                SetValidColors(e);
                return true;
            }
            else
            {
                SetInvalidColors(e);
                return false;
            }
        }

        //Verificar se texbox são iguais
        public static bool CheckBoxEqual(Window window, TextBox textBox1, TextBox textBox2)
        {
            SaveColors(window);
            if (textBox1.Text == textBox2.Text && !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrEmpty(textBox1.Text))
            {
                SetValidColors(textBox1);
                SetValidColors(textBox2);
                return true;
            }
            else
            {
                SetInvalidColors(textBox1);
                SetInvalidColors(textBox2);
                return false;
            }
        }

        // Define as cores de destaque para um controle válido
        private static void SetValidColors(Control control)
        {
            control.BorderBrush = (Brush)new BrushConverter().ConvertFromString("#8937FF00");
            control.Foreground = (Brush)new BrushConverter().ConvertFromString("#FF0E9403");
        }

        // Define as cores de destaque para um controle inválido
        private static void SetInvalidColors(Control control)
        {
            control.BorderBrush = (Brush)new BrushConverter().ConvertFromString("#89E40000");
            control.Foreground = (Brush)new BrushConverter().ConvertFromString("#FFCC1212");
        }
    }
}