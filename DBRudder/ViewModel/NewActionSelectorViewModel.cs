using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DBRudder.ViewModel
{
    public class NewActionSelectorViewModel
    {
        public ObservableCollection<String> ActionsName { get; set; }


        public NewActionSelectorViewModel(Model.RegisteredActions registered)
        {
            ActionsName = new ObservableCollection<string>(registered.GetActionNames());

        }


    }
}
