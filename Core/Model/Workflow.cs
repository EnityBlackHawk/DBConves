using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public class Workflow : INotifyPropertyChanged
    {
        private List<Action> _actions;
        public List<Action> Actions
        {
            get => _actions;
        }

        private int _step;


        public string Name { get; }
        private int _progression;

        public int Progression
        {
            get { return _progression; }
            set { _progression = value; OnPropertyChanged(); }
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        public Workflow(string name, params Action[] actions)
        {
            _actions = new(actions);
            Name = name;
        }

        public Action this[int index]
        {
            get => _actions[index];
        }

        private void IncProgression() => Progression =+ _step;

        public async Task StartAsync()
        {
            await Task.Run(async () =>
            {
                //Thread.Sleep(5 * 1000);
                _step = 100 / _actions.Count;

                foreach (var action in _actions)
                {
                    await action.Run();
                    IncProgression();
                }
            });
            
        }

        public void AddAction(Action action) => _actions.Add(action);

        private void OnPropertyChanged([CallerMemberName] string? member = null, System.Action? callback = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(member));
            callback?.Invoke();
        }

    }
}
