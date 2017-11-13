using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using World.Data.Xml;
using System.Windows.Media.Animation;
using System;
using World.Data.Common;
using World.ViewModels;

#if DEBUG
using System.Diagnostics;
#endif

namespace World
{
	using i = System.Windows.Interactivity;

	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MapWindow : Window
	{

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

				var pathEventTrigger = new i.EventTrigger("MouseEnter");
				var executeCommandAction = new i.InvokeCommandAction
				{
					Command = (mainGrid.DataContext as MapViewModel).GetCountryCommand,
					CommandParameter = pathItem.Name.ToLower()
				};

				pathEventTrigger.Actions.Add(executeCommandAction);
				pathEventTrigger.Attach(pathItem);
			}

			fadeInAnimation = (DoubleAnimation)Application.Current.Resources["opacityFadeInAnimation"];
			fadeOutAnimation = (DoubleAnimation)Application.Current.Resources["opacityFadeOutAnimation"];

		}

		private void Path_OnMouseLeave(object sender, MouseEventArgs e)
		{
			if (infoAreaBorder.DataContext == null)
				return;

			infoAreaBorder.BeginAnimation(Border.OpacityProperty, fadeOutAnimation);
		}

		private void Path_OnMouseEnter(object sender, MouseEventArgs mouseEventArgs)
		{
			infoAreaBorder.BeginAnimation(Border.OpacityProperty, fadeInAnimation);
		}

	}
}
