using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
    public class Router : NewObservableObject
    {
        private object _currentView;
        public object CurrentView 
        { 
            get { return _currentView; } 
            set { _currentView = value; OnPropertyChanged(); }
        }

        private object _callerView;

        public object CallerView
        {
            get { return _callerView; }
            set { _callerView = value; OnPropertyChanged(); }
        }


        public void Navegate(object dest)
        {
            CallerView = CurrentView;
            CurrentView = dest;
        }

        public void NavegateBack()
        {
            object temp = CallerView;
            CallerView = CurrentView;
            CurrentView = temp;
        }

        public Router(object dest)
        {
            CurrentView = dest;
        }

    }
}
