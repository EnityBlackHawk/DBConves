using Microsoft.UI.Xaml;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Tools;
using DBRudder.ViewModel;
using System.Collections.Generic;
using System;
using DBRudder.Model;


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
        private static Microsoft.UI.Dispatching.DispatcherQueue dispatcherQueue;

        private static IHost _host = Host.CreateDefaultBuilder().ConfigureServices((services) =>
        {
            services.AddSingleton<ViewModel.MainWindowViewModel>();
            services.AddSingleton<ViewModel.NewDatabaseViewModel>();
            services.AddSingleton<ViewModel.NewWorkflowViewModel>();
            services.AddSingleton<ViewModel.NewActionSelectorViewModel>();
            services.AddSingleton<ViewModel.NewActionViewModel>();

            services.AddSingleton<Model.RegisteredActions>();

            services.AddSingleton<View.NewWorkflow>();
            services.AddSingleton<View.NewDatabaseView>();
            services.AddSingleton<View.NewActionSelectorPage>();
            services.AddSingleton<View.NewActionPage>();
        }).Build();

        private static Router _router;
        public static Router GetRouter() => _router;

        public static T Get<T>(T obj) where T : class => _host.Services.GetService(typeof(T)) as T;
        public static T Get<T>() where T : class => _host.Services.GetService(typeof(T)) as T;

        public static MessageStream GetStream() => _stream;

        public static Microsoft.UI.Dispatching.DispatcherQueue GetDispatcherQueue() => dispatcherQueue;

        private static ActionViewManager _viewManager;

        public static ActionViewManager GetViewActionManager() => _viewManager;

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            dispatcherQueue = Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread();
            _router = new Router(null);
            _viewManager = new ActionViewManager();
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            m_window = new MainWindow();
            _viewManager.Add(Get<View.NewDatabaseView>());
            _router.Navegate(Get<View.NewWorkflow>());
            m_window.Activate();
        }

        private Window m_window;
    }
}
