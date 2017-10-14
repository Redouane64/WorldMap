using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using World.Data.Xml;

#if DEBUG
using System.Diagnostics;
#endif

namespace World
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private readonly WorldRepository worldRepository;

		public MainWindow()
		{
			InitializeComponent();

			ItemsControl countriesItemsControl = (ItemsControl)mainGrid.Children
																		.OfType<ScrollViewer>()
																		.First().Content;

			foreach (Path pathItem in countriesItemsControl.Items.OfType<Path>())
			{
				pathItem.MouseEnter += Path_OnMouseEnter;
			}

			worldRepository = new WorldRepository("Assets/countries.xml");
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
			}
#if DEBUG
			watch.Start();
			Debug.WriteLine($"Took {watch.Elapsed} to retreive country data from database.");
#endif
		}

	}
}
