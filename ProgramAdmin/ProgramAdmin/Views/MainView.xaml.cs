using ProgramAdmin.ViewModels;
using System;
using System.IO;
using System.Windows;
using LoggingTools;
using Telerik.Windows.Controls;

namespace ProgramAdmin.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            StyleManager.ApplicationTheme = new Windows8TouchTheme();
            DataContext = new MainViewModel();
            InitializeComponent();
        }
        private void MainView_OnLoaded(object sender, RoutedEventArgs e)
        {
            //ReleaseNotes.Initialize();
        }
    }
}
