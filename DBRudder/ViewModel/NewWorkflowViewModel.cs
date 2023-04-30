using DBRudder.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace DBRudder.ViewModel
{
    public class NewWorkflowViewModel : NewObservableObject
    {
        public ObservableCollection<Model.Action> Actions { get; set; }

        public ButtonCommand NewActionCommand { get; set; }

        public NewWorkflowViewModel()
        {
            Actions = new ObservableCollection<Model.Action>();
            NewActionCommand = new ButtonCommand(NewAction);
        }

        private void NewAction()
        {

            App.GetStream().MessageSend += MessageReceved;
            App.Get<ViewModel.MainWindowViewModel>().CurrentView = App.Get<View.NewDatabaseView>();
        }

        private void MessageReceved(object sender, Tools.MessageEventArgs e)
        {
            if(e.From == nameof(NewDatabaseViewModel))
            {
                Actions.Add(e.Message as Model.Action);
            }
            App.GetStream().MessageSend -= MessageReceved;
        }
    }
}
