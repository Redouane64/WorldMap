using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using World.Commands;
using World.Data.Common;
using World.Data.Models;
using World.Data.Xml;

namespace World.ViewModels
{
	public class MapViewModel : INotifyPropertyChanged
	{
		private readonly ICountriesRepository countriesRepository;
		private readonly ICommand getCountryCommand;
		private Country country;

		public event PropertyChangedEventHandler PropertyChanged;

		public MapViewModel()
		{
			// TO DO: should be injected .
			var resourceInfo = Application.GetResourceStream(new Uri("countries.xml", UriKind.Relative));
			countriesRepository = new CountriesRepository(resourceInfo.Stream);

			getCountryCommand = new RelyCommand(new Func<object, Task>(GetCountryCommandExecuted),
										new Predicate<object>(CanExecutedGetCountry));
		}

		private bool CanExecutedGetCountry(object parameter)
		{
			return true;
		}

		private async Task GetCountryCommandExecuted(object parameter)
		{
			Country = await Task.FromResult(countriesRepository.GetByKey(parameter.ToString()));
		}

		public ICommand GetCountryCommand => getCountryCommand;

		public ItemsControl Map => Application.Current.Resources["worldMap"] as ItemsControl;

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
