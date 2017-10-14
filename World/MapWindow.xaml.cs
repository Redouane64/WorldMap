using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using World.Data.Repositories;

#if DEBUG
using System.Diagnostics;
#endif

namespace World
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MapWindow : Window
	{
		private readonly SqliteConnection connection;
		private readonly WorldRepository worldRepository;

		public MapWindow()
		{
			InitializeComponent();

			ItemsControl countriesItemsControl = (ItemsControl)mainGrid.Children
																		.OfType<ScrollViewer>()
																		.First().Content;

			foreach (Path pathItem in countriesItemsControl.Items.OfType<Path>())
			{
				pathItem.MouseEnter += Path_OnMouseEnter;
				pathItem.MouseLeave += Path_OnMouseLeave;
			}

			connection = new SqliteConnection("Data Source=Assets/world.db");
			worldRepository = new WorldRepository(connection);
		}

		private void Path_OnMouseLeave(object sender, MouseEventArgs e)
		{
			if (infoAreaBorder.DataContext == null)
				return;

			infoAreaBorder.BeginAnimation(Border.OpacityProperty, new DoubleAnimation
			{
				Duration = new Duration(TimeSpan.FromSeconds(0.25)),
			});
		}

		private async void Path_OnMouseEnter(object sender, MouseEventArgs mouseEventArgs)
		{
			Path path = sender as Path;
#if DEBUG
			Debug.WriteLine($"Mouse over {path.Name}");
			var watch = Stopwatch.StartNew();
#endif
			await connection.OpenAsync();
			var country = await Task.FromResult(worldRepository.GetByKey(path.Name.ToLower()));

			if(country != null)
			{
				infoAreaBorder.DataContext = country;

				infoAreaBorder.BeginAnimation(Border.OpacityProperty, new DoubleAnimation
				{
					To = 1,
					Duration = new Duration(TimeSpan.FromSeconds(0.4)),
					FillBehavior = FillBehavior.HoldEnd
				});
			}

			connection.Close();

#if DEBUG
			watch.Start();
			Debug.WriteLine($"Took {watch.Elapsed} to retreive country data from database.");
#endif
		}

	}
}
