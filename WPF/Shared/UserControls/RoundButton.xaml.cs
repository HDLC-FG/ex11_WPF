using System.Windows;
using System.Windows.Controls;

namespace WPF.Shared.UserControls
{
    /// <summary>
    /// Logique d'interaction pour RoundButton.xaml
    /// </summary>
    public partial class RoundButton : UserControl
    {
        public RoundButton()
        {
            InitializeComponent();
        }

        public event RoutedEventHandler Click
        {
            add { AddHandler(Button.ClickEvent, value); }
            remove { RemoveHandler(Button.ClickEvent, value); }
        }
    }
}
