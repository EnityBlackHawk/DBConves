using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DBRudder.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewWorkflow : Page
    {
        public ViewModel.NewWorkflowViewModel ViewModel { get; set; }
        public NewWorkflow()
        {
            this.InitializeComponent();
            ViewModel = App.Get(ViewModel);
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {

            var content = new View.NewActionSelectorPage();
            ContentDialog cd = new ContentDialog();
            cd.XamlRoot = this.XamlRoot;
            cd.Content = content;
            cd.PrimaryButtonText = "OK";
            cd.CloseButtonText = "Cancel";
            ContentDialogResult result = await cd.ShowAsync();
            
            if(result == ContentDialogResult.Primary)
            {
                NewActionConfigDialog();
            }
        }


        public async void NewActionConfigDialog()
        {
            var factory = new ActionsFactories.AutoActionFactory(typeof(Core.Actions.FunctionAction));
            var actionPage = new ActionPage(factory);

            ContentDialog cd = new ContentDialog();
            cd.XamlRoot = this.XamlRoot;
            cd.Content = actionPage;
            cd.PrimaryButtonText = "OK";
            //cd.PrimaryButtonClick += AssignProp;
            cd.CloseButtonText = "Cancel";
            ContentDialogResult result = await cd.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                foreach(var x in actionPage.Elements)
                {
                    //factory.AddValue(x.Name, x.Tag);
                }

                var actionUI = new Model.Action(factory.Name, factory.CreateCoreAction());
                ViewModel.NewActionRecevedCommand.Execute(
                    actionUI
                    );
            }
        }

        private void AssignProp(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var actionPage = sender.Content as ActionPage ?? throw new Exception("Content is null");

        }

        private async void progressBar_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if(e.NewValue >= 100)
            {
                ((sender as ProgressBar).Resources["out_animation"] as Storyboard).Begin();
                checkMarkIcon.Opacity = 1;
                await Task.Delay(1000);
                checkMarkIcon.Opacity = 0;
            }
            else
                ((sender as ProgressBar).Resources["in_animation"] as Storyboard).Begin();
        }
    }
}
