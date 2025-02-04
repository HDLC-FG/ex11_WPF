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
        public static readonly DependencyProperty OptionsCommandProperty =
            DependencyProperty.Register(
                "OptionsCommand",
                typeof(ICommand),
                typeof(AddRoundButton),
                new PropertyMetadata(null));

        public ICommand OptionsCommand
        {
            get { return (ICommand)GetValue(OptionsCommandProperty); }
            set { SetValue(OptionsCommandProperty, value); }
        }

        public AddRoundButton()
        {
            InitializeComponent();
        }
    }
}
