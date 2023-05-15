using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace DBRudder.ViewModel
{
    public class NewActionSelectorViewModel : NewObservableObject
    {

        public ObservableCollection<String> ActionsName { get; set; }

        private int _actionNameSelected = 0;
        public int ActionNameSelected 
        {
            get
            {
               return _actionNameSelected;
            }
            set 
            {
                _actionNameSelected = value;
                OnPropertyChanged();
            }
        }


        public NewActionSelectorViewModel(Model.RegisteredActions registered)
        {
            ActionsName = new ObservableCollection<string>(registered.GetActionNames());

        }
    }
}
