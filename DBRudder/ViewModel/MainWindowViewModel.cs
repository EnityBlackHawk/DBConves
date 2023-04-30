using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace DBRudder.ViewModel
{
    public class MainWindowViewModel : NewObservableObject
    {
        private Page _newDatabaseView = new View.NewDatabaseView();

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }


        public MainWindowViewModel() 
        {
            //CurrentView = _newDatabaseView;
            CurrentView = App.Get<View.NewWorkflow>();
        }
        
    }
}
