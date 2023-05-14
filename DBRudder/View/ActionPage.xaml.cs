using DBRudder.Model;
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
        public List<Object> Elements { get;}
        public StackPanel stackPanel { get; set; }
        public ActionPage(ActionFactory actionFactory)
        {
            this.InitializeComponent();
            Elements = new List<Object>();
            stackPanel = new StackPanel();

            foreach(var prop in  actionFactory.Properties)
            {
                StackPanel sp = new StackPanel();
                sp.Orientation = Orientation.Horizontal;
                
                TextBox textBox = new TextBox();
                textBox.Tag = prop.Value1;
                
                TextBlock textBlock = new TextBlock();
                textBlock.Text = prop.Value1;

                sp.Children.Add(textBlock);
                sp.Children.Add(textBox);

                Elements.Add(textBlock);
                

                stackPanel.Children.Add(sp);
            }


            base.Content = stackPanel;

        }
    }
}
