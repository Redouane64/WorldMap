using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using World.ViewModels;

#if DEBUG
#endif

namespace World
{
	using i = Microsoft.Xaml.Behaviors;

	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MapWindow : Window
	{

		private readonly DoubleAnimation fadeInAnimation;
		private readonly DoubleAnimation fadeOutAnimation;

		public MapWindow(MapViewModel viewModel)
		{
			InitializeComponent();

			DataContext = viewModel;
			InitializeMapDrawings(viewModel);

			fadeInAnimation = (DoubleAnimation)Application.Current.Resources["opacityFadeInAnimation"];
			fadeOutAnimation = (DoubleAnimation)Application.Current.Resources["opacityFadeOutAnimation"];
		}

		private void InitializeMapDrawings(MapViewModel viewModel)
		{
			foreach (Path pathItem in viewModel.Map.Items.OfType<Path>())
			{
				pathItem.MouseEnter += Path_OnMouseEnter;
				pathItem.MouseLeave += Path_OnMouseLeave;

				var pathEventTrigger = new i.EventTrigger("MouseEnter");
				var executeCommandAction = new i.InvokeCommandAction
				{
					Command = viewModel.GetCountryCommand,
					CommandParameter = pathItem.Name.ToLower()
				};

				pathEventTrigger.Actions.Add(executeCommandAction);
				pathEventTrigger.Attach(pathItem);
			}
		}

		private void Path_OnMouseLeave(object sender, MouseEventArgs e)
		{
			infoAreaBorder.BeginAnimation(Border.OpacityProperty, fadeOutAnimation);
		}

		private void Path_OnMouseEnter(object sender, MouseEventArgs mouseEventArgs)
		{
			infoAreaBorder.BeginAnimation(Border.OpacityProperty, fadeInAnimation);
		}

	}
}
