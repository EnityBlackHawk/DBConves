#nullable enable
using DBRudder.ActionsFactories;
using DBRudder.CustomElements;
using DBRudder.Model;
using DBRudder.View;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;
using Windows.Web.Syndication;

namespace DBRudder.ViewModel
{
    public class NewActionViewModel : NewObservableObject
    {
        public ObservableCollection<string> ActionNames { get; set; }


        private int _selectedAction;

        public int SelectedAction
        {
            get { return _selectedAction; }
            set { _selectedAction = value; OnPropertyChanged(); }
        }

        private object _options;

        public object Options
        {
            get { return _options; }
            set { _options = value; OnPropertyChanged(); }
        }


        public AsyncCommand SetActionCommand { get; set; }
        public AsyncCommand CreateActionCommand { get; set; }

        private List<IExportableValue> _exportableValues;
        private Type? _actionAdding;
        private AutoActionFactory? _factory;
        private RegisteredActions _registeredActions;

        public NewActionViewModel(RegisteredActions registeredActions)
        {
            ActionNames = new ObservableCollection<string>(registeredActions.GetActionNames());
            SetActionCommand = new AsyncCommand(SetAction);
            CreateActionCommand = new AsyncCommand(CreateAction);
            _registeredActions = registeredActions;
        }


        private async Task SetAction()
        {
            _actionAdding = _registeredActions.GetActionByName(ActionNames[SelectedAction]);

           
            if(App.GetViewActionManager().Get(_actionAdding, out IActionView customActionViewType))
            {
                var actionPage = customActionViewType as Page ?? null;
                Options = actionPage!;
            }
            else
            {
                _factory = new ActionsFactories.AutoActionFactory(_actionAdding);

                var actionPage = new ActionPage(_factory);
                Options = actionPage;
                _exportableValues = actionPage.Elements;
            }


        }

        private async Task CreateAction()
        {
            foreach(var exportableValue in _exportableValues)
            {
                _factory?.AddValue(exportableValue.Id, Convert.ChangeType(exportableValue.Value, exportableValue.ExportType));
            }
            var actionUI = new Model.Action(_factory?.Name, _factory?.CreateCoreAction());
            App.GetStream().Send(
                this, 
                new MessageEventArgs(
                    nameof(NewActionViewModel),
                    MessagesKeys.NewAction,
                    actionUI
                    ));
            App.GetRouter().NavegateBack();
        }

    }
}
