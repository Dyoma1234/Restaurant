﻿using System;
using System.Windows.Input;

namespace restaurant_manager
{
    public class DelegateCommand : ICommand
    {
        public DelegateCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException("execute");
            _canExecute = canExecute;
        }

        public DelegateCommand(ICommand clearCurCheck, object p)
        {
            this.clearCurCheck = clearCurCheck;
            this.p = p;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute != null ? _canExecute(parameter) : true;
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        Action<object> _execute;
        Predicate<object> _canExecute;
        private ICommand clearCurCheck;
        private object p;
    }
}