using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HotelManagementDesktop.ViewModel
{
    class Command : ICommand
    {
        bool _canExecute;
        Action _action;

        public event EventHandler CanExecuteChanged;

        public Command(Action action, bool canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public void Execute(object parameter)
        {
            _action();
        }
    }

    class CommandPara1 : ICommand
    {
        bool _canExecute;
        Action<object> _action;

        public event EventHandler CanExecuteChanged;

        public CommandPara1(Action<object> action, bool canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public void Execute(object parameter)
        {
            _action(parameter);
        }
    }
}
