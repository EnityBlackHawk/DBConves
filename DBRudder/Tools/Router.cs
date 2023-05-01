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

        public void Navegate(object dest)
        {
            CurrentView = dest;
        }

        public Router(object dest)
        {
            CurrentView = dest;
        }

    }
}
