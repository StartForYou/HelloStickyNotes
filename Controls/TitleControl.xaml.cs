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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HelloStickyNotes.Controls
{
    /// <summary>
    /// TitleControl.xaml 的交互逻辑
    /// </summary>
    public partial class TitleControl : UserControl
    {
        public TitleControl()
        {
            InitializeComponent();
        }

        private void Button_Close(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this)?.Close();
        }

        private void Button_ToggleOptions(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.ContextMenu != null)
            {
                button.ContextMenu.DataContext = DataContext;
                button.ContextMenu.IsOpen = true;
            }
        }

        private void MenuItem_AlwaysOnTop(object sender, RoutedEventArgs e)
        {
            //e.
            
        }
    }
}
