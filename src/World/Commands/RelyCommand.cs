using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace World.Commands
{
	public class RelyCommand : ICommand
	{
		private readonly Func<object, Task> _action;
		private readonly Predicate<object> _predicate;

		public RelyCommand(Func<Object, Task> action, Predicate<object> predicate)
		{
			_action = action;
			_predicate = predicate;
		}

		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter)
		{
			return _predicate(parameter);
		}

		public void Execute(object parameter)
		{
			_action(parameter);
		}
	}
}
