using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;
using Windows.UI.Core;

namespace DBRudder.Model
{
    public class Action : NewObservableObject
    {
        public string Name { get; }
        public Core.Model.Action CoreObject { get; }

        public Dictionary<string, string> Properties { get; }

        private bool _isWorking;

        public bool IsWorking
        {
            get { return _isWorking; }
            set { _isWorking = value; OnPropertyChanged();}
        }

        private int _progress;

        public int Progress
        {
            get { return _progress; }
            set { _progress = value; OnPropertyChanged(); }
        }

        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(); }
        }

        private Core.Model.Result _result = null;

        public Core.Model.Result Result
        {
            get { return _result; }
            set { _result = value; OnPropertyChanged(); }
        }


        public Action(string name, Core.Model.Action coreObject)
        {
            Name = name;
            CoreObject = coreObject;
            Result = coreObject.Status;
            coreObject.ActionCompleted += ExecutionCompleted;
        }

        private void ExecutionCompleted(object sender, Core.Model.ActionCompletedEventArgs e)
        {

            App.GetDispatcherQueue().TryEnqueue(() => 
            {
                Result = e.Result;
            });
        }

        public static implicit operator Core.Model.Action(Action action) => action.CoreObject;

    }
}
