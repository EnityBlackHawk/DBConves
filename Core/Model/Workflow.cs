using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public class Workflow
    {
        private List<Action> _actions;
        public List<Action> Actions
        {
            get => _actions;
        }

        private double _step;
        public string Name { get; }
        public double Progression { get; private set; } = 0.0;

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
            _step = 100 / _actions.Count;

            foreach (var action in _actions)
            {
                await action.Run();
                IncProgression();
            }
        }

        public void AddAction(Action action) => _actions.Add(action);

    }
}
