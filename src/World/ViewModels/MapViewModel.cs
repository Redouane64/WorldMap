using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using World.Commands;
using World.Data.Common;
using World.Data.Models;

namespace World.ViewModels
{
	public class MapViewModel : INotifyPropertyChanged
	{
		private readonly ICountriesRepository _repository;
		private readonly ItemsControl _mapControl;
		private readonly ICommand _getCountryCommand;

		private Country country;

		public event PropertyChangedEventHandler PropertyChanged;

		public MapViewModel(ICountriesRepository repository, ItemsControl mapControl)
		{
			_repository = repository;
			_mapControl = mapControl;

			_getCountryCommand = new RelyCommand(new Func<object, Task>(GetCountryCommandExecuted),
										new Predicate<object>(CanExecutedGetCountry));
		}

		private bool CanExecutedGetCountry(object parameter)
		{
			return true;
		}

		private async Task GetCountryCommandExecuted(object parameter)
		{
			Country = await Task.FromResult(_repository.GetByKey(parameter.ToString()));
		}

		public ICommand GetCountryCommand => _getCountryCommand;

		public ItemsControl Map => _mapControl;

		public Country Country
		{
			get => country;
			private set
			{
				country = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Country)));
			}
		}
	}
}
