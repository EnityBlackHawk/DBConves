using Core.Model;
using DBRudder.CustomElements;
using DBRudder.Model;
using DBRudder.ViewModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Tools;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DBRudder.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ActionPage : Page
    {
        public List<IExportableValue> Elements { get;}
        public StackPanel stackPanel { get; set; }

        private ActionFactory _factory;
        public ActionPage(ActionFactory actionFactory)
        {
            _factory = actionFactory;
            this.InitializeComponent();
            Elements = new List<IExportableValue>();
            stackPanel = new StackPanel();

            foreach(var prop in  actionFactory.Properties)
            {
                StackPanel sp = new StackPanel();
                sp.Orientation = Orientation.Horizontal;
                
                ExportableTextBox textBox = new ExportableTextBox();
                textBox.Id = prop.Value1;
                
                TextBlock textBlock = new TextBlock();
                textBlock.Text = prop.Value1;

                sp.Children.Add(textBlock);
                sp.Children.Add(textBox);

                Elements.Add(textBox);
                

                stackPanel.Children.Add(sp);
            }
            Button button = new Button();
            button.Content = "Salvar";
            button.Click += save_click;
            stackPanel.Children.Add(button);


            base.Content = stackPanel;
        }

        private void save_click(object sender, RoutedEventArgs e)
        {
            foreach (var exportableValue in Elements)
            {
                _factory?.AddValue(exportableValue.Id, Convert.ChangeType(exportableValue.Value, exportableValue.ExportType));
            }
            _factory.CreateCoreAction();
        }
    }
}
