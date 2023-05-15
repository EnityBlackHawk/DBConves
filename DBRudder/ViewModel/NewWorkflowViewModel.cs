using Core.Model;
using DBRudder.Model;
using DBTelegraph;
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
        public ButtonCommand<Model.Action> NewActionRecevedCommand { get; set; }
        public ConfigClass ConfigClass { get; set; }

        private int _progress;

        public int Progress
        {
            get { return _progress; }
            set { _progress = value; OnPropertyChanged(); }
        }

        private bool _isWorking;

        public bool IsWorking
        {
            get { return _isWorking; }
            set { _isWorking = value; OnPropertyChanged(); }
        }

        private Type _newActionAdding = null;

        public Type NewActionAdding
        {
            get { return _newActionAdding; }
            set { _newActionAdding = value; OnPropertyChanged(); }
        }


        public NewWorkflowViewModel()
        {
            Actions = new ObservableCollection<Model.Action>();
            NewActionCommand = new ButtonCommand(NewAction);
            StartCommand = new AsyncCommand(RunWorflow);
            NewActionRecevedCommand = new ButtonCommand<Model.Action>(NewActionReceved);


            ConfigClass = new DBTelegraph.ConfigClass(
                "Server=BLACKHAWKPC\\SQLSERVER;Trusted_Connection=True;",
                DBTelegraph.Model.SGBD.SQL_SERVER
                );


        }

        private void NewAction()
        {

            App.GetStream().MessageSend += MessageReceved;
            App.GetRouter().Navegate(App.Get<View.NewDatabaseView>());
        }

        private void NewActionReceved(Model.Action action)
        {
            Actions.Add(action);
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
            IsWorking = true;
            List<Core.Model.Action> actionsCORE = new List<Core.Model.Action>();
            foreach(var action in Actions)
            {
                actionsCORE.Add(action.CoreObject);
                action.IsWorking = true;
            }
            wf = new Workflow("Workflow", actionsCORE.ToArray());
            wf.PropertyChanged += Wf_PropertyChanged;
            wf.StoreObject(ConfigClass);
            await wf.StartAsync();
            IsWorking = false;
        }

        private void Wf_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            App.GetDispatcherQueue().TryEnqueue(() =>
            {
                Progress = wf.Progression;
            });
        }

        public void AddingNewAction(string actionName)
        {
            var r = App.Get<RegisteredActions>();
            NewActionAdding = r.GetActionByName(actionName);
        }

    }
}
