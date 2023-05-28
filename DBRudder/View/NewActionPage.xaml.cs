using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DBRudder.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewActionPage : Page
    {
        private ViewModel.NewActionViewModel ViewModel { get; }
        public NewActionPage(ViewModel.NewActionViewModel viewModel)
        {
            this.InitializeComponent();
            ViewModel = viewModel;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.SetActionCommand.Execute(null);
        }
    }
}
