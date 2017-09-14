using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace World
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private Style pathStyle;

		public MainWindow()
		{
			InitializeComponent();

			pathStyle = (Style) Application.Current.Resources["CountryPathStyle"];

			foreach (Path countryPath in ((Grid) ((ScrollViewer) mainGrid.Children[0]).Content).Children)
			{
				countryPath.MouseEnter += Path_OnMouseEnter;
				countryPath.MouseLeave += Path_OnMouseLeave;

				countryPath.Style = pathStyle;
			}
			
		}

		private void Path_OnMouseLeave(object sender, MouseEventArgs mouseEventArgs)
		{
			Path path = sender as Path;
#if DEBUG
			Debug.WriteLine($"Mouse left {path.Name}");
#endif

		}

		private void Path_OnMouseEnter(object sender, MouseEventArgs mouseEventArgs)
		{
			Path path = sender as Path;
#if DEBUG
			Debug.WriteLine($"Mouse over {path.Name}");
#endif

		}

	}
}
