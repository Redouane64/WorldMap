using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

using World.ViewModels;

using Autofac;

using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using WorldMap.Common.Data.Repositories;
using WorldMap.Common.Data;

namespace World
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public App()
		{
			AppCenter.LogLevel = LogLevel.Verbose;
			AppCenter.Start("f15785e4-0ed1-453e-a5a2-a187337048e8", typeof(Analytics));
		}

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
			builder.RegisterType<XmlCountriesRepository>()
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
