using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfTechPharma.Auxiliares
{
    static class Ultis
    {
        public static bool check(TextBox e, int length)
        {
            string text = e.Text.Replace("_", "").Replace(".", "").Replace("-", "");

            if (text.Length == length)
            {
                e.BorderBrush = (Brush)new BrushConverter().ConvertFromString("#8937FF00");
                e.Foreground = (Brush)new BrushConverter().ConvertFromString("#FF0E9403");
                return true;
            }
            else
            {
                e.BorderBrush = (Brush)new BrushConverter().ConvertFromString("#89E40000");
                e.Foreground = (Brush)new BrushConverter().ConvertFromString("#FFCC1212");
                return false;
            }
        }

        public static bool check(ComboBox e)
        {
            if (e.SelectedIndex != -1)
            {
                e.BorderBrush = (Brush)new BrushConverter().ConvertFromString("#8937FF00");
                e.Foreground = (Brush)new BrushConverter().ConvertFromString("#FF0E9403");
                return true;
            }
            else
            {
                e.BorderBrush = (Brush)new BrushConverter().ConvertFromString("#89E40000");
                e.Foreground = (Brush)new BrushConverter().ConvertFromString("#FFCC1212");
                return false;
            }
        }

        public static bool check(TextBox e)
        {
            if (e.Text.Length != 0)
            {
                e.BorderBrush = (Brush)new BrushConverter().ConvertFromString("#8937FF00");
                e.Foreground = (Brush)new BrushConverter().ConvertFromString("#FF0E9403");
                return true;
            }
            else
            {
                e.BorderBrush = (Brush)new BrushConverter().ConvertFromString("#89E40000");
                e.Foreground = (Brush)new BrushConverter().ConvertFromString("#FFCC1212");
                return false;
            }
        }

        public static bool check(DatePicker e)
        {
            if (e.SelectedDate != null)
            {
                e.BorderBrush = (Brush)new BrushConverter().ConvertFromString("#8937FF00");
                e.Foreground = (Brush)new BrushConverter().ConvertFromString("#FF0E9403");
                return true;
            }
            else
            {
                e.BorderBrush = (Brush)new BrushConverter().ConvertFromString("#89E40000");
                e.Foreground = (Brush)new BrushConverter().ConvertFromString("#FFCC1212");
                return false;
            }
        }
    }
}
