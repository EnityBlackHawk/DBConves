using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public abstract class Action
    {
        public string StatusReport { get; protected set; } = string.Empty;
        public string Status { get; protected set; } = string.Empty;

        public System.Action? OnActionCompleted { get; set; }

        protected abstract void OnRun();

        public async Task Run()
        {
            await Task.Run(OnRun);
            OnActionCompleted?.Invoke();
        }

    }
}
