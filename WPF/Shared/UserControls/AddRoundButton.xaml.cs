using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPF.Shared.UserControls
{
    /// <summary>
    /// Logique d'interaction pour RoundButton.xaml
    /// </summary>
    public partial class AddRoundButton : UserControl
    {
        public static readonly DependencyProperty ActionCommandProperty =
            DependencyProperty.Register(
                "ActionCommand",
                typeof(ICommand),
                typeof(AddRoundButton),
                new PropertyMetadata(null));

        public ICommand ActionCommand
        {
            get { return (ICommand)GetValue(ActionCommandProperty); }
            set { SetValue(ActionCommandProperty, value); }
        }

        public AddRoundButton()
        {
            InitializeComponent();
        }
    }
}
