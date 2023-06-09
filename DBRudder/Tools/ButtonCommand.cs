﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Tools
{
    public class ButtonCommand : ICommand
    {
        private Action _action;
        private Func<Task> _func;

        public ButtonCommand(Action action)
        {
            _action = action;
        }

        public ButtonCommand(Func<Task> func)
        {
            _func = func;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if(_action != null) _action();
            if(_func != null) _func();
        }
    }

    public class ButtonCommand<T> : ICommand
    {
        private Action<T> _action;

        public ButtonCommand(Action<T> action)
        {
            _action = action;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (_action != null) _action((T)parameter);
        }
    }
}
