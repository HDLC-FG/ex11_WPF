using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ApplicationCore.Models;
using WPF.Events;
using static ApplicationCore.Enums;

namespace WPF.Shared.UserControls
{
    /// <summary>
    /// Logique d'interaction pour AddOrUpdateVehicle.xaml
    /// </summary>
    public partial class AddOrUpdateVehicle : UserControl
    {
        public static readonly DependencyProperty DataContextVehicleProperty =
            DependencyProperty.Register(
                "DataContextVehicle",
                typeof(Vehicle),
                typeof(AddOrUpdateVehicle),
                new PropertyMetadata(null));

        public Vehicle DataContextVehicle
        {
            get { return (Vehicle)GetValue(DataContextVehicleProperty); }
            set { SetValue(DataContextVehicleProperty, value); }
        }

        public static readonly DependencyProperty EngineTypesContextProperty =
            DependencyProperty.Register(
                "EngineTypes",
                typeof(List<EngineType>),
                typeof(AddOrUpdateVehicle),
                new PropertyMetadata(null));

        public List<EngineType> EngineTypes
        {
            get { return (List<EngineType>)GetValue(EngineTypesContextProperty); }
            set { SetValue(EngineTypesContextProperty, value); }
        }

        public static readonly DependencyProperty ApplyCommandProperty =
            DependencyProperty.Register(
                "ApplyCommand",
                typeof(ICommand),
                typeof(AddOrUpdateVehicle),
                new PropertyMetadata(null));

        public ICommand ApplyCommand
        {
            get { return (ICommand)GetValue(ApplyCommandProperty); }
            set { SetValue(ApplyCommandProperty, value); }
        }

        public static readonly DependencyProperty OptionsCommandProperty =
            DependencyProperty.Register(
                "OptionsCommand",
                typeof(ICommand),
                typeof(AddOrUpdateVehicle),
                new PropertyMetadata(null));

        public ICommand OptionsCommand
        {
            get { return (ICommand)GetValue(OptionsCommandProperty); }
            set { SetValue(OptionsCommandProperty, value); }
        }

        public AddOrUpdateVehicle()
        {
            InitializeComponent();
        }
    }
}
