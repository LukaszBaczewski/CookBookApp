using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CookBookApp.Commands
{
    public class RelayCommandParam : ICommand
    {
        #region Fields
        readonly Action<object> _execute;
        readonly Func<bool> _canExecute;
        #endregion

        #region Constructors
        public RelayCommandParam(Action<object> execute) : this(execute, null) { }
        public RelayCommandParam(Action<object> execute, Func<Boolean> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            _execute = execute;
            _canExecute = canExecute;
        }
        #endregion

        #region ICommand Members
        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute();
        }
        public event EventHandler CanExecuteChanged
        {

            add
            {
                if (_canExecute != null)
                    CommandManager.RequerySuggested += value;
            }
            remove
            {
                if (_canExecute != null)
                    CommandManager.RequerySuggested -= value;
            }
        }

        public void Execute(object parameter) { _execute(parameter); }


        #endregion
    }
}
