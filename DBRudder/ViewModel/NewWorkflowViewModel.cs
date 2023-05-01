using Core.Model;
using DBRudder.Model;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;
using Windows.ApplicationModel.UserDataTasks;

namespace DBRudder.ViewModel
{
    public class NewWorkflowViewModel : NewObservableObject
    {
        public ObservableCollection<Model.Action> Actions { get; set; }

        public ButtonCommand NewActionCommand { get; set; }

        public AsyncCommand StartCommand { get; set; }

        public NewWorkflowViewModel()
        {
            Actions = new ObservableCollection<Model.Action>();
            NewActionCommand = new ButtonCommand(NewAction);
            StartCommand = new AsyncCommand(RunWorflow);
        }

        private void NewAction()
        {

            App.GetStream().MessageSend += MessageReceved;
            App.GetRouter().Navegate(App.Get<View.NewDatabaseView>());
        }

        private void MessageReceved(object sender, Tools.MessageEventArgs e)
        {
            if(e.From == nameof(NewDatabaseViewModel))
            {
                Actions.Add(e.Message as Model.Action);
            }
            App.GetStream().MessageSend -= MessageReceved;
        }

        private Workflow wf;
        private async Task RunWorflow()
        {
            List<Core.Model.Action> actionsCORE = new List<Core.Model.Action>();
            foreach(var action in Actions)
            {
                actionsCORE.Add(action.CoreObject);
                action.IsWorking = true;
            }
            wf = new Workflow("Workflow", actionsCORE.ToArray());
            wf.PropertyChanged += Wf_PropertyChanged;
            // TODO: Adding out params
            await wf.StartAsync();

            //foreach (var action in Actions)
            //    action.IsWorking = false;
        }

        private void Wf_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            App.GetDispatcherQueue().TryEnqueue(() =>
            {
                Actions[0].Progress = wf.Progression;
            });

        }
    }
}
