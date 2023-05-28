using Core.ActionsFactories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public delegate void ActionEventHandler<TArgs>(Action sender, TArgs args);


    public abstract class Action
    {
        public abstract string Name { get; }

        public Model.Result? Status { get; set; }

        public System.Action? OnActionCompleted { get; set; }

        public Object? ResultArtifact { get; protected set; }

        public Type? ResultArtifactType { get; protected set; }

        public event EventHandler<Model.ActionCompletedEventArgs>? ActionCompleted;
        public event ActionEventHandler<Model.ActionSettupEventArgs>? ActionSettup;

        protected abstract void OnRun();

        public virtual void OnActionCreation()
        {
            Methods.OnActionCreated(this, new ActionCreatedEventArgs(this));
        }

        public virtual void Settup(params object[] args) {}

        public virtual void ResetValues()
        {
            Status = null;
            ActionSettup?.Invoke(this, new ActionSettupEventArgs());
        }

        public async Task Run()
        {
            await Task.Run(OnRun);
            OnActionCompleted?.Invoke();
            ActionCompleted?.Invoke(this, new ActionCompletedEventArgs(Status!, ResultArtifact, ResultArtifactType));
        }
    }
}
