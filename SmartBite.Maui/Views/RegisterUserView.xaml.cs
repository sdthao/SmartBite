using SmartBite.Maui.ViewModels;

namespace SmartBite.Maui.Views;

public partial class RegisterUserView : ContentPage
{
	public RegisterUserView(RegisterUserViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;

		NavigationPage.SetHasNavigationBar(this, false);
    }
}