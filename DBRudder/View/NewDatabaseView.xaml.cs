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
    public sealed partial class NewDatabaseView : Page, IActionView
    {
        public ViewModel.NewDatabaseViewModel ViewModel { get; set; }

        Type IActionView.ViewModelType => typeof(ViewModel.NewDatabaseViewModel);

        object IActionView.ViewModelObject => ViewModel;

        Type IActionView.CoreActionType => typeof(Core.Actions.CreateDatabaseAction);

        public NewDatabaseView()
        {
            ViewModel = App.Get(ViewModel);
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int tag = (int)(sender as Button).Tag;
            var c = ViewModel.Tables[tag].columns;
            c.Add(new Model.Column { Name = $"Column {c.Count + 1}", Type = "Int32", IsPrimaryKey = c.Count == 0, TableId = tag});
        }

        private void AddTable(object sender, RoutedEventArgs e)
        {
            ViewModel.Tables.Add(new Model.Table { Id = ViewModel.Tables.Count, Name = "Nova tabela", columns = new() });
        }

        private void RemoveTable(object sender, RoutedEventArgs e)
        {
            int tag = (int)((sender as Button).Tag);
            ViewModel.Tables.RemoveAt(tag);
        }

        private void RemoveColumn(object sender, RoutedEventArgs e)
        {
            Column column = (Column)((sender as Button).Tag);
            ViewModel.Tables[column.TableId].columns.Remove(column);
        }
    }
}
