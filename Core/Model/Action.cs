using Core.ActionsFactories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public abstract class Action
    {
        public abstract string Name { get; }
        public string StatusReport { get; protected set; } = string.Empty;
        public string Status { get; protected set; } = string.Empty;

        public System.Action? OnActionCompleted { get; set; }

        public Object? Result { get; protected set; }

        public Type? ResultType { get; protected set; }

        protected abstract void OnRun();

        public virtual void OnActionCreation()
        {
            Methods.OnActionCreated(this, new ActionCreatedEventArgs(this));
        }

        public virtual void Settup(params object[] args) { }

        public async Task Run()
        {
            await Task.Run(OnRun);
            OnActionCompleted?.Invoke();
        }
    }
}
