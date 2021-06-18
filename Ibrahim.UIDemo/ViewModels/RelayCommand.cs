using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ibrahim.Scheduler.ViewModels
{
    class RelayCommand : ICommand
    {
        private readonly Action invokeOperation;
        private readonly Func<bool> canExecute;

        public RelayCommand(Action invokeOperation,Func<bool> canExecute)
        {
            this.canExecute = canExecute;
            this.invokeOperation = invokeOperation;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return canExecute.Invoke();
        }

        public void Execute(object parameter)
        {
            invokeOperation.Invoke();
        }
    }
}
