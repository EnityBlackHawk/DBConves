using DBRudder.Model;
using DBTelegraph;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;
using Windows.Devices.AllJoyn;

namespace DBRudder.ViewModel
{
    public class NewDatabaseViewModel : NewObservableObject
    {
        public ObservableCollection<Model.Table> Tables { get; }

        private string _databaseName;

        public string DatabaseName
        {
            get { return _databaseName; }
            set { _databaseName = value.Replace(" ", "_"); OnPropertyChanged(); }
        }

        public ButtonCommand CreateDatabaseCommand { get; set; }

        private bool _isLoading;

        public bool IsLoading
        {
            get { return _isLoading; }
            set { _isLoading = value; OnPropertyChanged(); }
        }

        private bool _isDone;

        public bool IsDone
        {
            get { return _isDone; }
            set { _isDone = value; OnPropertyChanged(); }
        }

        private string _statusResult;
        public string StatusResult
        {
            get => _statusResult;
            set { _statusResult = value; OnPropertyChanged(); }
        }

        private string _status;
        public string Status
        {
            get => _status;
            set { _status = value; OnPropertyChanged(); }
        }

        public NewDatabaseViewModel()
        {
            Tables = new ObservableCollection<Model.Table>();
            CreateDatabaseCommand = new ButtonCommand(CreateAction);
        }

        public async void CreateDatabase()
        {
            Database ui_db = new Database(DatabaseName, Tables.ToList());

            var config = new DBTelegraph.ConfigClass(
                "Server=BLACKHAWKPC\\SQLSERVER;Trusted_Connection=True;",
                DBTelegraph.Model.SGBD.SQL_SERVER
                );
            var acc = new DataAccess( config );

            var task = acc.CreateDatabaseAndTablesAsync(ui_db);
            var result = await task;
            if(task.IsCompleted)
            {
                
                Status = result.Status;
                StatusResult = result.StatusResult;
                IsDone = true;
            }
        }

        private void CreateAction()
        {
            Database ui_db = new Database(DatabaseName, Tables.ToList());

            var actionCORE = new Core.Actions.CreateDatabaseAction(ui_db);
            var actionUI = new Model.Action("Create new database", actionCORE);
            App.GetStream().Send(
                this,
                new Tools.MessageEventArgs(
                    nameof(NewDatabaseViewModel),
                    "action",
                    actionUI
                    )
                );
             App.GetRouter().Navegate(App.Get<View.NewWorkflow>());
        }

    }
}
