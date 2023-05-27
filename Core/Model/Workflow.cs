using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Core.Attributes;

namespace Core.Model
{
    public class Workflow : INotifyPropertyChanged
    {
        private Dictionary<Type, Object> _storedObjects;

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
            _storedObjects = new Dictionary<Type, Object>();
        }

        public Action this[int index]
        {
            get => _actions[index];
        }

        private void IncProgression() => Progression += _step;

        public async Task StartAsync()
        {
            await Task.Run(async () =>
            {
                _step = 100 / _actions.Count;
                Progression = 0;

                foreach (var action in _actions)
                {
                    //Thread.Sleep(5* 1000);
                    var m = action.GetType().GetMethod("Settup");
                    var p = m?.GetParameters();
                    if (p?[0] != null)
                    {
                        var att = (ObjectInjectAttribute[]) p[0].GetCustomAttributes(typeof(ObjectInjectAttribute), false);
                        if(att != null && att.Length > 0)
                        {
                            List<Object> param = new();
                            foreach (Type? a in att[0].TargetType)
                            {
                                if (!_storedObjects.TryGetValue(a, out object? o))
                                    throw new Exceptions.PropertyNotFoundException(a);
                                param.Add((Object)o);
                            }
                            Object[] objParam = new object[1];
                            objParam[0] = (Object[])param.ToArray();
                            m?.Invoke(action, objParam);
                        }
                    }

                    await action.Run();
                    if(action.ResultArtifact != null)
                    {
                        _storedObjects.Add(action.ResultArtifactType!, action.ResultArtifact);
                    }
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

        public void StoreObject<T>(T obj)
        {
            if (!_storedObjects.ContainsKey(typeof(T)))
                _storedObjects.Add(typeof(T), obj!);
            else
                throw new Exception("Type already defined");
        }

    }
}
