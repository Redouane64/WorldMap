using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

using World.Data.Common;
using World.Data.Xml;
using World.ViewModels;

using Autofac;

namespace World
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{

		public IContainer Container
		{
			get;
			private set;
		}

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			var builder = new ContainerBuilder();

			builder.RegisterInstance<Stream>(GetResourceStream(new Uri("countries.xml", UriKind.Relative)).Stream);
			builder.RegisterType<CountriesRepository>()
					.As<ICountriesRepository>();
			builder.RegisterInstance<ItemsControl>(Resources["worldMap"] as ItemsControl);
			builder.RegisterType<MapViewModel>();
			builder.RegisterType<MapWindow>();

			Container = builder.Build();

			MainWindow = Container.Resolve<MapWindow>();
			MainWindow.Show();
		}
	}
}
