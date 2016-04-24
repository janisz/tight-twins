using System.Windows;

namespace Twins
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainViewModel MainViewModel { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            MainViewModel = new MainViewModel();
            DataContext = MainViewModel;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            MainViewModel.StartGame();
        }

        private void Reset_OnClick(object sender, RoutedEventArgs e)
        {
            MainViewModel = new MainViewModel();
            DataContext = MainViewModel;
        }
    }
}
