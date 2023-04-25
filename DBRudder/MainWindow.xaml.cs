using Microsoft.UI.Xaml;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DBRudder
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public ViewModel.MainWindowViewModel ViewModel { get; set; }
        public MainWindow()
        {
            this.InitializeComponent();
            ViewModel = App.Get(ViewModel);
        }

    }
}
