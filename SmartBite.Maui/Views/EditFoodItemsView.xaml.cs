using SmartBite.Maui.ViewModels;

namespace SmartBite.Maui.Views;

public partial class EditFoodItemsView : ContentPage
{
	public EditFoodItemsView(EditFoodItemsViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;

        NavigationPage.SetHasNavigationBar(this, false);
    }
}