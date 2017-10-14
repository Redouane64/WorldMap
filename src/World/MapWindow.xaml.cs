using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using World.Data.Xml;
using System.Windows.Media.Animation;
using System;

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
		private readonly CountriesRepository worldRepository;
		private readonly DoubleAnimation fadeInAnimation;
		private readonly DoubleAnimation fadeOutAnimation;

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

			worldRepository = new CountriesRepository("Assets/countries.xml");
			fadeInAnimation = (DoubleAnimation)Application.Current.Resources["opacityFadeInAnimation"];
			fadeOutAnimation = (DoubleAnimation)Application.Current.Resources["opacityFadeOutAnimation"];
		}

		private void Path_OnMouseLeave(object sender, MouseEventArgs e)
		{
			if (infoAreaBorder.DataContext == null)
				return;

			infoAreaBorder.BeginAnimation(Border.OpacityProperty, fadeOutAnimation);
		}

		private async void Path_OnMouseEnter(object sender, MouseEventArgs mouseEventArgs)
		{
			Path path = sender as Path;
#if DEBUG
			Debug.WriteLine($"Mouse over {path.Name}");
			var watch = Stopwatch.StartNew();
#endif
			var country = await Task.FromResult(worldRepository.GetByKey(path.Name.ToLower()));
			if(country != null)
			{
				infoAreaBorder.DataContext = country;
				infoAreaBorder.BeginAnimation(Border.OpacityProperty, fadeInAnimation);
			}
#if DEBUG
			watch.Start();
			Debug.WriteLine($"Took {watch.Elapsed} to retreive country data from database.");
#endif
		}

	}
}
