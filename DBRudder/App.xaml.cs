using Microsoft.UI.Xaml;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using DBRudder.Tools;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DBRudder
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        private static MessageStream _stream = new MessageStream();

        private static IHost _host = Host.CreateDefaultBuilder().ConfigureServices((services) =>
        {
            services.AddSingleton<ViewModel.MainWindowViewModel>();
            services.AddSingleton<ViewModel.NewDatabaseViewModel>();
            services.AddSingleton<ViewModel.MessageTesteViewModel>();

            services.AddSingleton<View.MainView>();


        }).Build();


        public static T Get<T>(T obj) where T : class => _host.Services.GetService(typeof(T)) as T;
        public static T Get<T>() where T : class => _host.Services.GetService(typeof(T)) as T;

        public static MessageStream GetStream() => _stream;

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            m_window = new MainWindow();
            m_window.Activate();
        }

        private Window m_window;
    }
}
